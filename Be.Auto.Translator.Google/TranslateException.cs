using System;

namespace Be.Auto.Translator.Google;

public class GoogleTranslateException
    : Exception
{
    
    public GoogleTranslateException(string txt)
        : base(txt)
    {

    }

    
    public GoogleTranslateException(string txt, Exception ex)
        : base(txt, ex)
    {

    }
}