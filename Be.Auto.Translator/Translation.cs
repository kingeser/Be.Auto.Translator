
using System.Text.Json.Serialization;

namespace Be.Auto.Translator;

public class Translation
{
    public Translation(string translatedText, string originalText, string sourceLanguage, string targetlanguage)
    {
        TranslatedText = translatedText;
        OriginalText = originalText;
        SourceLanguage = sourceLanguage;
        TargetLanguage = targetlanguage;
        IsSuccess = !string.IsNullOrEmpty(translatedText) && !string.Equals(originalText, translatedText);

    }

    [JsonPropertyName("IsSuccess")]
    public bool IsSuccess { get; set; }
    [JsonPropertyName("SourceLanguage")]
    public string SourceLanguage { get; set; }
    [JsonPropertyName("TargetLanguage")]
    public string TargetLanguage { get; set; }
    [JsonPropertyName("OriginalText")]
    public string OriginalText { get; set; }
    [JsonPropertyName("TranslatedText")]
    public string TranslatedText { get; set; }


}