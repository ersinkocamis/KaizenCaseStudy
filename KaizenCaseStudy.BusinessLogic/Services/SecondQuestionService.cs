using KaizenCaseStudy.BusinessLogic.Helper;
using KaizenCaseStudy.BusinessLogic.Interfaces;
using KaizenCaseStudy.BusinessLogic.Models.Request;
using KaizenCaseStudy.BusinessLogic.Models.Response;
using Microsoft.AspNetCore.Http;

namespace KaizenCaseStudy.BusinessLogic.Services
{
    public class SecondQuestionService : ISecondQuestionService
    {
        public List<ReceiptResponse> ExtractFromFile(IFormFile file)
        {
            List<ReceiptResponse> response = [];

            var fileContent = FileHelper.Read<List<ReceiptRequest>>(file);

            fileContent!.RemoveRange(0, 1);

            var boundingPolyDtos = BoundingPolyHelper.PrepareBoundingPolyDto(fileContent);

            boundingPolyDtos = boundingPolyDtos.OrderBy(x => x.AverageYAxis).ToList();

            double threshold = BoundingPolyHelper.CalculateTextThreshold(boundingPolyDtos);

            var groupedTexts = BoundingPolyHelper.GroupTextsByAvgYAxis(boundingPolyDtos, threshold);

            foreach (var groupedText in groupedTexts)
            {
                response.Add(new ReceiptResponse
                {
                    Line = groupedText.Key,
                    Text = string.Join(' ', groupedText.Value.OrderBy(x => x.BeginningOfText).Select(x => x.Text))
                });
            }
            return response != null && response.Count > 0 ? response : ([]);
        }
    }
}