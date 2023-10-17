using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SAR.DAO.SarReportType
{
    partial class SarReportTypeCreate : EntityBase
    {
        public SarReportTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_REPORT_TYPE>();
        }

        private BridgeDAO<SAR_REPORT_TYPE> bridgeDAO;

        public bool Create(SAR_REPORT_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SAR_REPORT_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
