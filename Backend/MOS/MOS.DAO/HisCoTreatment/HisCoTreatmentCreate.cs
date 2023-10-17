using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisCoTreatment
{
    partial class HisCoTreatmentCreate : EntityBase
    {
        public HisCoTreatmentCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CO_TREATMENT>();
        }

        private BridgeDAO<HIS_CO_TREATMENT> bridgeDAO;

        public bool Create(HIS_CO_TREATMENT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CO_TREATMENT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
