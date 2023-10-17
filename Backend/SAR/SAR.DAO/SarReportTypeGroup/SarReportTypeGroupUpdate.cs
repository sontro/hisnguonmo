using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarReportTypeGroup
{
    partial class SarReportTypeGroupUpdate : EntityBase
    {
        public SarReportTypeGroupUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_REPORT_TYPE_GROUP>();
        }

        private BridgeDAO<SAR_REPORT_TYPE_GROUP> bridgeDAO;

        public bool Update(SAR_REPORT_TYPE_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SAR_REPORT_TYPE_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
