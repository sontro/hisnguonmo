using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarReportTypeGroup
{
    partial class SarReportTypeGroupTruncate : EntityBase
    {
        public SarReportTypeGroupTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_REPORT_TYPE_GROUP>();
        }

        private BridgeDAO<SAR_REPORT_TYPE_GROUP> bridgeDAO;

        public bool Truncate(SAR_REPORT_TYPE_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SAR_REPORT_TYPE_GROUP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
