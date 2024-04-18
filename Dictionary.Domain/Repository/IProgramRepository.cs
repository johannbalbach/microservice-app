using Shared.DTO.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Domain.Repository
{
    public interface IProgramRepository<UniversityProgram> where UniversityProgram : Entities.UniversityProgram
    {
        Task<UniversityProgram> GetByIdAsync(Guid id);
        Task<UniversityProgram> GetByIdIntAsync(int id);
        Task<IEnumerable<UniversityProgram>> GetAllAsync();
        Task AddAsync(UniversityProgram entity);
        Task UpdateAsync(UniversityProgram entity);
        Task DeleteAsync(UniversityProgram entity);
        Task SaveChangesAsync();
        Task<List<UniversityProgram>> GetProgramsWithPaginationAndFiltering(ProgramsFilterQuery query);
        Task<int> ElementsCount();
    }
}
