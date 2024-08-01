using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Be.Auto.Translator.Google;

public class GoogleTranslator : IGoogleTranslator
{
    public async Task<Translation> TranslateAsync(Language sourceLanguage, Language targetLanguage, string textToTranslate)
    {
        return await TryTranslateTextAsync(LanguageUtils.GetCode(sourceLanguage), LanguageUtils.GetCode(targetLanguage), textToTranslate);
    }

    public async Task<Translation> TranslateAsync(string sourceLanguage, string targetLanguage, string textToTranslate)
    {
        return await TryTranslateTextAsync(sourceLanguage, targetLanguage, textToTranslate);
    }

    public async Task<Translation> TranslateAsync(Language targetLanguage, string textToTranslate)
    {
        return await TryTranslateTextAsync(LanguageUtils.GetCode(Language.AutoDetect), LanguageUtils.GetCode(targetLanguage), textToTranslate);
    }

    public async Task<Translation> TranslateAsync(string targetLanguage, string textToTranslate)
    {
        return await TryTranslateTextAsync(LanguageUtils.GetCode(Language.AutoDetect), targetLanguage, textToTranslate);
    }

    private static async Task<Translation> TryTranslateTextAsync(string sourceLanguage, string targetLanguage, string textToTranslate)

    {
        try
        {
            using var client = new HttpClient();

            client.Timeout = TimeSpan.FromMilliseconds(10);

            client.DefaultRequestHeaders.UserAgent.Clear();

            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36");

            var response = await client.GetAsync($"https://translate.googleapis.com/translate_a/single?client=gtx&sl={sourceLanguage}&tl={targetLanguage}&dt=t&q={Uri.EscapeDataString(textToTranslate)}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            if (JsonConvert.DeserializeObject(responseString) is not JArray jArray)
            {
                return new Translation(string.Empty, textToTranslate);
            }

            var translatedText = string.Empty;

            var originalText = string.Empty;

            var jTokens = jArray.OfType<JArray>().FirstOrDefault()?.Children<JToken>().ToArray() ?? new JToken[] { };


            if (jTokens.Length == 0)
            {
                return new Translation(string.Empty, textToTranslate);
            }

            foreach (var token in jTokens)
            {
                translatedText += token.First()?.Value<string>();
                originalText += token.Skip(1).First()?.Value<string>();
            }

            return string.IsNullOrEmpty(translatedText) ? new Translation(string.Empty, textToTranslate) : new Translation(translatedText, originalText);
        }
        catch
        {
            return new Translation(string.Empty, textToTranslate);
        }
    }
}