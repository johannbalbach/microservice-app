using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface IExternalSystemService
    {
        Task<string> GetEducationLevelsAsync();
        Task<string> GetDocumentTypesAsync();
        Task<string> GetFacultiesAsync();
        Task<string> GetProgramsAsync(int page, int size);
    }
}
