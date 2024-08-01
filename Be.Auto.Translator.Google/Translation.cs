namespace Be.Auto.Translator.Google;

public class Translation
{
    public Translation(string translatedText, string originalText)
    {
        TranslatedText = translatedText;
        OriginalText = originalText;
        IsSuccess = !string.IsNullOrEmpty(translatedText);

    }
    public bool IsSuccess { get; private set; }
    public string TranslatedText { get; set; }
    public string OriginalText { get; set; }


}