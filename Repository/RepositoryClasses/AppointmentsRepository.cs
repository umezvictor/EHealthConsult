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
            //Delete method here actually performs an update
            Delete(appointment);
        }

        //return all appointments using isDeleted as the conndition
        public async Task<IEnumerable<Appointment>> GetAllAppointmentsByDeleteStatus(bool status)
        {
            return await FindByCondition(appointment => appointment.isDeleted.Equals(status)).ToListAsync();
         
        }

        //get all appointments where isDeleted = false or true
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
        public async Task<Appointment> GetAppointmentByReferenceAsync(string referenceNumber)
        {
            return await FindByCondition(appointment => appointment.ReferenceNumber.Equals(referenceNumber)).FirstOrDefaultAsync();
        }

        public void UpdateAppointment(Appointment appointment)
        {
            Update(appointment);
        }
    }
}

