using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Models.RequestModels
{
    public class CustomerRequestModel
    {
        [BindRequired]
        public string Name { get; set; }
        [BindRequired]
        public string Adress { get; set; }
    }
}
