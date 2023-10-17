using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTreatmentEndTypeExt
{
    partial class HisTreatmentEndTypeExtCheck : EntityBase
    {
        public HisTreatmentEndTypeExtCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_END_TYPE_EXT>();
        }

        private BridgeDAO<HIS_TREATMENT_END_TYPE_EXT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
