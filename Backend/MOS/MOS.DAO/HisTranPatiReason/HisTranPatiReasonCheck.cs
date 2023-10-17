using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTranPatiReason
{
    partial class HisTranPatiReasonCheck : EntityBase
    {
        public HisTranPatiReasonCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRAN_PATI_REASON>();
        }

        private BridgeDAO<HIS_TRAN_PATI_REASON> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
