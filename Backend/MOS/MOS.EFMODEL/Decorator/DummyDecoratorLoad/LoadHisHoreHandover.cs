using MOS.EFMODEL.DataModels;
using System.Collections.Generic;
using System.Reflection;

namespace MOS.EFMODEL.Decorator
{
    public partial class DummyDecorator
    {
        private static void LoadHisHoreHandover()
        {
            List<PropertyInfo> pies = new List<PropertyInfo>();
            pies.Add(typeof(HIS_HORE_HANDOVER).GetProperty("HORE_HANDOVER_CODE"));

            properties[typeof(HIS_HORE_HANDOVER)] = pies;
        }
    }
}
