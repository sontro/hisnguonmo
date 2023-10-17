using MOS.EFMODEL.DataModels;
using System.Collections.Generic;
using System.Reflection;

namespace MOS.EFMODEL.Decorator
{
    public partial class DummyDecorator
    {
        private static void LoadHisImpMestPropose()
        {
            List<PropertyInfo> pies = new List<PropertyInfo>();
            pies.Add(typeof(HIS_IMP_MEST_PROPOSE).GetProperty("HIS_IMP_MEST_PROPOSE_CODE"));

            properties[typeof(HIS_IMP_MEST_PROPOSE)] = pies;
        }
    }
}
