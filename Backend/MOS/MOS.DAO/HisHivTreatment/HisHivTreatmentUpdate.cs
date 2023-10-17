using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHivTreatment
{
    partial class HisHivTreatmentUpdate : EntityBase
    {
        public HisHivTreatmentUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HIV_TREATMENT>();
        }

        private BridgeDAO<HIS_HIV_TREATMENT> bridgeDAO;

        public bool Update(HIS_HIV_TREATMENT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_HIV_TREATMENT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
