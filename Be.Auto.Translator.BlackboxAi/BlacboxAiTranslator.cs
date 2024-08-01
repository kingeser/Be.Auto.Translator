using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Be.Auto.Translator.BlackboxAi;

public class BlackboxATranslator(TimeSpan timeout) : IBlacboxAiTranslator
{
    public BlackboxATranslator() : this(TimeSpan.FromSeconds(15))
    {
    }
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

    public async Task<string> TryTranslateAsync(Language sourceLanguage, Language targetLanguage, string textToTranslate)
    {
        var result = await TryTranslateTextAsync(LanguageUtils.GetCode(sourceLanguage), LanguageUtils.GetCode(targetLanguage), textToTranslate);

        return result.IsSuccess ? result.TranslatedText : result.OriginalText;
    }

    public async Task<string> TryTranslateAsync(string sourceLanguage, string targetLanguage, string textToTranslate)
    {
        var result = await TryTranslateTextAsync(sourceLanguage, targetLanguage, textToTranslate);
        return result.IsSuccess ? result.TranslatedText : result.OriginalText;
    }

    public async Task<string> TryTranslateAsync(Language targetLanguage, string textToTranslate)
    {
        var result = await TryTranslateTextAsync(LanguageUtils.GetCode(Language.AutoDetect), LanguageUtils.GetCode(targetLanguage), textToTranslate);
        return result.IsSuccess ? result.TranslatedText : result.OriginalText;
    }

    public async Task<string> TryTranslateAsync(string targetLanguage, string textToTranslate)
    {
        var result = await TryTranslateTextAsync(LanguageUtils.GetCode(Language.AutoDetect), targetLanguage, textToTranslate);
        return result.IsSuccess ? result.TranslatedText : result.OriginalText;
    }

    private  async Task<Translation> TryTranslateTextAsync(string sourceLanguage, string targetLanguage, string textToTranslate)

    {
        try
        {
            var key = GenerateKey();
            using var httpClient = new HttpClient();
            httpClient.Timeout = timeout;
            httpClient.DefaultRequestHeaders.UserAgent.Clear();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36");
            var request = new HttpRequestMessage(HttpMethod.Post, "https://www.blackbox.ai/api/chat");
            request.Headers.Referrer = new Uri("https://www.blackbox.ai/agent/LanguageTranslatorCqX13Ch");
            var requestBody = new Request
            {
                Messages =
                [
                    new Message()
                    {
                        Id = key,
                        Content =$"Requested target language: {targetLanguage}\r\nText to translate: {textToTranslate}",
                        Role = "user"
                    }
                ],
                Id = key,
                PreviewToken = null,
                UserId = null,
                CodeModelMode = true,
                AgentMode = new AgentMode()
                { Mode = true, Id = "LanguageTranslatorCqX13Ch", Name = "Language Translator" },
                TrendingAgentMode = new TrendingAgentMode { },
                IsMicMode = false,
                MaxTokens = 1024,
                IsChromeExt = false,
                GithubToken = null,
                ClickedAnswer2 = false,
                ClickedAnswer3 = false,
                ClickedForceWebSearch = false,
                VisitFromDelta = null
            };

            var json = JsonSerializer.Serialize(requestBody);
           
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.SendAsync(request, CancellationToken.None);

            if (!response.IsSuccessStatusCode) return new Translation(string.Empty, textToTranslate, sourceLanguage, targetLanguage);

            var responseBody = await response.Content.ReadAsStringAsync();

            var jsonResponse = Regex.Match(responseBody, @"\{(?:[^{}]|\{|\})*\}", RegexOptions.Multiline).Value;

            if (string.IsNullOrEmpty(jsonResponse))
            {
                return new Translation(string.Empty, textToTranslate, sourceLanguage, targetLanguage);
            }

            var aiResponse = JsonSerializer.Deserialize<Translation>(jsonResponse);

            if (aiResponse == null) return new Translation(string.Empty, textToTranslate, sourceLanguage, targetLanguage);

            aiResponse.IsSuccess = !string.IsNullOrEmpty(textToTranslate);
            aiResponse.OriginalText = textToTranslate;

            return aiResponse;
        }
        catch
        {
            return new Translation(string.Empty, textToTranslate, sourceLanguage, targetLanguage);
        }

        string GenerateKey()
        {
            const int length = 7;
            const string uppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
            const string numbers = "0123456789";
            const string allCharacters = uppercaseLetters + lowercaseLetters + numbers;
            var random = new Random();
            var keyArray = new char[length];
            for (var i = 0; i < length; i++)
            {
                keyArray[i] = allCharacters[random.Next(allCharacters.Length)];
            }
            return new string(keyArray);
        }
    }


}