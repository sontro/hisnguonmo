using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCarerCard
{
    partial class HisCarerCardUpdate : EntityBase
    {
        public HisCarerCardUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARER_CARD>();
        }

        private BridgeDAO<HIS_CARER_CARD> bridgeDAO;

        public bool Update(HIS_CARER_CARD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CARER_CARD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
