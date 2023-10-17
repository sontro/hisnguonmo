using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarReportStt
{
    partial class SarReportSttUpdate : EntityBase
    {
        public SarReportSttUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_REPORT_STT>();
        }

        private BridgeDAO<SAR_REPORT_STT> bridgeDAO;

        public bool Update(SAR_REPORT_STT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SAR_REPORT_STT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
