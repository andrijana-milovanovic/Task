using System;
using System.Collections.Generic;

namespace Models.DTOs
{
    public class DocumentViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public CustomerDTO Customer { get; set; }
        public List<DocumentDetailsDTO> DocumentsDetails { get; set; }
    }
}
