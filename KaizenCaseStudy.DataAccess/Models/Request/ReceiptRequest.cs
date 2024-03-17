namespace KaizenCaseStudy.DataAccess.Models.Request
{
    public class ReceiptRequest
    {
        public ReceiptRequest()
        {
            ReceiptBoundingPoly = new();
        }

        public string? Locale { get; set; }
        public string? Description { get; set; }
        public BoundingPolyRequest ReceiptBoundingPoly { get; set; }
    }
}