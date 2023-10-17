using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceReqMety
{
    partial class HisServiceReqMetyUpdate : EntityBase
    {
        public HisServiceReqMetyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ_METY>();
        }

        private BridgeDAO<HIS_SERVICE_REQ_METY> bridgeDAO;

        public bool Update(HIS_SERVICE_REQ_METY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE_REQ_METY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
