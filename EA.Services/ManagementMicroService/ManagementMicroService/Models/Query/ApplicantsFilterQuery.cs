namespace Management.Domain.Models.Query
{
    public class ApplicantsFilterQuery
    {
        public string Search { get; set; }
        public DateTime? BirthDateFrom { get; set; }
        public DateTime? BirthDateTo { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        public string PhoneNumber { get; set; }
    }
}
