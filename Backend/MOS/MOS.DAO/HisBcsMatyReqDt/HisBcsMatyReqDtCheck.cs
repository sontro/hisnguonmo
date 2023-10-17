using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBcsMatyReqDt
{
    partial class HisBcsMatyReqDtCheck : EntityBase
    {
        public HisBcsMatyReqDtCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BCS_MATY_REQ_DT>();
        }

        private BridgeDAO<HIS_BCS_MATY_REQ_DT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
