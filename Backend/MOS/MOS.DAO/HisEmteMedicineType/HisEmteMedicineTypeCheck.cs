using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisEmteMedicineType
{
    partial class HisEmteMedicineTypeCheck : EntityBase
    {
        public HisEmteMedicineTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMTE_MEDICINE_TYPE>();
        }

        private BridgeDAO<HIS_EMTE_MEDICINE_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
