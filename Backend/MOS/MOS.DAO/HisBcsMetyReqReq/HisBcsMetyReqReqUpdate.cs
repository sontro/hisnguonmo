using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBcsMetyReqReq
{
    partial class HisBcsMetyReqReqUpdate : EntityBase
    {
        public HisBcsMetyReqReqUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BCS_METY_REQ_REQ>();
        }

        private BridgeDAO<HIS_BCS_METY_REQ_REQ> bridgeDAO;

        public bool Update(HIS_BCS_METY_REQ_REQ data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BCS_METY_REQ_REQ> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
