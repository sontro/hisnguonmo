using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisReportTypeCat
{
    partial class HisReportTypeCatCheck : EntityBase
    {
        public HisReportTypeCatCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REPORT_TYPE_CAT>();
        }

        private BridgeDAO<HIS_REPORT_TYPE_CAT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
