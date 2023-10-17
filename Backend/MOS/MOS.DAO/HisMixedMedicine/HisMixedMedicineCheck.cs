using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMixedMedicine
{
    partial class HisMixedMedicineCheck : EntityBase
    {
        public HisMixedMedicineCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MIXED_MEDICINE>();
        }

        private BridgeDAO<HIS_MIXED_MEDICINE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
