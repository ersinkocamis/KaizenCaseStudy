using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace KaizenCaseStudy.BusinessLogic.Helper
{
    public static class FileHelper
    {
        public static T Read<T>(IFormFile file)
        {
            using StreamReader streamReader = new(file.OpenReadStream());
            string readFile = streamReader.ReadToEnd();
            var fileContent = JsonConvert.DeserializeObject<T>(readFile);
            return fileContent!;
        }
    }
}