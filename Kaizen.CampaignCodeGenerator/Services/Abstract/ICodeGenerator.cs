namespace Kaizen.CampaignCodeGenerator.Services.Abstract;

public interface ICodeGenerator
{
    string GenerateCode();
    List<string> GenerateUniqueCodes(int count);
}
