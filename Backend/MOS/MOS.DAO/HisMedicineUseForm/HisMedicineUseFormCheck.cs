using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMedicineUseForm
{
    partial class HisMedicineUseFormCheck : EntityBase
    {
        public HisMedicineUseFormCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_USE_FORM>();
        }

        private BridgeDAO<HIS_MEDICINE_USE_FORM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
