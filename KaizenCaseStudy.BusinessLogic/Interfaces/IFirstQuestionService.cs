namespace KaizenCaseStudy.BusinessLogic.Interfaces
{
    public interface IFirstQuestionService
    {
        string[] GenerateCodes();
        bool CheckCode(string code);
    }
}