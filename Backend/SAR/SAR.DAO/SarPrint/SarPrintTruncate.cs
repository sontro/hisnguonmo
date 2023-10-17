using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarPrint
{
    partial class SarPrintTruncate : EntityBase
    {
        public SarPrintTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_PRINT>();
        }

        private BridgeDAO<SAR_PRINT> bridgeDAO;

        public bool Truncate(SAR_PRINT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SAR_PRINT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
