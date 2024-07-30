# GoogleTranslator

The `GoogleTranslator` translate text from one language to another using the Google Translate API.

## Prerequisites

- .NET Core 8
- Microsoft.AspNet.WebApi.Client

## Example Usage

```csharp
 var translator = new GoogleTranslator();
 var result = await translator.TranslateAsync(Language.Turkish, Language.English, text);
```

## Notes
Ensure that you handle exceptions properly when using the TranslateAsync method, as it can throw GoogleTranslateException for various errors.

The Translation class is assumed to have two properties: TranslatedText and OriginalText.