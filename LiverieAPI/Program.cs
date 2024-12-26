using LiverieAPI.Model.Business;
using LiverieAPI.Model.Domaine;
using LiverieAPI.Infrastructuer;
using LiverieAPI.Model.Domaine.IDAO;
using GestionLaverie.Domaine.infrastructure.IDAO;
using Microsoft.AspNetCore.WebSockets;
using System.Net.WebSockets;
using System.Text;

// Cr�er et configurer le serveur
var builder = WebApplication.CreateBuilder(args);

// Ajouter les services n�cessaires
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enregistrer les services pour l'injection de d�pendances
builder.Services.AddScoped<ICycleDAO, ICycleDAO>();
builder.Services.AddScoped<ILaverieDAO, ILaverieDAO>();
builder.Services.AddScoped<IMachineDAO, IMachineDAO>();
builder.Services.AddScoped<IProprietaireDAO, IProprietaireDAO>();

// Ajouter des services personnalis�s
builder.Services.AddScoped<IDAOPropretaire.IDAOProp, RepoImplimentation>(); // Utilise RepoImplimentation comme impl�mentation de IDAOProp
builder.Services.AddScoped<ConfigurationBusiness>(); // Injecte ConfigurationBusiness l� o� n�cessaire

// Ajouter WebSockets au container des services
builder.Services.AddWebSockets(options =>
{
    options.KeepAliveInterval = TimeSpan.FromSeconds(120); // D�finir l'intervalle de "keep alive" des connexions
});

// Construire l'application apr�s avoir ajout� tous les services
var app = builder.Build();

// Configurer le pipeline de requ�tes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Utiliser WebSocket pour g�rer les connexions WebSocket
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
        context.Response.StatusCode = 400; // Code d'erreur pour requ�te WebSocket invalide
    }
}

// Fonction pour g�rer la connexion WebSocket
async Task HandleWebSocketConnection(WebSocket webSocket)
{
    var buffer = new byte[1024 * 4];
    WebSocketReceiveResult result;
    do
    {
        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        // Traitez le message ici, puis renvoyez-le si n�cessaire
        var message = System.Text.Encoding.UTF8.GetString(buffer, 0, result.Count);
        Console.WriteLine($"Message re�u: {message}");

        await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

    } while (!result.CloseStatus.HasValue); // Continuer tant qu'il n'y a pas de fermeture de la connexion WebSocket

    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
}
