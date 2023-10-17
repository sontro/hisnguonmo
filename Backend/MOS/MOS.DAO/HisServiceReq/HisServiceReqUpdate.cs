using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceReq
{
    partial class HisServiceReqUpdate : EntityBase
    {
        public HisServiceReqUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ>();
        }

        private BridgeDAO<HIS_SERVICE_REQ> bridgeDAO;

        public bool Update(HIS_SERVICE_REQ data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE_REQ> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
