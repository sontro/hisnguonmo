using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisHivTreatment
{
    partial class HisHivTreatmentCreate : EntityBase
    {
        public HisHivTreatmentCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HIV_TREATMENT>();
        }

        private BridgeDAO<HIS_HIV_TREATMENT> bridgeDAO;

        public bool Create(HIS_HIV_TREATMENT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_HIV_TREATMENT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
