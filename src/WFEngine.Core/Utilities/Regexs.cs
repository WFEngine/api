namespace WFEngine.Core.Utilities
{
    public static class Regexs
    {
        public static string PhoneNumberRegex => @"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}";
        public static string UrlRegex => @"(https?:\/\/)?([\w\-])+\.{1}([a-zA-Z]{2,63})([\/\w-]*)*\/?\??([^#\n\r]*)?#?([^\n\r]*)";
    }
}
