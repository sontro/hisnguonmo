using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMrChecklist
{
    partial class HisMrChecklistCreate : EntityBase
    {
        public HisMrChecklistCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MR_CHECKLIST>();
        }

        private BridgeDAO<HIS_MR_CHECKLIST> bridgeDAO;

        public bool Create(HIS_MR_CHECKLIST data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MR_CHECKLIST> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
