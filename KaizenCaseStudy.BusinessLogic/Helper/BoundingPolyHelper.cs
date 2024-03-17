using KaizenCaseStudy.BusinessLogic.DTO;
using KaizenCaseStudy.BusinessLogic.Models.Request;

namespace KaizenCaseStudy.BusinessLogic.Helper
{
    public static class BoundingPolyHelper
    {
        public static List<BoundingPolyDto> PrepareBoundingPolyDto(List<ReceiptRequest> fileContent)
        {
            List<BoundingPolyDto> boundingPolyDtos = [];
            foreach (var content in fileContent)
            {
                var vertices = content.BoundingPoly.Vertices.ToList();
                boundingPolyDtos.Add(new BoundingPolyDto
                {
                    Text = content.Description,
                    Height = ((vertices[3].Y - vertices[0].Y) + (vertices[2].Y - vertices[0].Y)) / 2,
                    AverageYAxis = vertices.Average(x => x.Y),
                    BeginningOfText = (vertices[0].X + vertices[3].X) / 2
                });
            }

            return boundingPolyDtos;
        }

        public static double CalculateTextThreshold(List<BoundingPolyDto> boundingPolyDtos)
        {
            List<double> differences = [];
            for (int i = 0; i < boundingPolyDtos.Count; i++)
            {
                if (i == 0)
                {
                    differences.Add(boundingPolyDtos[i].AverageYAxis);
                    continue;
                }
                differences.Add(Math.Abs(boundingPolyDtos[i].AverageYAxis - boundingPolyDtos[i - 1].AverageYAxis));
            }

            var average = differences.Average();
            var diffsSquareSums = differences.Sum(x => (x - average) * (x - average));
            var standardDeviation = Math.Sqrt(diffsSquareSums / differences.Count);
            return standardDeviation;
        }

        public static Dictionary<int, IEnumerable<BoundingPolyDto>> GroupTextsByAvgYAxis(List<BoundingPolyDto> boundingPolyDtos, double threshold)
        {
            Dictionary<int, IEnumerable<BoundingPolyDto>> groupedTexts = [];
            int lineNumber = 1;

            for (int i = 0; i < boundingPolyDtos.Count; i++)
            {
                if (i == 0)
                {
                    groupedTexts.Add(lineNumber, [boundingPolyDtos[i]]);
                    continue;
                }

                var diff = Math.Abs(boundingPolyDtos[i].AverageYAxis - boundingPolyDtos[i - 1].AverageYAxis);
                if (diff > threshold)
                {
                    lineNumber++;
                    if (groupedTexts.TryGetValue(lineNumber, out IEnumerable<BoundingPolyDto>? value))
                        groupedTexts[lineNumber] = value.Append(boundingPolyDtos[i]);
                    else
                        groupedTexts.Add(lineNumber, [boundingPolyDtos[i]]);
                }
                else
                {
                    groupedTexts[lineNumber] = groupedTexts[lineNumber].Append(boundingPolyDtos[i]);
                }
            }

            return groupedTexts;
        }
    }
}