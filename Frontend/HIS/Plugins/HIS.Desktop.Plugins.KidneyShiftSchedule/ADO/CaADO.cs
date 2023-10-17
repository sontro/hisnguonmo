using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.KidneyShiftSchedule.ADO
{
    internal class CaADO
    {
        internal CaADO()
        {
        }
        public long Value { get; set; }

        internal List<CaADO> CaADOs
        {
            get
            {
                List<CaADO> rs = new List<CaADO>();
                rs.Add(new CaADO() { Value = 1 });
                rs.Add(new CaADO() { Value = 2 });
                rs.Add(new CaADO() { Value = 3 });
                rs.Add(new CaADO() { Value = 4 });
                rs.Add(new CaADO() { Value = 5 });
                return rs;
            }
        }

    }
}
