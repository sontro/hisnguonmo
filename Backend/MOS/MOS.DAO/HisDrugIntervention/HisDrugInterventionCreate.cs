using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDrugIntervention
{
    partial class HisDrugInterventionCreate : EntityBase
    {
        public HisDrugInterventionCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DRUG_INTERVENTION>();
        }

        private BridgeDAO<HIS_DRUG_INTERVENTION> bridgeDAO;

        public bool Create(HIS_DRUG_INTERVENTION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DRUG_INTERVENTION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
