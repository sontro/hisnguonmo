using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPaanLiquid
{
    partial class HisPaanLiquidUpdate : EntityBase
    {
        public HisPaanLiquidUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PAAN_LIQUID>();
        }

        private BridgeDAO<HIS_PAAN_LIQUID> bridgeDAO;

        public bool Update(HIS_PAAN_LIQUID data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PAAN_LIQUID> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
