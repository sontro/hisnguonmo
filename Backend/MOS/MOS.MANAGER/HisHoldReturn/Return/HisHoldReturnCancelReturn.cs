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

namespace MOS.MANAGER.HisHoldReturn.Return
{
    class HisHoldReturnCancelReturn : BusinessBase
    {
        private HisHoldReturnUpdate hisHoldReturnUpdate;

        internal HisHoldReturnCancelReturn(CommonParam param)
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
                WorkPlaceSDO workplace = null;
                HisHoldReturnCheck checker = new HisHoldReturnCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && checker.VerifyId(data.HoldReturnId, ref raw);
                valid = valid && checker.IsReturn(raw);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workplace);
                valid = valid && checker.VerifyResponsibleRoom(raw, workplace.RoomId);
                if (valid)
                {
                    Mapper.CreateMap<HIS_HOLD_RETURN, HIS_HOLD_RETURN>();
                    HIS_HOLD_RETURN before = Mapper.Map<HIS_HOLD_RETURN>(raw);
                    raw.RETURN_TIME = null;
                    raw.RETURN_ROOM_ID = null;
                    raw.RETURN_LOGINNAME = null;
                    raw.RETURN_LOGINNAME = null;

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
