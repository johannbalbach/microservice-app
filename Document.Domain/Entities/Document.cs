﻿using Shared.Enums;

namespace Document.Domain.Entities
{
    public abstract class Document
    {
        public Guid Id { get; set; }
        public Guid ApplicantId { get; set; }
        public DocumentType DocumentType { get; set; }
        public List<Guid> FilesId { get; set; } = new List<Guid>();
    }
}
