using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Optometrist
{
    class HisServiceReqOptometristProcessCheck : BusinessBase
    {
        internal HisServiceReqOptometristProcessCheck()
            : base()
        {

        }

        internal HisServiceReqOptometristProcessCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool IsValidData(HIS_SERVICE_REQ serviceReq)
        {
            bool valid = true;
            try
            {
                
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
