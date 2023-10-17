using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SAR.DAO.SarReportStt
{
    partial class SarReportSttCheck : EntityBase
    {
        public SarReportSttCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_REPORT_STT>();
        }

        private BridgeDAO<SAR_REPORT_STT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
