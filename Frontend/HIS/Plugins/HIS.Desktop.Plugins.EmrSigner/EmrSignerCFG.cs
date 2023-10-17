using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EmrSigner
{
    class EmrSignerCFG
    {
        public enum EmrHsmIntegrateOption
        {
            USING_BKAV = 1,
            USING_VIETRAD = 2,
            USING_VIETSENS = 3,
            USING_EasySign = 4,
            USING_eSignCloud = 5
        }
    }
}
