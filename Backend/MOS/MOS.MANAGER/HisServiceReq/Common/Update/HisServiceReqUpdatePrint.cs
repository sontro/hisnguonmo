using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq
{
    partial class HisServiceReqUpdate : BusinessBase
    {
        /// <summary>
        /// Update json_print_id
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool UpdateJsonPrintId(HIS_SERVICE_REQ data, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                HIS_SERVICE_REQ raw = null;
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                if (checker.VerifyId(data.ID, ref raw))
                {
                    raw.JSON_PRINT_ID = data.JSON_PRINT_ID;
                    if (this.Update(raw, true))
                    {
                        resultData = raw;
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
