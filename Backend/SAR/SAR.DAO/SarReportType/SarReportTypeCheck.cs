using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SAR.DAO.SarReportType
{
    partial class SarReportTypeCheck : EntityBase
    {
        public SarReportTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_REPORT_TYPE>();
        }

        private BridgeDAO<SAR_REPORT_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
