using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarReportStt
{
    partial class SarReportSttTruncate : EntityBase
    {
        public SarReportSttTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_REPORT_STT>();
        }

        private BridgeDAO<SAR_REPORT_STT> bridgeDAO;

        public bool Truncate(SAR_REPORT_STT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SAR_REPORT_STT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
