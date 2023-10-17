using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisHoldReturn;
using MOS.MANAGER.HisHoreHoha;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisHoreHandover.Update
{
    class HisHoreHandoverUpdateCheck : BusinessBase
    {
        internal HisHoreHandoverUpdateCheck()
            : base()
        {

        }

        internal HisHoreHandoverUpdateCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HisHoreHandoverCreateSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if ((data.Id ?? 0) <= 0) throw new ArgumentNullException("data.Id");
                if (data.WorkingRoomId <= 0) throw new ArgumentNullException("data.WorkingRoomId");
                if (data.ReceiveRoomId <= 0) throw new ArgumentNullException("data.ReceiveRoomId");
                if (!IsNotNullOrEmpty(data.HisHoldReturnIds)) throw new ArgumentNullException("data.HisHoldReturnIds");
                if (data.ReceiveRoomId == data.WorkingRoomId) throw new ArgumentNullException("data.ReceiveRoomId == data.WorkingRoomId");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }


        internal bool CheckNotHandovering(HIS_HORE_HANDOVER handover, List<HIS_HOLD_RETURN> holdReturns, ref List<HIS_HORE_HOHA> horeHohas)
        {
            bool valid = true;
            try
            {
                List<HIS_HORE_HOHA> listHoreHoha = new HisHoreHohaGet().GetByHoreHandoverId(handover.ID);
                List<HIS_HOLD_RETURN> checkDatas = holdReturns != null ? holdReturns.Where(o => listHoreHoha == null || !listHoreHoha.Any(a => a.HOLD_RETURN_ID == o.ID)).ToList() : null;
                if (IsNotNullOrEmpty(checkDatas))
                {
                    valid = new HisHoldReturnCheck(param).IsNotHandovering(checkDatas);
                }
                horeHohas = listHoreHoha;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool CheckWorkingRoom(HIS_HORE_HANDOVER handover, long workingRoomId)
        {
            bool valid = true;
            try
            {
                if (handover.SEND_ROOM_ID != workingRoomId)
                {
                    V_HIS_ROOM room = HisRoomCFG.DATA.FirstOrDefault(o => o.ID == handover.SEND_ROOM_ID);
                    string name = room != null ? room.ROOM_NAME : "";
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.BanDangKhongLamViecTaiPhong, name);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
