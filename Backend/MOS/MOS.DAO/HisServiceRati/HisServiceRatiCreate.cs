using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceRati
{
    partial class HisServiceRatiCreate : EntityBase
    {
        public HisServiceRatiCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_RATI>();
        }

        private BridgeDAO<HIS_SERVICE_RATI> bridgeDAO;

        public bool Create(HIS_SERVICE_RATI data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_RATI> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
