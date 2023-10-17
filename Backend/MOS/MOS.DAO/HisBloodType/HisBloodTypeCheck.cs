using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBloodType
{
    partial class HisBloodTypeCheck : EntityBase
    {
        public HisBloodTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_TYPE>();
        }

        private BridgeDAO<HIS_BLOOD_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
