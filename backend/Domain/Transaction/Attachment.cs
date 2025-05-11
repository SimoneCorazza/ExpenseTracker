namespace ExpenseTracker.Domain.Transaction
{
    public class Attachment
    {
        /// <summary>
        ///     Attachment id
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        ///     Name of the attachment
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///    Mime type of the attachment
        /// </summary>
        public string MimeType { get; private set; }

        /// <summary>
        ///    Description of the attachment
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        ///    Id of the object storage where the attachment is stored
        /// </summary>
        public string ObjectStorageId { get; private set; }

        public Attachment(string name, string? description, string objectStorageId, string mimeType)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            ObjectStorageId = objectStorageId;
            MimeType = mimeType;

            Validate();
        }

        public void Update(string name, string? description, string mimeType, string objectStorageId)
        {
            Name = name;
            Description = description;
            ObjectStorageId = objectStorageId;
            MimeType = mimeType;

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new DomainException("The name is mandatory");
            }

            if (string.IsNullOrWhiteSpace(ObjectStorageId))
            {
                throw new DomainException("The object storage id is mandatory");
            }

            if (string.IsNullOrWhiteSpace(MimeType))
            {
                throw new DomainException("The mime type is mandatory");
            }
        }
    }
}
