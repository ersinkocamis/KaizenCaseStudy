using KaizenCaseStudy.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KaizenCaseStudy.API.Controllers
{
    public class FirstQuestionController(IFirstQuestionService firstQuestionService) : Controller
    {
        private readonly IFirstQuestionService _firstQuestionService = firstQuestionService;

        [HttpGet("generate-codes")]
        public IActionResult GenerateCodes()
        {
            string[] result = _firstQuestionService.GenerateCodes();
            return result.Count() != 0 ? Ok(result) : NoContent();
        }

        [HttpGet("check-code/{code}")]
        public IActionResult CheckCode(string code)
        {
            bool result = _firstQuestionService.CheckCode(code);
            return _firstQuestionService.CheckCode(code) ? Ok($"Code {code} is valid") : BadRequest("Invalid code");
        }
    }
}