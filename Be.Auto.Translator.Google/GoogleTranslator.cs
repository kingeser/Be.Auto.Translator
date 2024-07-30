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

        try
        {
            using var client = new HttpClient();

            client.Timeout = TimeSpan.FromMilliseconds(2000);

            client.DefaultRequestHeaders.UserAgent.Clear();

            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36");

            var response = await client.GetAsync($"https://translate.googleapis.com/translate_a/single?client=gtx&sl={LanguageUtils.GetCode(sourceLanguage)}&tl={LanguageUtils.GetCode(targetLanguage)}&dt=t&q={Uri.EscapeDataString(textToTranslate)}");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            if (JsonConvert.DeserializeObject(responseString) is not JArray jArray)
            {
                throw new GoogleTranslateException("Wrong translate response!");
            }

            var translatedText = string.Empty;

            var originalText = string.Empty;

            var jTokens = jArray.OfType<JArray>().FirstOrDefault()?.Children<JToken>().ToArray() ?? new JToken[] { };


            if (jTokens.Length == 0)
            {
                throw new GoogleTranslateException("Wrong translate response!");
            }

            foreach (var token in jTokens)
            {
                translatedText += token.First()?.Value<string>();
                originalText += token.Skip(1).First()?.Value<string>();
            }

            if (string.IsNullOrEmpty(translatedText))
            {
                throw new GoogleTranslateException("Cannot translated!");
            }

            return new Translation(translatedText, originalText);

        }
        catch (Exception e)
        {
            throw new GoogleTranslateException(e.Message, e);
        }
    }


}