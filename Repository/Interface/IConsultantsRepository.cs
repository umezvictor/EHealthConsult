using EHealthConsult.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHealthConsult.Repository.Interface
{
    public interface IConsultantsRepository : IRepositoryBase<Consultant>
    {
        //in this class we extend the IReposioryBase
        //in ConsultantRepository class we then call the methods
        //in IRepositoryBase

        Task<IEnumerable<Consultant>> GetAllConsultantsAsync();

        Task<Consultant> GetConsultantByIdAsync(int Id);
        Task<Consultant> GetConsultantByEmailAsync(string Email);
        void CreateConsultant(Consultant consultant);
        void UpdateConsultant(Consultant consultant);
        void DeleteConsultant(Consultant consultant);
    }
}
