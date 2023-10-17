using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SAR.DAO.SarReportTypeGroup
{
    partial class SarReportTypeGroupCreate : EntityBase
    {
        public SarReportTypeGroupCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_REPORT_TYPE_GROUP>();
        }

        private BridgeDAO<SAR_REPORT_TYPE_GROUP> bridgeDAO;

        public bool Create(SAR_REPORT_TYPE_GROUP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SAR_REPORT_TYPE_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
