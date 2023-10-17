using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMestPeriodMety
{
    partial class HisMestPeriodMetyCreate : EntityBase
    {
        public HisMestPeriodMetyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PERIOD_METY>();
        }

        private BridgeDAO<HIS_MEST_PERIOD_METY> bridgeDAO;

        public bool Create(HIS_MEST_PERIOD_METY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEST_PERIOD_METY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
