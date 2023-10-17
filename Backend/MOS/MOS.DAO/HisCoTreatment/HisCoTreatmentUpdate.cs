using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCoTreatment
{
    partial class HisCoTreatmentUpdate : EntityBase
    {
        public HisCoTreatmentUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CO_TREATMENT>();
        }

        private BridgeDAO<HIS_CO_TREATMENT> bridgeDAO;

        public bool Update(HIS_CO_TREATMENT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CO_TREATMENT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
