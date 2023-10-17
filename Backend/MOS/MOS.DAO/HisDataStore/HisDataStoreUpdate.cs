using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDataStore
{
    partial class HisDataStoreUpdate : EntityBase
    {
        public HisDataStoreUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DATA_STORE>();
        }

        private BridgeDAO<HIS_DATA_STORE> bridgeDAO;

        public bool Update(HIS_DATA_STORE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DATA_STORE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
