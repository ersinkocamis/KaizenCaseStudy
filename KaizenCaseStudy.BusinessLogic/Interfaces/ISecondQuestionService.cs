using KaizenCaseStudy.BusinessLogic.Models.Response;
using Microsoft.AspNetCore.Http;

namespace KaizenCaseStudy.BusinessLogic.Interfaces
{
    public interface ISecondQuestionService
    {
        List<ReceiptResponse> ExtractFromFile(IFormFile file);
    }
}