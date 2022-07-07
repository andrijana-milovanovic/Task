using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace Models.RequestModels
{
    public class DocumentWithDetailsRequestModel
    {
        [BindRequired]
        public DateTime Date { get; set; }
        public string Note { get; set; }
        [BindRequired]
        public int CustomerId { get; set; }
        public List<DocumentDetailsRequestModel> DocumentDetails { get; set; }
    }
}
