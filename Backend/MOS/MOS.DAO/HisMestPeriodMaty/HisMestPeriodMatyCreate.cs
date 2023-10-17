using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPeriodMaty
{
    partial class HisMestPeriodMatyCreate : EntityBase
    {
        public HisMestPeriodMatyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_MATY>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_MATY> bridgeDAO;

        public bool Create(HIS_MEST_PERIOD_MATY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEST_PERIOD_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
