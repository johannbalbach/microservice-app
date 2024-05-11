namespace Document.Domain.Entities
{
    public class EducationDocument: Document
    {
        public EducationDocument() {
            DocumentType = Shared.Enums.DocumentType.EducationDoc;
        }
        public EducationDocument(string name, Guid doctype)
        {
            DocumentType = Shared.Enums.DocumentType.EducationDoc;
            Name = name;
            DocumentTypeGuid = doctype;
        }
        public string Name { get; set; }
        public Guid DocumentTypeGuid { get; set; }
    }
}
