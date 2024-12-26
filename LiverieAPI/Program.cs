using LiverieAPI.Model.Business;
using LiverieAPI.Model.Domaine;
using LiverieAPI.Infrastructuer;
using LiverieAPI.Model.Domaine.IDAO;
using GestionLaverie.Domaine.infrastructure.IDAO;
using Microsoft.AspNetCore.WebSockets;
using System.Net.WebSockets;
using System.Text;

// Créer et configurer le serveur
var builder = WebApplication.CreateBuilder(args);

// Ajouter les services nécessaires
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enregistrer les services pour l'injection de dépendances
builder.Services.AddScoped<ICycleDAO, ICycleDAO>();
builder.Services.AddScoped<ILaverieDAO, ILaverieDAO>();
builder.Services.AddScoped<IMachineDAO, IMachineDAO>();
builder.Services.AddScoped<IProprietaireDAO, IProprietaireDAO>();

// Ajouter des services personnalisés
builder.Services.AddScoped<IDAOPropretaire.IDAOProp, RepoImplimentation>(); // Utilise RepoImplimentation comme implémentation de IDAOProp
builder.Services.AddScoped<ConfigurationBusiness>(); // Injecte ConfigurationBusiness là où nécessaire

// Ajouter WebSockets au container des services
builder.Services.AddWebSockets(options =>
{
    options.KeepAliveInterval = TimeSpan.FromSeconds(120); // Définir l'intervalle de "keep alive" des connexions
});

// Construire l'application après avoir ajouté tous les services
var app = builder.Build();

// Configurer le pipeline de requêtes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Utiliser WebSocket pour gérer les connexions WebSocket
app.UseWebSockets();

// Ajouter le handler WebSocket
app.Map("/ws", WebSocketHandler);

// Configuration de l'authorization
app.UseAuthorization();
app.MapControllers();

// Lancer l'application
app.Run();

// Fonction de gestion des connexions WebSocket
async Task WebSocketHandler(HttpContext context)
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        await HandleWebSocketConnection(webSocket);
    }
    else
    {
        context.Response.StatusCode = 400; // Code d'erreur pour requête WebSocket invalide
    }
}

// Fonction pour gérer la connexion WebSocket
async Task HandleWebSocketConnection(WebSocket webSocket)
{
    var buffer = new byte[1024 * 4];
    WebSocketReceiveResult result;
    do
    {
        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        // Traitez le message ici, puis renvoyez-le si nécessaire
        var message = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
        Console.WriteLine($"Message reçu: {message}");

        await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

    } while (!result.CloseStatus.HasValue); // Continuer tant qu'il n'y a pas de fermeture de la connexion WebSocket

    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
}
