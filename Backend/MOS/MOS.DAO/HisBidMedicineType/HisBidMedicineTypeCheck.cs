using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisBidMedicineType
{
    partial class HisBidMedicineTypeCheck : EntityBase
    {
        public HisBidMedicineTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BID_MEDICINE_TYPE>();
        }

        private BridgeDAO<HIS_BID_MEDICINE_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
