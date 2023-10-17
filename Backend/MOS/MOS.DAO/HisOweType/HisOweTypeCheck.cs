using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisOweType
{
    partial class HisOweTypeCheck : EntityBase
    {
        public HisOweTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_OWE_TYPE>();
        }

        private BridgeDAO<HIS_OWE_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
