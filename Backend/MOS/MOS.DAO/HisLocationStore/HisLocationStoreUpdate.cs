using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisLocationStore
{
    partial class HisLocationStoreUpdate : EntityBase
    {
        public HisLocationStoreUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_LOCATION_STORE>();
        }

        private BridgeDAO<HIS_LOCATION_STORE> bridgeDAO;

        public bool Update(HIS_LOCATION_STORE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_LOCATION_STORE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
