namespace Kaizen.ReceiptParser.Models;

/// <summary>
/// Aynı satıra ait kelimeler
/// </summary>
public class Line
{
    public List<OCRWord> Words { get; set; } = new();

    // Bu, bu satırın fiş üzerinde nerede (dikeyde) yer aldığını belirtir.Bu Y değeri, yeni gelen kelimenin bu satıra mı ait olduğunu kontrol etmek için kullanılır.
    public float Y => Words.Any()
        ? Words.Average(w => w.BoundingPoly.GetTop())
        : float.MaxValue;

    //Bu satırı ekrana yazdırdığımızda, satırdaki kelimeleri soldan sağa sıralı şekilde tek bir string olarak döndürür.
    public override string ToString()
    {
        var sorted = Words
            .Where(w => w.BoundingPoly?.Vertices?.Any() == true)
            .OrderBy(w => w.BoundingPoly.GetLeft());

        return string.Join(" ", sorted.Select(w => w.Description));
    }
}