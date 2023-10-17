using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceRetyCat
{
    partial class HisServiceRetyCatUpdate : EntityBase
    {
        public HisServiceRetyCatUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_RETY_CAT>();
        }

        private BridgeDAO<HIS_SERVICE_RETY_CAT> bridgeDAO;

        public bool Update(HIS_SERVICE_RETY_CAT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE_RETY_CAT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
