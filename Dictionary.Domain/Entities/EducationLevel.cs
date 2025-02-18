﻿using System.ComponentModel.DataAnnotations;

namespace Dictionary.Domain.Entities
{
    public class EducationLevel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DocumentType>? DocumentTypes { get; set; } = new List<DocumentType>();
    }
}
