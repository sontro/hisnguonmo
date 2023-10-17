using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisFinancePeriod
{
    partial class HisFinancePeriodCreate : EntityBase
    {
        public HisFinancePeriodCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FINANCE_PERIOD>();
        }

        private BridgeDAO<HIS_FINANCE_PERIOD> bridgeDAO;

        public bool Create(HIS_FINANCE_PERIOD data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_FINANCE_PERIOD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
