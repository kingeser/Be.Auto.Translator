using System;

namespace Be.Auto.Translator.Google;

[AttributeUsage(AttributeTargets.Field)]
internal class LanguageCodeAttribute : Attribute
{
    public LanguageCodeAttribute(string code)
    {
        Code = code;
    }
    public string Code { get; set; }
}