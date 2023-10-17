using MOS.EFMODEL.DataModels;
using System.Collections.Generic;
using System.Reflection;

namespace MOS.EFMODEL.Decorator
{
    public partial class DummyDecorator
    {
        private static void LoadHisPrepare()
        {
            List<PropertyInfo> pies = new List<PropertyInfo>();
            pies.Add(typeof(HIS_PREPARE).GetProperty("PREPARE_CODE"));

            properties[typeof(HIS_PREPARE)] = pies;
        }
    }
}
