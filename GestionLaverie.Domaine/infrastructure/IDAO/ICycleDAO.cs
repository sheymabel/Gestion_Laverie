using GestionLaverie.Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionLaverie.Domaine.infrastructure.IDAO
{
    public interface ICycleDAO
    {
        Task<IEnumerable<Cycle>> GetAllCyclesAsync();
        Task<Cycle> GetCycleByIdAsync(int id);
        Task AddCycleAsync(Cycle cycle);
        Task UpdateCycleAsync(Cycle cycle);
        Task DeleteCycleAsync(int id);
    }
}
