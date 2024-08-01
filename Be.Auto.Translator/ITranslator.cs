using System.Threading.Tasks;

namespace Be.Auto.Translator;

public interface ITranslator
{
    Task<Translation> TranslateAsync(Language sourceLanguage, Language targetLanguage, string textToTranslate);
    Task<Translation> TranslateAsync(string sourceLanguage, string targetLanguage, string textToTranslate);
    Task<Translation> TranslateAsync(Language targetLanguage, string textToTranslate);
    Task<Translation> TranslateAsync(string targetLanguage, string textToTranslate);
    Task<string> TryTranslateAsync(Language sourceLanguage, Language targetLanguage, string textToTranslate);
    Task<string> TryTranslateAsync(string sourceLanguage, string targetLanguage, string textToTranslate);
    Task<string> TryTranslateAsync(Language targetLanguage, string textToTranslate);
    Task<string> TryTranslateAsync(string targetLanguage, string textToTranslate);
}