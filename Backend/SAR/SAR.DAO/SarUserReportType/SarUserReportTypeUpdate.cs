using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarUserReportType
{
    partial class SarUserReportTypeUpdate : EntityBase
    {
        public SarUserReportTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_USER_REPORT_TYPE>();
        }

        private BridgeDAO<SAR_USER_REPORT_TYPE> bridgeDAO;

        public bool Update(SAR_USER_REPORT_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SAR_USER_REPORT_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
