using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisCareType
{
    partial class HisCareTypeCheck : EntityBase
    {
        public HisCareTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARE_TYPE>();
        }

        private BridgeDAO<HIS_CARE_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
