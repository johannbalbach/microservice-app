using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Domain.Entities
{
    public abstract class Document
    {
        public Guid Id { get; set; }
        public string Path {  get; set; }
        public abstract Guid DocumentTypeId { get; set; }
    }
}
