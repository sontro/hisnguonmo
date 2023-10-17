namespace MOS.LibraryMessage
{
    public partial class Message
    {
        public LanguageEnum Language;
        public string message;
        public Enum EnumBC;

        private static string defaultViMessage = "[].";
        private static string defaultEnMessage = "[].";

        public Message(LanguageEnum language, Enum en)
        {
            Language = language;
            EnumBC = en;
            message = GetMessage(en);
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
