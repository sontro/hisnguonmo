using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SAR.DAO.SarReportStt
{
    partial class SarReportSttCreate : EntityBase
    {
        public SarReportSttCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_REPORT_STT>();
        }

        private BridgeDAO<SAR_REPORT_STT> bridgeDAO;

        public bool Create(SAR_REPORT_STT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SAR_REPORT_STT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
