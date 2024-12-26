using GestionLaverie.Domaine.Entities;
using LiverieAPI.Model.Domaine.IDAO;

namespace LiverieAPI.Infrastructuer
{

    public class RepoImplimentation : IDAOPropretaire.IDAOProp
    {

        private List<Proprietaire> proprietaires = new List<Proprietaire>();

        public bool Ajouter(Proprietaire p)
        {
            try
            {
                proprietaires.Add(p);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Proprietaire TrouverParId(int id) => proprietaires.FirstOrDefault(p => p.Id == id);

        public List<Proprietaire> ListerTous()
        {
            return proprietaires;
        }

        public bool MettreAJour(Proprietaire entite)
        {
            var proprietaire = TrouverParId(entite.Id);
            if (proprietaire != null)
            {
                proprietaire.Id = entite.Id;
                proprietaire.NomProprietaire = entite.NomProprietaire;
                return true;
            }
            return false;
        }

        public bool Supprimer(int id)
        {
            var proprietaire = TrouverParId(id);
            if (proprietaire != null)
            {
                proprietaires.Remove(proprietaire);
                return true;
            }
            return false;
        }
    }
}


