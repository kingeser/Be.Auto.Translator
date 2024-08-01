using System.Threading.Tasks;

namespace Be.Auto.Translator.Google;

public interface IGoogleTranslator
{
    Task<Translation> TranslateAsync(Language sourceLanguage, Language targetLanguage, string textToTranslate);
    Task<Translation> TranslateAsync(string sourceLanguage, string targetLanguage, string textToTranslate);
    Task<Translation> TranslateAsync(Language targetLanguage, string textToTranslate);
    Task<Translation> TranslateAsync(string targetLanguage, string textToTranslate);
}