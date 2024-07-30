using System.Threading.Tasks;

namespace Be.Auto.Translator.Google;

public interface IGoogleTranslator
{
    Task<Translation> TranslateAsync(Language sourceLanguage, Language targetLanguage, string textToTranslate);

}