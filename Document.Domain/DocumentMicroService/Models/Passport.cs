using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Document.Domain.Models
{
    public class Passport
    {
        public Guid? Id { get; set; }
        public Guid? ApplicantId { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public string IssuedBy { get; set; }
        public string BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
        public List<Guid?> ScansId { get; set; }
    }
}
