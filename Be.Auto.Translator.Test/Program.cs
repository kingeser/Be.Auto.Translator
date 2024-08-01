using Be.Auto.Translator.Google;

var googleTranslator = new GoogleTranslator();

var googleTranslateResult = await googleTranslator.TranslateAsync(Language.Turkish, "Hello, World!");


Console.ReadLine();