using EHealthConsult.Models;
using EHealthConsult.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHealthConsult.Repository.RepositoryClasses
{
    public class AppointmentsRepository : RepositoryBase<Appointment>, IAppointmentsRepository
    {
        public AppointmentsRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public void CreateAppointment(Appointment appointment)
        {
            Create(appointment);
        }

        public void DeleteAppointment(Appointment appointment)
        {
            Delete(appointment);
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByEmailAsync(string Email)
        {
            return await FindByCondition(appointment => appointment.Email.Equals(Email)).FirstOrDefaultAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int Id)
        {
            return await FindByCondition(appointment => appointment.Id.Equals(Id)).FirstOrDefaultAsync();
        }

        public void UpdateAppointment(Appointment appointment)
        {
            Update(appointment);
        }
    }
}

