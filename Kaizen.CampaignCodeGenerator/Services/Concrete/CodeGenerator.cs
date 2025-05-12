using Kaizen.CampaignCodeGenerator.Models;
using Kaizen.CampaignCodeGenerator.Services.Abstract;
using Kaizen.CampaignCodeGenerator.Utils;

namespace Kaizen.CampaignCodeGenerator.Services.Concrete;

/// <summary>
/// Özel bir kontrol algoritmasına göre (burada biz 7 karakter random 1 karakter mod23'e göre checksum kullandık) doğrulanabilir ve eşsiz kodlar üretir.
/// /// </summary>
public class CodeGenerator : ICodeGenerator
{
    public string GenerateCode()
    {
        var codeChars = new char[CodeConfig.CodeLength];
        int checksum = 0;

        for (int i = 0; i < CodeConfig.DataPartLength; i++)
        {
            int index = RandomProvider.Instance.Next(CodeConfig.Alphabet.Length); // 0-22 arası bir sayı seçiliyor.
            codeChars[i] = CodeConfig.Alphabet[index]; // karakter seçiliyor örneğin G, 2, K gibi
            checksum += index;
        }

        // Toplanan indekslerin toplamı % 23 alınır (çünkü alfabe 23 karakterli)
        codeChars[7] = CodeConfig.Alphabet[checksum % CodeConfig.Alphabet.Length];

        return new string(codeChars);// char[] birleştirilerek 8 karakterli bir stringe dönüştürülüyor burada.
    }

    public List<string> GenerateUniqueCodes(int count)
    {
        var codes = new HashSet<string>(); // aynı kod iki kez üretilirse otomatik olarak sadece birini alır → Unique garantisi

        while (codes.Count < count)
        {
            codes.Add(GenerateCode());
        }

        return new List<string>(codes);
    }
}