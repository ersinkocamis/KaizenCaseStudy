using KaizenCaseStudy.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KaizenCaseStudy.API.Controllers
{
    public class SecondQuestionController(ISecondQuestionService secondQuestionService) : Controller
    {
        private readonly ISecondQuestionService _secondQuestionService = secondQuestionService;

        [HttpPost("extract-lines")]
        public IActionResult SecondQuestion(IFormFile file)
        {
            var result = _secondQuestionService.ExtractFromFile(file);
            return result.Count != 0 ? Ok(result) : NoContent();
        }
    }
}