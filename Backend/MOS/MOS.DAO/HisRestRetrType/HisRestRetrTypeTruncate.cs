using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRestRetrType
{
    partial class HisRestRetrTypeTruncate : EntityBase
    {
        public HisRestRetrTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REST_RETR_TYPE>();
        }

        private BridgeDAO<HIS_REST_RETR_TYPE> bridgeDAO;

        public bool Truncate(HIS_REST_RETR_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_REST_RETR_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
