using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisHoldReturn;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisHoreHandover.Create
{
    class HisHoreHandoverCreateCheck : BusinessBase
    {
        internal HisHoreHandoverCreateCheck()
            : base()
        {

        }

        internal HisHoreHandoverCreateCheck(CommonParam paramCheck)
            : base(paramCheck)
        {

        }

        internal bool VerifyRequireField(HisHoreHandoverCreateSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
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

    }
}
