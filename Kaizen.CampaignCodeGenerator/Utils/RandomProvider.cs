namespace Kaizen.CampaignCodeGenerator.Utils;

/// <summary>
/// Singleton Random instance to avoid duplicate values
/// when generating codes quickly in succession.
/// </summary>
public static class RandomProvider
{
    public static readonly Random Instance = new Random();
}