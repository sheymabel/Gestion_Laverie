using GestionLaverie.Domaine.Entities;

namespace LiverieAPI.Controller
{
  
        public interface IProprietaireService
        {
            void Create(Proprietaire proprietaire);
            Proprietaire GetById(int id);
            IEnumerable<Proprietaire> GetAll();
            bool Update(Proprietaire proprietaire);
            bool Delete(int id);
        }
    }


