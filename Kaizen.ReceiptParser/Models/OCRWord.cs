using System.Text.Json.Serialization;

namespace Kaizen.ReceiptParser.Models;

/// <summary>
/// OCR tarafından yakalanan tek bir kelime ve onun koordinat kutusunu
/// </summary>
public class OCRWord
{
    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("boundingPoly")]
    public BoundingBox BoundingPoly { get; set; }
}