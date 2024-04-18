using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class ManagerWithPaginationInfo
    {
        public List<UserProfileDTO> managers = new List<UserProfileDTO>();
        public int? size { get; set; }
        public int? elementsCount { get; set; }
        public int? pageCurrent { get; set; }
    }
}
