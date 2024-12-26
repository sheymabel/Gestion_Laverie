using GestionLaverie.Domaine.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionLaverie.Domaine.infrastructure.IDAO
{
    public interface IMachineDAO
    {
        Task<IEnumerable<Machine>> GetAllMachinesAsync();
        Task<Machine> GetMachineByIdAsync(int id);
        Task AddMachineAsync(Machine machine);
        Task UpdateMachineAsync(Machine machine);
        Task DeleteMachineAsync(int id);

    }
}
