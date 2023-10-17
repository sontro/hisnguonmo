using MOS.EFMODEL.DataModels;
using System.Collections.Generic;
using System.Reflection;

namespace MOS.EFMODEL.Decorator
{
    public partial class DummyDecorator
    {
        private static void LoadHisRationSum()
        {
            List<PropertyInfo> pies = new List<PropertyInfo>();
            pies.Add(typeof(HIS_RATION_SUM).GetProperty("RATION_SUM_CODE"));

            properties[typeof(HIS_RATION_SUM)] = pies;
        }
    }
}
