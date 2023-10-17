using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBcsMetyReqDt
{
    partial class HisBcsMetyReqDtCheck : EntityBase
    {
        public HisBcsMetyReqDtCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BCS_METY_REQ_DT>();
        }

        private BridgeDAO<HIS_BCS_METY_REQ_DT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
