using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTreatmentFile
{
    partial class HisTreatmentFileCheck : EntityBase
    {
        public HisTreatmentFileCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_FILE>();
        }

        private BridgeDAO<HIS_TREATMENT_FILE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
