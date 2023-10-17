using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceReqStt
{
    partial class HisServiceReqSttUpdate : EntityBase
    {
        public HisServiceReqSttUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ_STT>();
        }

        private BridgeDAO<HIS_SERVICE_REQ_STT> bridgeDAO;

        public bool Update(HIS_SERVICE_REQ_STT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE_REQ_STT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
