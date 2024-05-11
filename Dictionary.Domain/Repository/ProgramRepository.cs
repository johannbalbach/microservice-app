using Dictionary.Domain.Context;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.Query;
using Dictionary.Domain.Entities;

namespace Dictionary.Domain.Repository
{
    public class ProgramRepository<UniversityProgram>: IProgramRepository<UniversityProgram> where UniversityProgram : Entities.UniversityProgram
    {
        private readonly AppDbContext _context;
        private readonly DbSet<UniversityProgram> _dbSet;

        public ProgramRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<UniversityProgram>();
        }

        public async Task<UniversityProgram> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<UniversityProgram> GetByIdIntAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<UniversityProgram>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(UniversityProgram entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(UniversityProgram entity)
        {
            _dbSet.Update(entity);
        }

        public async Task DeleteAsync(UniversityProgram entity)
        {
            _dbSet.Remove(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<List<UniversityProgram>> GetProgramsWithPaginationAndFiltering(ProgramsFilterQuery query)
        {
            var programsQuery = _dbSet.AsQueryable();

            if (query.FacultyId.HasValue)
            {
                programsQuery = programsQuery.Where(p => p.FacultyId == query.FacultyId);
            }

            if (query.educationLevelId != null)
            {
                programsQuery = programsQuery.Where(p => p.EducationLevel.Id == query.educationLevelId);
            }

            if (query.educationForm != null)
            {
                programsQuery = programsQuery.Where(p => p.EducationForm == query.educationForm);
            }

            if (!string.IsNullOrEmpty(query.Language))
            {
                programsQuery = programsQuery.Where(p => p.Language.ToLower() == query.Language.ToLower());
            }

            if (!string.IsNullOrEmpty(query.Search))
            {
                programsQuery = programsQuery.Where(p => p.Name.ToLower().Contains(query.Search.ToLower()));
            }

            var pageSize = query.Size ?? 10;
            var pageNumber = query.Page ?? 1;

            var programs = await programsQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return programs;
        }
        public async Task<int> ElementsCount()
        {
            return await _dbSet.CountAsync();
        }
    }
}
