namespace KaizenCaseStudy.BusinessLogic.Models.Request
{
    public class BoundingPolyRequest
    {
        public BoundingPolyRequest()
        {
            Vertices = Enumerable.Empty<VerticesRequest>();
        }

        public IEnumerable<VerticesRequest> Vertices { get; set; }
    }
}