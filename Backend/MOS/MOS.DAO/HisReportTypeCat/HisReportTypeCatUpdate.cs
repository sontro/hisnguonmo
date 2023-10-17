using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisReportTypeCat
{
    partial class HisReportTypeCatUpdate : EntityBase
    {
        public HisReportTypeCatUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REPORT_TYPE_CAT>();
        }

        private BridgeDAO<HIS_REPORT_TYPE_CAT> bridgeDAO;

        public bool Update(HIS_REPORT_TYPE_CAT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_REPORT_TYPE_CAT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
