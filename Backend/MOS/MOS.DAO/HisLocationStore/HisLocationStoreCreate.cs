using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisLocationStore
{
    partial class HisLocationStoreCreate : EntityBase
    {
        public HisLocationStoreCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_LOCATION_STORE>();
        }

        private BridgeDAO<HIS_LOCATION_STORE> bridgeDAO;

        public bool Create(HIS_LOCATION_STORE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_LOCATION_STORE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
