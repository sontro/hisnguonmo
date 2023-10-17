using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SAR.DAO.SarReportTypeGroup
{
    partial class SarReportTypeGroupCheck : EntityBase
    {
        public SarReportTypeGroupCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_REPORT_TYPE_GROUP>();
        }

        private BridgeDAO<SAR_REPORT_TYPE_GROUP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
