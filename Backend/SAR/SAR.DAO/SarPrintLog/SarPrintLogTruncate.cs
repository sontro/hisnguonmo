using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarPrintLog
{
    partial class SarPrintLogTruncate : EntityBase
    {
        public SarPrintLogTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_PRINT_LOG>();
        }

        private BridgeDAO<SAR_PRINT_LOG> bridgeDAO;

        public bool Truncate(SAR_PRINT_LOG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SAR_PRINT_LOG> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
