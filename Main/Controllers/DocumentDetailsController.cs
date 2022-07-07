using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentDetailsController : ControllerBase
    {
        private readonly IServiceDocumentDetails _serviceDocumentDetails;

        public DocumentDetailsController(IServiceDocumentDetails serviceDocumentDetails)
        {
            _serviceDocumentDetails = serviceDocumentDetails;
        }

        [HttpGet]
        public async Task<IActionResult> GetDocumentDetailsList()
        {
            var documentsDetails = await _serviceDocumentDetails.GetList();
            return Ok(documentsDetails);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _serviceDocumentDetails.Delete(id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentDetails(int id)
        {
            var documentDetails = await _serviceDocumentDetails.FindById(id);

            if (documentDetails == null)
            {
                return BadRequest("Customer doesn't exist");
            }

            return Ok(documentDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] DocumentDetailsRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return (IActionResult)ModelState;
            }

            var result = await _serviceDocumentDetails.Insert(model);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] DocumentDetailsRequestModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return (IActionResult)ModelState;
            }

            var result = await _serviceDocumentDetails.Update(model, id);

            return Ok(result);
        }

    }
}
