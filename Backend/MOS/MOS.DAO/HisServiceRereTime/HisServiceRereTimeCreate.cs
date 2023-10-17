using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceRereTime
{
    partial class HisServiceRereTimeCreate : EntityBase
    {
        public HisServiceRereTimeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_RERE_TIME>();
        }

        private BridgeDAO<HIS_SERVICE_RERE_TIME> bridgeDAO;

        public bool Create(HIS_SERVICE_RERE_TIME data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_RERE_TIME> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
