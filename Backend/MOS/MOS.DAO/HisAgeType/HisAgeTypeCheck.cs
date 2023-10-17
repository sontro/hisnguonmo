using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisAgeType
{
    partial class HisAgeTypeCheck : EntityBase
    {
        public HisAgeTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_AGE_TYPE>();
        }

        private BridgeDAO<HIS_AGE_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
