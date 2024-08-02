using System.Threading.Tasks;

namespace Be.Auto.Translator;

public abstract class Translator : ITranslator
{
    public abstract Task<Translation> TranslateAsync(Language sourceLanguage, Language targetLanguage, string textToTranslate);
    public abstract Task<Translation> TranslateAsync(string sourceLanguage, string targetLanguage, string textToTranslate);
    public abstract Task<Translation> TranslateAsync(Language targetLanguage, string textToTranslate);
    public abstract Task<Translation> TranslateAsync(string targetLanguage, string textToTranslate);
    public abstract Task<string> TryTranslateAsync(Language sourceLanguage, Language targetLanguage, string textToTranslate);
    public abstract Task<string> TryTranslateAsync(string sourceLanguage, string targetLanguage, string textToTranslate);
    public abstract Task<string> TryTranslateAsync(Language targetLanguage, string textToTranslate);
    public abstract Task<string> TryTranslateAsync(string targetLanguage, string textToTranslate);


}