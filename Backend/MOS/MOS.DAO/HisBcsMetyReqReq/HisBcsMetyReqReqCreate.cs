using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBcsMetyReqReq
{
    partial class HisBcsMetyReqReqCreate : EntityBase
    {
        public HisBcsMetyReqReqCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BCS_METY_REQ_REQ>();
        }

        private BridgeDAO<HIS_BCS_METY_REQ_REQ> bridgeDAO;

        public bool Create(HIS_BCS_METY_REQ_REQ data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BCS_METY_REQ_REQ> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
