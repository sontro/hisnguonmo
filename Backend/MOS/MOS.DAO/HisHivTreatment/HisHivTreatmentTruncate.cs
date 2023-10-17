using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisHivTreatment
{
    partial class HisHivTreatmentTruncate : EntityBase
    {
        public HisHivTreatmentTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HIV_TREATMENT>();
        }

        private BridgeDAO<HIS_HIV_TREATMENT> bridgeDAO;

        public bool Truncate(HIS_HIV_TREATMENT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_HIV_TREATMENT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
