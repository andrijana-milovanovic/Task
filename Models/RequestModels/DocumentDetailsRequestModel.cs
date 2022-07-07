using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models.RequestModels
{
    public class DocumentDetailsRequestModel
    {
        [BindRequired]
        public int DocumentId { get; set; }

        [BindRequired]
        public string ArticleName { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(1, double.MaxValue)]
        public double Price { get; set; }
    }
}
