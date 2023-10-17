using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarReportTemplate
{
    partial class SarReportTemplateUpdate : EntityBase
    {
        public SarReportTemplateUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_REPORT_TEMPLATE>();
        }

        private BridgeDAO<SAR_REPORT_TEMPLATE> bridgeDAO;

        public bool Update(SAR_REPORT_TEMPLATE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SAR_REPORT_TEMPLATE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
