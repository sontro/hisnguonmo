using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTreatmentBorrow
{
    class HisTreatmentBorrowReceive : BusinessBase
    {
        internal HisTreatmentBorrowReceive()
            : base()
        {

        }

        internal HisTreatmentBorrowReceive(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_TREATMENT_BORROW data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT_BORROW raw = null;
                HisTreatmentBorrowCheck checker = new HisTreatmentBorrowCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotReceive(raw);
                if (valid)
                {
                    raw.RECEIVE_TIME = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    raw.RECEIVER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    raw.RECEIVER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    if (!DAOWorker.HisTreatmentBorrowDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTreatmentBorrow_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisTreatmentBorrow that bai." + LogUtil.TraceData("data", data));
                    }
                    result = true;
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
