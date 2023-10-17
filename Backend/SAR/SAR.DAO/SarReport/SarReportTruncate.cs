using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarReport
{
    partial class SarReportTruncate : EntityBase
    {
        public SarReportTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_REPORT>();
        }

        private BridgeDAO<SAR_REPORT> bridgeDAO;

        public bool Truncate(SAR_REPORT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SAR_REPORT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
