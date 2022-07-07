using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Models.RequestModels;
using System.Threading.Tasks;

namespace Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IServiceCustomer _serviceCustomer;

        public CustomerController(IServiceCustomer serviceCustomer)
        {
            _serviceCustomer = serviceCustomer;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerList()
        {
            if (!ModelState.IsValid)
            {
                return (IActionResult)ModelState;
            }

            var customers = await _serviceCustomer.GetList();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _serviceCustomer.FindById(id);

            if (customer == null)
            {
                return BadRequest("Customer doesn't exist");
            }

            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _serviceCustomer.Delete(id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] CustomerRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return (IActionResult)ModelState;
            }

            var result = await _serviceCustomer.Insert(model);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] CustomerRequestModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return (IActionResult)ModelState;
            }

            var result = await _serviceCustomer.Update(model, id);

            return Ok(result);
        }

        [HttpGet("{id}/Document")]
        public async Task<IActionResult> GetCustomerWithDocuments(int id)
        {
            var result = await _serviceCustomer.GetAllDocumentsForCustomer(id);

            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }



        [HttpGet("{customerId}/Document/{documentId}")]
        public async Task<IActionResult> GetCustomerWithDocument(int customerId, int documentId)
        {
            var result = await _serviceCustomer.GetDocumentForCustomer(customerId, documentId);

            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

    }
}
