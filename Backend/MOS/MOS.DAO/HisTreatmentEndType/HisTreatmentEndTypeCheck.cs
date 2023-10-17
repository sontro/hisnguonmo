using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTreatmentEndType
{
    partial class HisTreatmentEndTypeCheck : EntityBase
    {
        public HisTreatmentEndTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_END_TYPE>();
        }

        private BridgeDAO<HIS_TREATMENT_END_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
