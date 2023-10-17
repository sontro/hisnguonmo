using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatment
{
    partial class HisTreatmentTruncate : EntityBase
    {
        public HisTreatmentTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT>();
        }

        private BridgeDAO<HIS_TREATMENT> bridgeDAO;

        public bool Truncate(HIS_TREATMENT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_TREATMENT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
