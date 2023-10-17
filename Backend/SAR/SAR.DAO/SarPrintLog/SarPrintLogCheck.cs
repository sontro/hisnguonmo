using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SAR.DAO.SarPrintLog
{
    partial class SarPrintLogCheck : EntityBase
    {
        public SarPrintLogCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_PRINT_LOG>();
        }

        private BridgeDAO<SAR_PRINT_LOG> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
