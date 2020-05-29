using EHealthConsult.Models;
using EHealthConsult.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHealthConsult.Repository.RepositoryClasses
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private AppDbContext _dbContext;


        private IConsultantsRepository consultant;
        private IAppointmentsRepository appointment;


        public RepositoryWrapper(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        

        public IAppointmentsRepository Appointments
        {
            get
            {
                if(appointment == null)
                {
                    appointment = new AppointmentsRepository(_dbContext);
                }

                return appointment;
            }
        }

        public IConsultantsRepository Consultants
        {
            get
            {
                if(consultant == null)
                {
                    consultant = new ConsultantsRepository(_dbContext);
                }
                return consultant;
            }
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
