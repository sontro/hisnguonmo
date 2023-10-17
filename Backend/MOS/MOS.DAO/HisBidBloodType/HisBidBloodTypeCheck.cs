using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBidBloodType
{
    partial class HisBidBloodTypeCheck : EntityBase
    {
        public HisBidBloodTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BID_BLOOD_TYPE>();
        }

        private BridgeDAO<HIS_BID_BLOOD_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
