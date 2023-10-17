using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPeriodMate
{
    partial class HisMestPeriodMateCreate : EntityBase
    {
        public HisMestPeriodMateCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_MATE>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_MATE> bridgeDAO;

        public bool Create(HIS_MEST_PERIOD_MATE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEST_PERIOD_MATE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
