namespace Kaizen.CampaignCodeGenerator.Models;

/// <summary>
/// Geçerli karakter kümesi ve kod yapısı ayarlarını içerir.
/// </summary>
public static class CodeConfig
{
    public static readonly char[] Alphabet = "ACDEFGHKLMNPRTXYZ234579".ToCharArray();
    public static readonly int CodeLength = 8; // üretim kodunun uzunluğu 8 karakter
    public static readonly int DataPartLength = 7; // Üretilen 8 karakterlik kampanya kodunun ilk 7 karakteri, veri (yani rastgele üretilen kısmıdır) 8. karakter ise bu ilk 7'ye dayalı checksum (doğrulama karakteridir). bu kısmı github'da md file kısmında da açıkladım. 
}