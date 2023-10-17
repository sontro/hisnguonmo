using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPeriodBlty
{
    partial class HisMestPeriodBltyCreate : EntityBase
    {
        public HisMestPeriodBltyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_BLTY>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_BLTY> bridgeDAO;

        public bool Create(HIS_MEST_PERIOD_BLTY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEST_PERIOD_BLTY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
