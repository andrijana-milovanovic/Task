using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class DocumentDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Document Document { get; set; }
        [Required]
        public int DocumentId { get; set; }
        [Required]
        public string ArticleName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
