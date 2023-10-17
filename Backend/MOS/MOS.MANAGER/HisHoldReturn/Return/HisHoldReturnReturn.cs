using AutoMapper;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisHoldReturn.Return
{
    class HisHoldReturnReturn : BusinessBase
    {
        private HisHoldReturnUpdate hisHoldReturnUpdate;

        internal HisHoldReturnReturn(CommonParam param)
            : base(param)
        {
            this.hisHoldReturnUpdate = new HisHoldReturnUpdate(param);
        }

        internal bool Run(HisHoldReturnSDO data, ref HIS_HOLD_RETURN resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_HOLD_RETURN raw = null;
                HIS_TREATMENT treatment = null;
                WorkPlaceSDO workplace = null;
                HisHoldReturnCheck checker = new HisHoldReturnCheck(param);
                HisTreatmentCheck treatChecker = new HisTreatmentCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && checker.VerifyId(data.HoldReturnId, ref raw);
                valid = valid && checker.IsNotHandovering(raw);
                valid = valid && checker.IsNotReturn(raw);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workplace);
                valid = valid && checker.VerifyResponsibleRoom(raw, workplace.RoomId);
                valid = valid && raw.TREATMENT_ID.HasValue && treatChecker.VerifyId(raw.TREATMENT_ID.Value, ref treatment);
                valid = valid && checker.IsLocked(treatment);
                if (valid)
                {
                    Mapper.CreateMap<HIS_HOLD_RETURN, HIS_HOLD_RETURN>();
                    HIS_HOLD_RETURN before = Mapper.Map<HIS_HOLD_RETURN>(raw);
                    raw.RETURN_TIME = Inventec.Common.DateTime.Get.Now().Value;
                    raw.RETURN_ROOM_ID = workplace.RoomId;
                    raw.RETURN_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    raw.RETURN_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();

                    if (!this.hisHoldReturnUpdate.Update(raw, before))
                    {
                        throw new Exception("hisHoldReturnUpdate. Ket thuc nghiep vu");
                    }
                    result = true;
                    resultData = raw;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
