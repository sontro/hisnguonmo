using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPeriodDriverDity
{
    partial class HisPeriodDriverDityCreate : EntityBase
    {
        public HisPeriodDriverDityCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PERIOD_DRIVER_DITY>();
        }

        private BridgeDAO<HIS_PERIOD_DRIVER_DITY> bridgeDAO;

        public bool Create(HIS_PERIOD_DRIVER_DITY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PERIOD_DRIVER_DITY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
