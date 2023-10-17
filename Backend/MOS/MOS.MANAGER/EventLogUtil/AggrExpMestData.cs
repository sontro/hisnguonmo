using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    class AggrExpMestData
    {
        public string AggrExpMestCode { get; set; }

        public List<string> ExpMestCodes { get; set; }

        public AggrExpMestData()
        {
        }

        public AggrExpMestData(string aggrExpCode, List<string> expMestCodes)
        {
            this.AggrExpMestCode = aggrExpCode;
            this.ExpMestCodes = expMestCodes;
        }

        public override string ToString()
        {
            string children = "";

            if (this.ExpMestCodes != null && this.ExpMestCodes.Count > 0)
            {
                foreach (string s in this.ExpMestCodes)
                {
                    string tmp = string.Format("{0}: {1}", SimpleEventKey.EXP_MEST_CODE, s);
                    children += tmp + "; ";
                }
            }

            string aggrExp = string.IsNullOrWhiteSpace(this.AggrExpMestCode) ?
                "" : string.Format("{0}: {1}", SimpleEventKey.EXP_MEST_CODE, this.AggrExpMestCode);
            return string.Format("{0} ({1})", aggrExp, children);
        }
    }
}
