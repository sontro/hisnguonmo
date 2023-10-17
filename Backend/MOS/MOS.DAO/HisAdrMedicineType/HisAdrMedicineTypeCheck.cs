using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisAdrMedicineType
{
    partial class HisAdrMedicineTypeCheck : EntityBase
    {
        public HisAdrMedicineTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ADR_MEDICINE_TYPE>();
        }

        private BridgeDAO<HIS_ADR_MEDICINE_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
