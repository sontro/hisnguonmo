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
        /// Goi benh nhan vao phong
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Call(long id, ref HIS_SERVICE_REQ resultData)
        {
            bool result = false;
            try
            {
                HIS_SERVICE_REQ raw = null;
                HisServiceReqCheck checker = new HisServiceReqCheck(param);
                if (checker.VerifyId(id, ref raw))
                {
                    //Thuc hien tang "call_count" len 1
                    raw.CALL_COUNT = raw.CALL_COUNT.HasValue ? raw.CALL_COUNT.Value + 1 : 1;
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
