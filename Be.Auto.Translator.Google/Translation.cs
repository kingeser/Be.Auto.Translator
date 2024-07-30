namespace Be.Auto.Translator.Google;

public class Translation
{
    public Translation(string translatedText, string originalText)
    {
        TranslatedText = translatedText;
        OriginalText = originalText;

    }
    public string TranslatedText { get; set; }
    public string OriginalText { get; set; }


}