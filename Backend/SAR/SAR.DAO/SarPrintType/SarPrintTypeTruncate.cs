using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarPrintType
{
    partial class SarPrintTypeTruncate : EntityBase
    {
        public SarPrintTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_PRINT_TYPE>();
        }

        private BridgeDAO<SAR_PRINT_TYPE> bridgeDAO;

        public bool Truncate(SAR_PRINT_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SAR_PRINT_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
