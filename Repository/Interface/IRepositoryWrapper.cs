using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHealthConsult.Repository.Interface
{
    public interface IRepositoryWrapper
    {
        IAppointmentsRepository Appointments { get; }
        IConsultantsRepository Consultants { get; }

        Task SaveAsync();
    }
}
