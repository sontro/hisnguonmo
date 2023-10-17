using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceReq
{
    partial class HisServiceReqCreate : EntityBase
    {
        public HisServiceReqCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ>();
        }

        private BridgeDAO<HIS_SERVICE_REQ> bridgeDAO;

        public bool Create(HIS_SERVICE_REQ data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_REQ> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
