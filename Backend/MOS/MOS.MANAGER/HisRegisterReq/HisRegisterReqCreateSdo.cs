using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisRegisterReq
{
    class HisRegisterReqCreateSdo : BusinessBase
    {
        internal HisRegisterReqCreateSdo()
            : base()
        {

        }

        internal HisRegisterReqCreateSdo(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisRegisterReqSDO data, ref V_HIS_REGISTER_REQ resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisRegisterReqCheck checker = new HisRegisterReqCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    HIS_REGISTER_REQ req = new HIS_REGISTER_REQ();
                    req.REGISTER_GATE_ID = data.RegisterGateId;
                    if (data.RegisterTime.HasValue)
                    {
                        req.REGISTER_TIME = data.RegisterTime.Value;
                    }

                    if (!new HisRegisterReqCreate(param).Create(req))
                    {
                        return false;
                    }
                    result = true;
                    resultData = new HisRegisterReqGet().GetViewById(req.ID);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
