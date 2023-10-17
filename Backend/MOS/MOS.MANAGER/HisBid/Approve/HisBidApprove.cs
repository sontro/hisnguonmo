using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBid.Approve
{
    partial class HisBidApprove : BusinessBase
    {		
        internal HisBidApprove()
            : base()
        {

        }

        internal HisBidApprove(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(long bidId,ref HIS_BID resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_BID bid = null;
                HisBidApproveCheck checker = new HisBidApproveCheck(param);
                valid = valid && checker.IsUnapprove(bidId, ref bid);
                if (valid)
                {
                    bid.APPROVAL_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                    bid.APPROVAL_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    bid.APPROVAL_TIME = Inventec.Common.DateTime.Get.Now().Value;

                    if (!DAOWorker.HisBidDAO.Update(bid))
                    {
                        throw new Exception("Cap nhat thong tin duyet thau that bai");
                    }
                    resultData = bid;
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisBid_DuyetThau, bid.BID_NAME, bid.BID_NUMBER, bid.APPROVAL_LOGINNAME, bid.APPROVAL_USERNAME).Run();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
