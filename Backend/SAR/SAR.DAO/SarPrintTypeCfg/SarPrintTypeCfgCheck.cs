using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SAR.DAO.SarPrintTypeCfg
{
    partial class SarPrintTypeCfgCheck : EntityBase
    {
        public SarPrintTypeCfgCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_PRINT_TYPE_CFG>();
        }

        private BridgeDAO<SAR_PRINT_TYPE_CFG> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
