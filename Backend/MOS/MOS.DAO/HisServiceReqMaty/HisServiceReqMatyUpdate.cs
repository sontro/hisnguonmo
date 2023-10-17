using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceReqMaty
{
    partial class HisServiceReqMatyUpdate : EntityBase
    {
        public HisServiceReqMatyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ_MATY>();
        }

        private BridgeDAO<HIS_SERVICE_REQ_MATY> bridgeDAO;

        public bool Update(HIS_SERVICE_REQ_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SERVICE_REQ_MATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
