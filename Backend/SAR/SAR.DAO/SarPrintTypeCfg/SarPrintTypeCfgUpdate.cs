using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarPrintTypeCfg
{
    partial class SarPrintTypeCfgUpdate : EntityBase
    {
        public SarPrintTypeCfgUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_PRINT_TYPE_CFG>();
        }

        private BridgeDAO<SAR_PRINT_TYPE_CFG> bridgeDAO;

        public bool Update(SAR_PRINT_TYPE_CFG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SAR_PRINT_TYPE_CFG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
