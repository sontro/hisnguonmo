using MOS.EFMODEL.DataModels;
using System.Collections.Generic;
using System.Reflection;

namespace MOS.EFMODEL.Decorator
{
    public partial class DenyUpdateDecorator
    {
        private static void LoadHisPtttCalendar()
        {
            List<string> pies = new List<string>();
            pies.Add("CREATOR");
            pies.Add("APP_CREATOR");
            pies.Add("CREATE_TIME");
            pies.Add("PTTT_CALENDAR_CODE");
            
            properties[typeof(HIS_PTTT_CALENDAR)] = pies;
        }
    }
}
