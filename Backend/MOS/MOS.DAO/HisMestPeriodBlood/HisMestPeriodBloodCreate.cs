using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPeriodBlood
{
    partial class HisMestPeriodBloodCreate : EntityBase
    {
        public HisMestPeriodBloodCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_BLOOD>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_BLOOD> bridgeDAO;

        public bool Create(HIS_MEST_PERIOD_BLOOD data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEST_PERIOD_BLOOD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
