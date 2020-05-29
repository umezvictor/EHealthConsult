using EHealthConsult.Models;
using EHealthConsult.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EHealthConsult.Repository.RepositoryClasses
{
    public class ConsultantsRepository : RepositoryBase<Consultant>, IConsultantsRepository
    {
        public ConsultantsRepository(AppDbContext dbContext) : base(dbContext)
        {

        }

        public void CreateConsultant(Consultant consultant)
        {
            Create(consultant); 
        }

        public void DeleteConsultant(Consultant consultant)
        {
            Delete(consultant);
        }

        public async Task<IEnumerable<Consultant>> GetAllConsultantsAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<Consultant> GetConsultantByEmailAsync(string Email)
        {
            return await FindByCondition(consultant => consultant.Email.Equals(Email)).FirstOrDefaultAsync();
        }

        public async Task<Consultant> GetConsultantByIdAsync(int Id)
        {
            return await FindByCondition(consultant => consultant.Id.Equals(Id)).FirstOrDefaultAsync();
        }

        public void UpdateConsultant(Consultant consultant)
        {
            Update(consultant);
        }

       
    }
}
