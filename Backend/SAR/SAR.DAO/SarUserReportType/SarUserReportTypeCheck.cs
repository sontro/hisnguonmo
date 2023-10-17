using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SAR.DAO.SarUserReportType
{
    partial class SarUserReportTypeCheck : EntityBase
    {
        public SarUserReportTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_USER_REPORT_TYPE>();
        }

        private BridgeDAO<SAR_USER_REPORT_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
