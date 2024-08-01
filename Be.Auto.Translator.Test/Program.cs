using Be.Auto.Translator.BlackboxAi;
using Be.Auto.Translator.Google;

var googleTranslator = new GoogleTranslator();
var blackboxaiTranslator = new BlackboxATranslator();

START:

Console.WriteLine("Please enter the language code you want to translate.");
var targetLanguage = Console.ReadLine();

if (string.IsNullOrEmpty(targetLanguage))
    goto START;

TEXT:
Console.WriteLine("Enter the text you want to translate.");
var text = Console.ReadLine();

if(string.IsNullOrWhiteSpace(text))
    goto TEXT;

var googleTranslatorResult = await googleTranslator.TryTranslateAsync(targetLanguage, text);

Console.WriteLine("Google Translate Result:");
Console.WriteLine(googleTranslatorResult);

var blackboxaiTranslatorTranslatorResult = await blackboxaiTranslator.TryTranslateAsync(targetLanguage, text);

Console.WriteLine("BlackboxAi Translate Result:");
Console.WriteLine(blackboxaiTranslatorTranslatorResult);

goto START;
