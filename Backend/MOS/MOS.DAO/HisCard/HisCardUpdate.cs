using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCard
{
    partial class HisCardUpdate : EntityBase
    {
        public HisCardUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARD>();
        }

        private BridgeDAO<HIS_CARD> bridgeDAO;

        public bool Update(HIS_CARD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CARD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
