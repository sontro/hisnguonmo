using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMediRecordBorrow
{
    class HisMediRecordBorrowReceive : BusinessBase
    {
        internal HisMediRecordBorrowReceive()
            : base()
        {

        }

        internal HisMediRecordBorrowReceive(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_MEDI_RECORD_BORROW data, ref HIS_MEDI_RECORD_BORROW resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_MEDI_RECORD_BORROW raw = null;
                HisMediRecordBorrowCheck checker = new HisMediRecordBorrowCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotReceive(raw);
                if (valid)
                {
                    raw.RECEIVE_TIME = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmss"));
                    raw.RECEIVE_DATE = raw.RECEIVE_TIME - (raw.RECEIVE_TIME % 1000000);
                    raw.RECEIVER_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    raw.RECEIVER_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    if (!DAOWorker.HisMediRecordBorrowDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediRecordBorrow_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMediRecordBorrow that bai." + LogUtil.TraceData("data", data));
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
