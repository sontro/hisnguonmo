using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisBlood;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisMaterial;
using MOS.MANAGER.HisMedicine;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBid.Unapprove
{
    partial class HisBidUnapproveCheck : BusinessBase
    {
        internal HisBidUnapproveCheck()
            : base()
        {

        }

        internal HisBidUnapproveCheck(CommonParam paramCheck)
            : base(paramCheck) 
        {

        }

        internal bool IsApprove(long bidId, ref HIS_BID bid)
        {
            bool valid = true;
            try
            {
                bid = new HisBidGet().GetById(bidId);
                if (!bid.APPROVAL_TIME.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBid_ThauChuaDuocDuyet, bid.BID_NAME, bid.BID_NUMBER);
                    return false;
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
