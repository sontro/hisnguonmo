using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisContactPoint
{
    partial class HisContactPointUpdate : EntityBase
    {
        public HisContactPointUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CONTACT_POINT>();
        }

        private BridgeDAO<HIS_CONTACT_POINT> bridgeDAO;

        public bool Update(HIS_CONTACT_POINT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_CONTACT_POINT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
