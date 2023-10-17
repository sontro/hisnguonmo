using MOS.EFMODEL.DataModels;
using System.Collections.Generic;
using System.Reflection;

namespace MOS.EFMODEL.Decorator
{
    public partial class DummyDecorator
    {
        private static void LoadHisPtttCalendar()
        {
            List<PropertyInfo> pies = new List<PropertyInfo>();
            pies.Add(typeof(HIS_PTTT_CALENDAR).GetProperty("PTTT_CALENDAR_CODE"));

            properties[typeof(HIS_PTTT_CALENDAR)] = pies;
        }
    }
}
