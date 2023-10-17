using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPeriodMedi
{
    partial class HisMestPeriodMediCreate : EntityBase
    {
        public HisMestPeriodMediCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_MEDI>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_MEDI> bridgeDAO;

        public bool Create(HIS_MEST_PERIOD_MEDI data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEST_PERIOD_MEDI> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
