using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDosageForm
{
    partial class HisDosageFormCheck : EntityBase
    {
        public HisDosageFormCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DOSAGE_FORM>();
        }

        private BridgeDAO<HIS_DOSAGE_FORM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
