using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDataStore
{
    partial class HisDataStoreCreate : EntityBase
    {
        public HisDataStoreCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DATA_STORE>();
        }

        private BridgeDAO<HIS_DATA_STORE> bridgeDAO;

        public bool Create(HIS_DATA_STORE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DATA_STORE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
