using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;

namespace MOS.MANAGER.HisMestPeriodMety
{
    class HisMestPeriodMetyCheck : BusinessBase
    {
        internal HisMestPeriodMetyCheck()
            : base()
        {

        }

        internal HisMestPeriodMetyCheck(CommonParam paramCheck)
            : base(paramCheck) 
        {

        }

        internal bool VerifyRequireField(HIS_MEST_PERIOD_METY data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.MEDI_STOCK_PERIOD_ID)) throw new ArgumentNullException("data.MEDI_STOCK_PERIOD_ID");
                if (!IsGreaterThanZero(data.MEDICINE_TYPE_ID)) throw new ArgumentNullException("data.MEDICINE_TYPE_ID");
                if (data.BEGIN_AMOUNT < 0) throw new ArgumentNullException("data.BEGIN_AMOUNT < 0");
                if (data.IN_AMOUNT < 0) throw new ArgumentNullException("data.IN_AMOUNT < 0");
                if (data.OUT_AMOUNT < 0) throw new ArgumentNullException("data.OUT_AMOUNT < 0");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        internal bool IsUnLock(HIS_MEST_PERIOD_METY data)
        {
            bool valid = true;
            try
            {
                if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE)
                {
                    valid = false;
                    MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.DuLieuDangBiKhoa);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsUnLock(long id)
        {
            bool valid = true;
            try
            {
                if (!DAOWorker.HisMestPeriodMetyDAO.IsUnLock(id))
                {
                    valid = false;
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDangBiKhoa);
                }
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
