using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisCashierAddConfig
{
    partial class HisCashierAddConfigCreate : EntityBase
    {
        public HisCashierAddConfigCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CASHIER_ADD_CONFIG>();
        }

        private BridgeDAO<HIS_CASHIER_ADD_CONFIG> bridgeDAO;

        public bool Create(HIS_CASHIER_ADD_CONFIG data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CASHIER_ADD_CONFIG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
