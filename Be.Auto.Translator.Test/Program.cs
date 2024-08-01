using Be.Auto.Translator.BlackboxAi;
using Be.Auto.Translator.Google;

var googleTranslator = new GoogleTranslator(TimeSpan.FromSeconds(30));
var blackboxaiTranslator = new BlackboxAiTranslator(TimeSpan.FromSeconds(30));

START:


Console.WriteLine("Please enter the language code you want to translate.");
Console.WriteLine();
var targetLanguage = Console.ReadLine();
Console.WriteLine("----------------------------------------------------");
if (string.IsNullOrEmpty(targetLanguage))
    goto START;

TEXT:
Console.WriteLine("Enter the text you want to translate.");
Console.WriteLine();
var text = Console.ReadLine();
Console.WriteLine("----------------------------------------------------");
if (string.IsNullOrWhiteSpace(text))
    goto TEXT;

var googleTranslatorResult = await googleTranslator.TryTranslateAsync(targetLanguage, text);

Console.WriteLine("Google Translate Result:");
Console.WriteLine();
Console.WriteLine(googleTranslatorResult);
Console.WriteLine("----------------------------------------------------");

var blackboxaiTranslatorTranslatorResult = await blackboxaiTranslator.TryTranslateAsync(targetLanguage, text);

Console.WriteLine("BlackboxAi Translate Result:");
Console.WriteLine();
Console.WriteLine(blackboxaiTranslatorTranslatorResult);
Console.WriteLine("----------------------------------------------------");

goto START;
