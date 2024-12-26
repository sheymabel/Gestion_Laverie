using GestionLaverie.Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionLaverie.Domaine.infrastructure.IDAO
{
    public interface IProprietaireDAO
    {

        Task<IEnumerable<Proprietaire>> GetAllProprietairesAsync();
        Task<Proprietaire> GetProprietaireByIdAsync(int id);
        Task AddProprietaireAsync(Proprietaire proprietaire);
        Task UpdateProprietaireAsync(Proprietaire proprietaire);
        Task DeleteProprietaireAsync(int id);
    }
}
