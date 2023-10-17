using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDrugIntervention
{
    partial class HisDrugInterventionCheck : EntityBase
    {
        public HisDrugInterventionCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DRUG_INTERVENTION>();
        }

        private BridgeDAO<HIS_DRUG_INTERVENTION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
