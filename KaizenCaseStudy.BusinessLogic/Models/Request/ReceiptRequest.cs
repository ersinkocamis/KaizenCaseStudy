namespace KaizenCaseStudy.BusinessLogic.Models.Request
{
    public class ReceiptRequest
    {
        public ReceiptRequest()
        {
            BoundingPoly = new();
        }

        public string? Locale { get; set; }
        public string? Description { get; set; }
        public BoundingPolyRequest BoundingPoly { get; set; }
    }
}