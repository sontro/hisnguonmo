using System;
using System.Collections.Generic;

namespace SDA.LibraryEventLog
{
    public class DatabaseEventLog
    {
        private static Dictionary<EventLog.LanguageEnum, Dictionary<EventLog.Enum, EventLog>> dicMultiLanguage = new Dictionary<EventLog.LanguageEnum, Dictionary<EventLog.Enum, EventLog>>();
        private static Dictionary<EventLog.Enum, EventLog> dic = new Dictionary<EventLog.Enum, EventLog>();
        private static Object thisLock = new Object();

        public static EventLog Get(string languageName, EventLog.Enum enumBC)
        {
            EventLog result = null;
            Dictionary<EventLog.Enum, EventLog> dic = null;
            EventLog.LanguageEnum languageEnum = EventLog.GetLanguageEnum(languageName);
            lock (thisLock)
            {
                if (!dicMultiLanguage.TryGetValue(languageEnum, out dic))
                {
                    dic = new Dictionary<EventLog.Enum, EventLog>();
                    result = new EventLog(languageEnum, enumBC);
                    dic.Add(enumBC, result);
                    dicMultiLanguage.Add(languageEnum, dic);
                }
                else
                {
                    if (!dic.TryGetValue(enumBC, out result))
                    {
                        result = new EventLog(languageEnum, enumBC);
                        dic.Add(enumBC, result);
                    }
                }
            }
            
            return result;
        }
    }
}
