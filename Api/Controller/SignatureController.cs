using Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core;

namespace Api.Controller
{
    [Route("api/signature")]
    [ApiController]
    public class SignatureController : ControllerBase
    {
        private readonly ISignatureRepository _signatureRepository;

        public SignatureController(ISignatureRepository signatureRepository)
        {
            _signatureRepository = signatureRepository;
        }


        [HttpPost("create")]
        public IActionResult CreateSignature(Signature signature)
        {
            _signatureRepository.CreateSignature(signature);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllSignatures()
        {
            return Ok(_signatureRepository.GetAllSignatures());
        }

        [HttpGet("get-specific/{id}")]
        public IActionResult GetSignatureById(string id)
        {
            return Ok(_signatureRepository.GetSignatureById(id));
        }

        [HttpPut]
        public IActionResult UpdateSignature(Signature signature)
        {
            _signatureRepository.UpdateSignature(signature);
            return Ok();
        }

        [HttpDelete("delete-specific/{id}")]
        public IActionResult DeleteSignature(string id)
        {
            _signatureRepository.DeleteSignature(id);
            return Ok();
        }
        
    }
}
