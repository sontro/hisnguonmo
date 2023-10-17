using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisOtherPaySource
{
    partial class HisOtherPaySourceUpdate : EntityBase
    {
        public HisOtherPaySourceUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_OTHER_PAY_SOURCE>();
        }

        private BridgeDAO<HIS_OTHER_PAY_SOURCE> bridgeDAO;

        public bool Update(HIS_OTHER_PAY_SOURCE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_OTHER_PAY_SOURCE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
