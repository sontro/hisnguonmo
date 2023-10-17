using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExpMestMedicine
{
    partial class HisExpMestMedicineCheck : EntityBase
    {
        public HisExpMestMedicineCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_MEDICINE>();
        }

        private BridgeDAO<HIS_EXP_MEST_MEDICINE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
