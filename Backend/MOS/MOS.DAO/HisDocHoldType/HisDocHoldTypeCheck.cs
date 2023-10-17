using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDocHoldType
{
    partial class HisDocHoldTypeCheck : EntityBase
    {
        public HisDocHoldTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DOC_HOLD_TYPE>();
        }

        private BridgeDAO<HIS_DOC_HOLD_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
