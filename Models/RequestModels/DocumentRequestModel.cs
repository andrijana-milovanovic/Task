using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Models.RequestModels
{
    public class DocumentRequestModel
    {
        [BindRequired]
        public DateTime Date { get; set; }
        public string Note { get; set; }
        [BindRequired]
        public int CustomerId { get; set; }
    }
}
