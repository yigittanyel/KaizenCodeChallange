using Kaizen.CampaignCodeGenerator.Models;
using Kaizen.CampaignCodeGenerator.Services.Abstract;

namespace Kaizen.CampaignCodeGenerator.Services.Concrete;

/// <summary>
/// Stateless validation of codes using the same checksum logic as the generator.
/// </summary>
public class CodeValidator : ICodeValidator
{
    private readonly Dictionary<char, int> _charToIndex;

    // A-> 0 C-> 1 gibi indexleme mekanizması yaptım.
    public CodeValidator()
    {
        _charToIndex = CodeConfig.Alphabet
            .Select((c, i) => new { c, i })
            .ToDictionary(x => x.c, x => x.i);
    }

    public bool IsValid(string code)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Length != CodeConfig.CodeLength)
            return false;

        int checksum = 0;

        for (int i = 0; i < CodeConfig.DataPartLength; i++)
        {
            // sistem sadece geçerli karakter kümesindeki (ACDEFGHKLMNPRTXYZ234579) karakterleri kabul eder.
            if (!_charToIndex.TryGetValue(code[i], out int index))
                return false;

            checksum += index;
        }

        char expectedChecksum = CodeConfig.Alphabet[checksum % CodeConfig.Alphabet.Length];
        return code[7] == expectedChecksum;
    }
}