using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    class AggrImpMestData
    {
        public string AggrImpMestCode { get; set; }

        public List<string> ImpMestCodes { get; set; }

        public AggrImpMestData()
        {
        }

        public AggrImpMestData(string aggrImpCode, List<string> expMestCodes)
        {
            this.AggrImpMestCode = aggrImpCode;
            this.ImpMestCodes = expMestCodes;
        }

        public override string ToString()
        {
            string children = "";

            if (this.ImpMestCodes != null && this.ImpMestCodes.Count > 0)
            {
                foreach (string s in this.ImpMestCodes)
                {
                    string tmp = string.Format("{0}: {1}", SimpleEventKey.IMP_MEST_CODE, s);
                    children += tmp + "; ";
                }
            }

            string aggrExp = string.IsNullOrWhiteSpace(this.AggrImpMestCode) ?
                "" : string.Format("{0}: {1}", SimpleEventKey.IMP_MEST_CODE, this.AggrImpMestCode);
            return string.Format("{0} ({1})", aggrExp, children);
        }
    }
}
