using Kaizen.ReceiptParser.Models;
using System.Text.Json;

namespace Kaizen.ReceiptParser.Utils;

/// <summary>
/// JSON dosyasını yükler ve OCRWord listesine deserialize eder.
/// </summary>
public static class JsonLoader
{
    public static List<OCRWord> Load(string path)
    {
        var json = File.ReadAllText(path);
        var words = JsonSerializer.Deserialize<List<OCRWord>>(json);
        return words ?? new();
    }
}
