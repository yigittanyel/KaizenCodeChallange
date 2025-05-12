using Kaizen.ReceiptParser.Models;

namespace Kaizen.ReceiptParser.Services;

/// <summary>
/// OCR sonucu olan OCRWord listesini alır, ve Line nesneleri listesine dönüştürür.
/// </summary>
public class ReceiptProcessor
{
    // Bir kelime ile bir satırın Y (dikey) konumları arasındaki fark bu değerden küçükse → aynı satır kabul edilir.
    private const int LineThreshold = 10;

    public List<Line> Process(List<OCRWord> words)
    {
        // yalnızca geçerli ve konumu bilinen kelimeler işleniyor
        var filtered = words
                      .Where(w => !string.IsNullOrWhiteSpace(w.Description) && w.BoundingPoly?.Vertices?.Any() == true)
                      .ToList();

        var lines = new List<Line>();

        foreach (var word in filtered)
        {
            var y = word.BoundingPoly.GetTop();

            // Var olan Line'lar içinde Y değeri bu kelimeye yakın olan satır aranır.Eğer fark LineThreshold(10 piksel) kadar küçükse → o satır bu kelimeyle hizalı kabul edilir.
                        var line = lines.FirstOrDefault(l => Math.Abs(l.Y - y) < LineThreshold);
            if (line == null)
            {
                line = new Line();
                lines.Add(line);
            }

            line.Words.Add(word);
        }

        // Satırları yukarıdan aşağı sırala
        return lines.OrderBy(l => l.Y).ToList();
    }
}