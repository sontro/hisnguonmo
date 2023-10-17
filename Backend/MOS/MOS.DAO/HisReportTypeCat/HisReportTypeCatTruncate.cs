using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisReportTypeCat
{
    partial class HisReportTypeCatTruncate : EntityBase
    {
        public HisReportTypeCatTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REPORT_TYPE_CAT>();
        }

        private BridgeDAO<HIS_REPORT_TYPE_CAT> bridgeDAO;

        public bool Truncate(HIS_REPORT_TYPE_CAT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_REPORT_TYPE_CAT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
