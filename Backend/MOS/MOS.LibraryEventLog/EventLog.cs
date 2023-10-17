namespace MOS.LibraryEventLog
{
    public partial class EventLog
    {
        public LanguageEnum Language;
        public string message;
        public Enum EnumBC;

        private static string defaultViEventLog = "[].";
        private static string defaultEnEventLog = "[].";

        public EventLog(LanguageEnum language, Enum en)
        {
            Language = language;
            EnumBC = en;
            message = GetEventLog(en);
        }

        public enum LanguageEnum
        {
            VI,
            EN,
        }

        public class LanguageCode
        {
            public const string VI = "VI";
            public const string EN = "EN";
        }

        public static string GetLanguageName(LanguageEnum type)
        {
            string result = LanguageCode.VI;
            switch (type)
            {
                case LanguageEnum.VI:
                    result = LanguageCode.VI;
                    break;
                case LanguageEnum.EN:
                    result = LanguageCode.EN;
                    break;
                default:
                    result = LanguageCode.VI;
                    break;
            }
            return result;
        }

        public static LanguageEnum GetLanguageEnum(string languageName)
        {
            LanguageEnum result = LanguageEnum.VI;
            switch (languageName)
            {
                case LanguageCode.VI:
                    result = LanguageEnum.VI;
                    break;
                case LanguageCode.EN:
                    result = LanguageEnum.EN;
                    break;
                default:
                    result = LanguageEnum.VI;
                    break;
            }
            return result;
        }
    }
}
