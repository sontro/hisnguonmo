using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSourceMedicine
{
    partial class HisSourceMedicineCheck : EntityBase
    {
        public HisSourceMedicineCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SOURCE_MEDICINE>();
        }

        private BridgeDAO<HIS_SOURCE_MEDICINE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
