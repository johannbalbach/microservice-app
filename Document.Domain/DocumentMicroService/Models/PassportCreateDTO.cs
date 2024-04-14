using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Document.Domain.Models
{
    public class PassportCreateDTO
    {
        public string Series { get; set; }
        public string Number { get; set; }
        public string IssuedBy { get; set; }
        public string BirthDate { get; set; }
        public string PlaceOfBirth { get; set; }
    }
}
