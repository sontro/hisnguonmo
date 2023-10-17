using MOS.EFMODEL.DataModels;
using System.Collections.Generic;
using System.Reflection;

namespace MOS.EFMODEL.Decorator
{
    public partial class DummyDecorator
    {
        private static void LoadHisVitaminA()
        {
            List<PropertyInfo> pies = new List<PropertyInfo>();
            pies.Add(typeof(HIS_VITAMIN_A).GetProperty("VITAMIN_A_CODE"));

            properties[typeof(HIS_VITAMIN_A)] = pies;
        }
    }
}
