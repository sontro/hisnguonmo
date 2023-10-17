using MOS.EFMODEL.DataModels;
using System.Collections.Generic;
using System.Reflection;

namespace MOS.EFMODEL.Decorator
{
    public partial class DummyDecorator
    {
        private static void LoadHisVaccination()
        {
            List<PropertyInfo> pies = new List<PropertyInfo>();
            pies.Add(typeof(HIS_VACCINATION).GetProperty("VACCINATION_CODE"));

            properties[typeof(HIS_VACCINATION)] = pies;
        }
    }
}
