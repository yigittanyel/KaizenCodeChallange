using System.Text.Json.Serialization;

namespace Kaizen.ReceiptParser.Models;

/// <summary>
/// OCR'den gelen dörtgen koordinatları
/// </summary>
public class BoundingBox
{
    [JsonPropertyName("vertices")]
    public List<Vertex> Vertices { get; set; } // vertex ile beraber x,y koordinatlarını tutuyoruz burada.

    public float GetTop()
    {
        return Vertices?.Any() == true
            ? Vertices.Min(v => v.Y) // listedeki en küçük Y değeri, yani en yukarıdaki nokta
            : float.MaxValue;
    }

    public float GetLeft()
    {
        return Vertices?.Any() == true
            ? Vertices.Min(v => v.X)
            : float.MaxValue;
    }

    // Elimizde hiç Vertices yoksa, yani kutunun yeri bilinmiyor → O zaman bu kelimenin satır gruplamasına dahil olmasını istemiyoruz. İşte bu yüzden çok büyük bir değer döndürüyoruz. Ve bu sayı hiçbir zaman LineThreshold'dan küçük olmayacak. Yani: Bu satır(veya kelime) asla eşleşmeyecek → güvenli şekilde yok sayılmış olacak
}