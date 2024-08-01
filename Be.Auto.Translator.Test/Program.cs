using Be.Auto.Translator.Google;

var googleTranslator = new GoogleTranslator();

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

goto START;
