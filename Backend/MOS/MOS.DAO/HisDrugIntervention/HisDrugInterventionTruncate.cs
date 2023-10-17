using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDrugIntervention
{
    partial class HisDrugInterventionTruncate : EntityBase
    {
        public HisDrugInterventionTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DRUG_INTERVENTION>();
        }

        private BridgeDAO<HIS_DRUG_INTERVENTION> bridgeDAO;

        public bool Truncate(HIS_DRUG_INTERVENTION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_DRUG_INTERVENTION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
