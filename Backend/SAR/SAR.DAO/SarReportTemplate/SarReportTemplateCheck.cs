using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SAR.DAO.SarReportTemplate
{
    partial class SarReportTemplateCheck : EntityBase
    {
        public SarReportTemplateCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_REPORT_TEMPLATE>();
        }

        private BridgeDAO<SAR_REPORT_TEMPLATE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
