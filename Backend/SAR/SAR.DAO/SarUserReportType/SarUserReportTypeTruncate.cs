using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarUserReportType
{
    partial class SarUserReportTypeTruncate : EntityBase
    {
        public SarUserReportTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_USER_REPORT_TYPE>();
        }

        private BridgeDAO<SAR_USER_REPORT_TYPE> bridgeDAO;

        public bool Truncate(SAR_USER_REPORT_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SAR_USER_REPORT_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
