using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMrChecklist
{
    partial class HisMrChecklistUpdate : EntityBase
    {
        public HisMrChecklistUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MR_CHECKLIST>();
        }

        private BridgeDAO<HIS_MR_CHECKLIST> bridgeDAO;

        public bool Update(HIS_MR_CHECKLIST data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MR_CHECKLIST> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
