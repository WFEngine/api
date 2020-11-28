using System;

namespace WFEngine.Core.Attributes
{
    public class LanguageCodeAttribute : Attribute
    {
        public string LanguageCode { get; set; }

        public LanguageCodeAttribute(string LanguageCode)
        {
            this.LanguageCode = LanguageCode;
        }
    }
}
