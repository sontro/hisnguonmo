using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisServiceReqType
{
    partial class HisServiceReqTypeTruncate : EntityBase
    {
        public HisServiceReqTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERVICE_REQ_TYPE>();
        }

        private BridgeDAO<HIS_SERVICE_REQ_TYPE> bridgeDAO;

        public bool Truncate(HIS_SERVICE_REQ_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERVICE_REQ_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
