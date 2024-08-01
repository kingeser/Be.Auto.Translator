using System.Reflection;

namespace Be.Auto.Translator;

public static class LanguageUtils
{
    public static string GetCode(Language value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<LanguageCodeAttribute>();
        return attribute == null ? value.ToString() : attribute.Code;
    }
}