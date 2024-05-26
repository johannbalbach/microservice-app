using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class DocumentTypeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int EducationLevel { get; set; }
        public List<int>? NextEducationLevels { get; set; }
    }
}
