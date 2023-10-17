using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisKskPeriodDriver
{
    partial class HisKskPeriodDriverCreate : EntityBase
    {
        public HisKskPeriodDriverCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_PERIOD_DRIVER>();
        }

        private BridgeDAO<HIS_KSK_PERIOD_DRIVER> bridgeDAO;

        public bool Create(HIS_KSK_PERIOD_DRIVER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_KSK_PERIOD_DRIVER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
