using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMedicineTypeAcin
{
    partial class HisMedicineTypeAcinCheck : EntityBase
    {
        public HisMedicineTypeAcinCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_TYPE_ACIN>();
        }

        private BridgeDAO<HIS_MEDICINE_TYPE_ACIN> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
