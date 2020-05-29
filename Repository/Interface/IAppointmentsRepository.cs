using EHealthConsult.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHealthConsult.Repository.Interface
{
    //IRepositoryBase interface is inherited into IAppointmentsRepository interface
    /*
     When a class implements the inherited interface then it must provide the implementation of all 
     the members that are defined within the interface inheritance chain
    */
    public interface IAppointmentsRepository : IRepositoryBase<Appointment>
    {
        //getallcustomersasync
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(int Id);
        Task<Appointment> GetAppointmentByEmailAsync(string Email);
        void CreateAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(Appointment appointment);
    }
}
