namespace Management.Domain.Models.Query
{
    public class ManagersFilterQuery
    {
        public string Type { get; set; }
        public Guid? FacultyId { get; set; }
        public Guid? ApplicantId { get; set; }
        public string Search { get; set; }
        public string SortBy { get; set; }
    }
}
