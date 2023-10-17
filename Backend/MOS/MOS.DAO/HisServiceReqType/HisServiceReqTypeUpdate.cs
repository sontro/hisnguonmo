using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceReqType
{
    partial class HisServiceReqTypeUpdate : EntityBase
    {
        public HisServiceReqTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ_TYPE>();
        }

        private BridgeDAO<HIS_SERVICE_REQ_TYPE> bridgeDAO;

        public bool Update(HIS_SERVICE_REQ_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE_REQ_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
