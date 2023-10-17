using LIS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Desktop.Plugins.SampleInfo
{
    internal class ComparerObject : IEqualityComparer<V_LIS_SAMPLE>
    {
        public bool Equals(V_LIS_SAMPLE x, V_LIS_SAMPLE y)
        {
            return x.ID == y.ID;
        }
        public int GetHashCode(V_LIS_SAMPLE obj)
        {
            return obj.ID.GetHashCode();
        }
    }
}
