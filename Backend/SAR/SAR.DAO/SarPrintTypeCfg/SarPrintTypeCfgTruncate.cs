using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarPrintTypeCfg
{
    partial class SarPrintTypeCfgTruncate : EntityBase
    {
        public SarPrintTypeCfgTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_PRINT_TYPE_CFG>();
        }

        private BridgeDAO<SAR_PRINT_TYPE_CFG> bridgeDAO;

        public bool Truncate(SAR_PRINT_TYPE_CFG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SAR_PRINT_TYPE_CFG> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
