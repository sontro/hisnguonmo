using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisImpUserTempDt
{
    partial class HisImpUserTempDtCheck : EntityBase
    {
        public HisImpUserTempDtCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_USER_TEMP_DT>();
        }

        private BridgeDAO<HIS_IMP_USER_TEMP_DT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
