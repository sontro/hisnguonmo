using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPaanLiquid
{
    partial class HisPaanLiquidCreate : EntityBase
    {
        public HisPaanLiquidCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PAAN_LIQUID>();
        }

        private BridgeDAO<HIS_PAAN_LIQUID> bridgeDAO;

        public bool Create(HIS_PAAN_LIQUID data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PAAN_LIQUID> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
