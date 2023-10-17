using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarReportTemplate
{
    partial class SarReportTemplateTruncate : EntityBase
    {
        public SarReportTemplateTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_REPORT_TEMPLATE>();
        }

        private BridgeDAO<SAR_REPORT_TEMPLATE> bridgeDAO;

        public bool Truncate(SAR_REPORT_TEMPLATE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SAR_REPORT_TEMPLATE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
