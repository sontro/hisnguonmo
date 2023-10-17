using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisContact
{
    partial class HisContactUpdate : EntityBase
    {
        public HisContactUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CONTACT>();
        }

        private BridgeDAO<HIS_CONTACT> bridgeDAO;

        public bool Update(HIS_CONTACT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CONTACT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
