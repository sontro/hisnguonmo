using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisImpMestMedicine
{
    partial class HisImpMestMedicineCheck : EntityBase
    {
        public HisImpMestMedicineCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_MEDICINE>();
        }

        private BridgeDAO<HIS_IMP_MEST_MEDICINE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
