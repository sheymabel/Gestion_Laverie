using GestionLaverie.Domaine.Entities;

namespace LiverieAPI.Model.Domaine.IDAO
{
    public class IDAOPropretaire
    {
        public interface IDAOProp
        {
            bool Ajouter(Proprietaire p);
            Proprietaire TrouverParId(int id);
            List<Proprietaire> ListerTous();
            bool MettreAJour(Proprietaire entite);
            bool Supprimer(int id);

        }
    }
}
