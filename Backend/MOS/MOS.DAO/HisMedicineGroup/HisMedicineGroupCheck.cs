using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMedicineGroup
{
    partial class HisMedicineGroupCheck : EntityBase
    {
        public HisMedicineGroupCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_GROUP>();
        }

        private BridgeDAO<HIS_MEDICINE_GROUP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
