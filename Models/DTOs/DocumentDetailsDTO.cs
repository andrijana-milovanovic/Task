namespace Models.DTOs
{
    public class DocumentDetailsDTO
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public string ArticleName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
