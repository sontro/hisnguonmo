using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisCarerCardBorrow
{
    class HisCarerCardBorrowGiveBackCheck : BusinessBase
    {
        internal HisCarerCardBorrowGiveBackCheck()
            : base()
        {
        }

        internal HisCarerCardBorrowGiveBackCheck(CommonParam paramCheck)
            : base(paramCheck)
        {
        }

        internal bool VerifyRequireField(HisCarerCardBorrowGiveBackSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.CarerCardBorrowId)) throw new ArgumentNullException("data.CarerCardBorrowId");
                if (string.IsNullOrWhiteSpace(data.ReceivingLoginName)) throw new ArgumentNullException("data.ReceivingLoginName");
                if (string.IsNullOrWhiteSpace(data.ReceivingUserName)) throw new ArgumentNullException("data.ReceivingUserName");
                if (!IsGreaterThanZero(data.GiveBackTime)) throw new ArgumentNullException("data.GiveBackTime");
                if (!IsGreaterThanZero(data.RequestRoomId)) throw new ArgumentNullException("data.RequestRoomId");
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

