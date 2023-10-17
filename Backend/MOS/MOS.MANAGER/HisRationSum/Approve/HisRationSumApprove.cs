﻿using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisRationSum.Approve
{
    class HisRationSumApprove : BusinessBase
    {
        private HisRationSumUpdate hisRationSumUpdate;

        internal HisRationSumApprove()
            : base()
        {
            this.hisRationSumUpdate = new HisRationSumUpdate(param);
        }

        internal HisRationSumApprove(CommonParam param)
            : base(param)
        {
            this.hisRationSumUpdate = new HisRationSumUpdate(param);
        }

        internal bool Run(HisRationSumUpdateSDO data, ref HIS_RATION_SUM resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_RATION_SUM raw = null;
                WorkPlaceSDO workplaceSDO = null;
                HisRationSumCheck checker = new HisRationSumCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.RationSumId, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsSttAllowApproveOrReject(raw);
                valid = valid && checker.HasWorkPlaceInfo(data.WorkingRoomId, ref workplaceSDO);
                valid = valid && checker.IsWorkingAtRoom(raw.ROOM_ID, data.WorkingRoomId);
                if (valid)
                {
                    Mapper.CreateMap<HIS_RATION_SUM, HIS_RATION_SUM>();
                    HIS_RATION_SUM before = Mapper.Map<HIS_RATION_SUM>(raw);
                    raw.RATION_SUM_STT_ID = IMSys.DbConfig.HIS_RS.HIS_RATION_SUM_STT.ID__APPROVAL;
                    raw.APPROVAL_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    raw.APPROVAL_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    raw.APPROVAL_TIME = Inventec.Common.DateTime.Get.Now().Value;
                    raw.APPROVAL_DATE = raw.APPROVAL_TIME - (raw.APPROVAL_TIME % 1000000);
                    if (!this.hisRationSumUpdate.Update(raw, before))
                    {
                        throw new Exception("hisRationSumUpdate. Ket thuc nghiep vu");
                    }
                    resultData = raw;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
                this.Rollback();
            }
            return result;
        }

        private void Rollback()
        {
            try
            {
                this.hisRationSumUpdate.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
