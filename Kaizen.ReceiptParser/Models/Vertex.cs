using System.Text.Json.Serialization;

namespace Kaizen.ReceiptParser.Models;

/// <summary>
/// (x, y) koordinatları
/// </summary>
public class Vertex
{
    [JsonPropertyName("x")]
    public int X { get; set; }

    [JsonPropertyName("y")]
    public int Y { get; set; }
}
