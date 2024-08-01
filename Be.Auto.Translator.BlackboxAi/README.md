# BlackboxAi Translator

The `BlackboxAiTranslator` translate text from one language to another using the BlackboxAi.

## Prerequisites

- .NET Core 8
- Microsoft.AspNet.WebApi.Client

## Example Usage

```csharp

using Be.Auto.Translator.BlackboxAi;

var blackboxAiTranslator = new BlackboxAiTranslator(TimeSpan.FromSeconds(30));

START:

Console.WriteLine("Please enter the language code you want to translate.");
Console.WriteLine();
var targetLanguage = Console.ReadLine();

if (string.IsNullOrEmpty(targetLanguage))
    goto START;

TEXT:
Console.WriteLine("Enter the text you want to translate.");
Console.WriteLine();
var text = Console.ReadLine();

if(string.IsNullOrWhiteSpace(text))
    goto TEXT;

var result = await blackboxaiTranslator.TryTranslateAsync(targetLanguage, text);

Console.WriteLine("BlackboxAi Translate Result:");
Console.WriteLine();
Console.WriteLine(result);
Console.WriteLine("----------------------------------------------------");

goto START;


```

## Notes


Enjoy coding :)
