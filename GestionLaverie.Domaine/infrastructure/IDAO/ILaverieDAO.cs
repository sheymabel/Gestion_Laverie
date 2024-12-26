using GestionLaverie.Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionLaverie.Domaine.infrastructure.IDAO
{
    public interface ILaverieDAO
    {
        Task<IEnumerable<Laverie>> GetAllLaveriesAsync();
        Task<Laverie> GetLaverieByIdAsync(int id);
        Task AddLaverieAsync(Laverie laverie);
        Task UpdateLaverieAsync(Laverie laverie);
        Task DeleteLaverieAsync(int id);
    }
}
