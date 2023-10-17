using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDrugIntervention
{
    partial class HisDrugInterventionUpdate : EntityBase
    {
        public HisDrugInterventionUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DRUG_INTERVENTION>();
        }

        private BridgeDAO<HIS_DRUG_INTERVENTION> bridgeDAO;

        public bool Update(HIS_DRUG_INTERVENTION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DRUG_INTERVENTION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
