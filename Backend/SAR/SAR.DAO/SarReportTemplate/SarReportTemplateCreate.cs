using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SAR.DAO.SarReportTemplate
{
    partial class SarReportTemplateCreate : EntityBase
    {
        public SarReportTemplateCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_REPORT_TEMPLATE>();
        }

        private BridgeDAO<SAR_REPORT_TEMPLATE> bridgeDAO;

        public bool Create(SAR_REPORT_TEMPLATE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SAR_REPORT_TEMPLATE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
