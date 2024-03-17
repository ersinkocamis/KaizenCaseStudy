using KaizenCaseStudy.BusinessLogic.Models.Request;

namespace KaizenCaseStudy.BusinessLogic.DTO
{
    public class BoundingPolyDto
    {
        public string? Text { get; set; }
        public double AverageYAxis{ get; set; }
        public decimal Height { get; set; }
        public decimal BeginningOfText { get; set; }
    }
}