using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisServiceChangeReq
{
    partial class HisServiceChangeReqCreate : EntityBase
    {
        public HisServiceChangeReqCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_CHANGE_REQ>();
        }

        private BridgeDAO<HIS_SERVICE_CHANGE_REQ> bridgeDAO;

        public bool Create(HIS_SERVICE_CHANGE_REQ data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SERVICE_CHANGE_REQ> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
