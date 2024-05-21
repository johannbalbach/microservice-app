namespace User.Domain.Entities
{
    public class Manager
    {
        public Guid Id {  get; set; }
        public required UserE User { get; set; }
        public Guid? FacultyId { get; set; }
    }
}
