using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMrChecklist
{
    partial class HisMrChecklistTruncate : EntityBase
    {
        public HisMrChecklistTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MR_CHECKLIST>();
        }

        private BridgeDAO<HIS_MR_CHECKLIST> bridgeDAO;

        public bool Truncate(HIS_MR_CHECKLIST data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MR_CHECKLIST> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
