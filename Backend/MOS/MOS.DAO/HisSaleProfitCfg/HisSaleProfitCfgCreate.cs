using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSaleProfitCfg
{
    partial class HisSaleProfitCfgCreate : EntityBase
    {
        public HisSaleProfitCfgCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SALE_PROFIT_CFG>();
        }

        private BridgeDAO<HIS_SALE_PROFIT_CFG> bridgeDAO;

        public bool Create(HIS_SALE_PROFIT_CFG data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SALE_PROFIT_CFG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
