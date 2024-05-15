using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class ApplicantWithPaginationInfo
    {
        public List<ApplicantWithIdDTO> applicants { get; set; }
        public int? size { get; set; }
        public int? elementsCount { get; set; }
        public int? pageCurrent { get; set; }
    }
}
