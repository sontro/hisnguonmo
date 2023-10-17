using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceType
{
    partial class HisServiceTypeCreate : EntityBase
    {
        public HisServiceTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_TYPE>();
        }

        private BridgeDAO<HIS_SERVICE_TYPE> bridgeDAO;

        public bool Create(HIS_SERVICE_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
