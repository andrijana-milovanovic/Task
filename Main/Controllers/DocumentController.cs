
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IServiceDocument _serviceDocument;

        public DocumentController(IServiceDocument serviceDocument)
        {
            _serviceDocument = serviceDocument;
        }

        [HttpGet]
        public async Task<IActionResult> GetDocumentList()
        {
            var documents = await _serviceDocument.GetList();
            return Ok(documents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument(int id)
        {
            var document = await _serviceDocument.FindById(id);

            if (document == null)
            {
                return BadRequest("Document doesn't exist");
            }

            return Ok(document);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _serviceDocument.Delete(id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] DocumentRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return (IActionResult)ModelState;
            }

            var result = await _serviceDocument.Insert(model);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] DocumentRequestModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return (IActionResult)ModelState;
            }

            var result = await _serviceDocument.Update(model, id);

            return Ok(result);
        }

        [HttpPost("DocumentWithDetails")]
        public async Task<IActionResult> InsertDocumentWithDetails([FromBody] DocumentWithDetailsRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return (IActionResult)ModelState;
            }

            var result = await _serviceDocument.InsertDocumentWithDetails(model);

            return Ok(result);
        }
    }
}
