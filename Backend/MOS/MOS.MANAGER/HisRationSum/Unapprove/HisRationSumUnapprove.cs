using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisRationSum.Unapprove
{
    class HisRationSumUnapprove : BusinessBase
    {
        private HisRationSumUpdate hisRationSumUpdate;

        internal HisRationSumUnapprove()
            : base()
        {
            this.hisRationSumUpdate = new HisRationSumUpdate(param);
        }

        internal HisRationSumUnapprove(CommonParam param)
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
                valid = valid && checker.IsSttAllowUnapprove(raw);
                valid = valid && checker.HasWorkPlaceInfo(data.WorkingRoomId, ref workplaceSDO);
                valid = valid && checker.IsWorkingAtRoom(raw.ROOM_ID, data.WorkingRoomId);
                if (valid)
                {
                    Mapper.CreateMap<HIS_RATION_SUM, HIS_RATION_SUM>();
                    HIS_RATION_SUM before = Mapper.Map<HIS_RATION_SUM>(raw);
                    raw.RATION_SUM_STT_ID = IMSys.DbConfig.HIS_RS.HIS_RATION_SUM_STT.ID__REQ;
                    raw.APPROVAL_LOGINNAME = null;
                    raw.APPROVAL_USERNAME = null;
                    raw.APPROVAL_TIME = null;
                    raw.APPROVAL_DATE = null;
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
