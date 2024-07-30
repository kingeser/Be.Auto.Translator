# GoogleTranslator

The `GoogleTranslator` translate text from one language to another using the Google Translate API.

## Prerequisites

- .NET Core 8
- Newtonsoft.Json library

## Example Usage

```csharp
 var translator = new GoogleTranslator();
 var result = await translator.TranslateAsync(Language.Turkish, Language.English, text);
```
