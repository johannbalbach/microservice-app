namespace Shared.DTO
{
    public class ManagerWithPaginationInfo
    {
        public List<ManagerDTO> managers { get; set; }
        public int? size { get; set; }
        public int? elementsCount { get; set; }
        public int? pageCurrent { get; set; }
    }
}
