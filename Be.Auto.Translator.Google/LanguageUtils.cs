using System.Reflection;

namespace Be.Auto.Translator.Google;

internal static class LanguageUtils
{
    internal static string GetCode(Language value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<LanguageCodeAttribute>();
        return attribute == null ? value.ToString() : attribute.Code;
    }
}