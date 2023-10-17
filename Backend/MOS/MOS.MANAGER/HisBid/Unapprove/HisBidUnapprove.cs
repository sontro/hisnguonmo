using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBid.Unapprove
{
    partial class HisBidUnapprove : BusinessBase
    {
        internal HisBidUnapprove()
            : base()
        {

        }

        internal HisBidUnapprove(CommonParam param)
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
                HisBidUnapproveCheck checker = new HisBidUnapproveCheck(param);
                valid = valid && checker.IsApprove(bidId, ref bid);
                if (valid)
                {
                    bid.APPROVAL_USERNAME = null;
                    bid.APPROVAL_LOGINNAME = null;
                    bid.APPROVAL_TIME = null;

                    if (!DAOWorker.HisBidDAO.Update(bid))
                    {
                        throw new Exception("Cap nhat thong tin duyet thau that bai");
                    }
                    resultData = bid;
                    result = true;
                    new EventLogGenerator(EventLog.Enum.HisBid_HuyDuyetThau, bid.BID_NAME, bid.BID_NUMBER, bid.APPROVAL_LOGINNAME, bid.APPROVAL_USERNAME).Run();
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
