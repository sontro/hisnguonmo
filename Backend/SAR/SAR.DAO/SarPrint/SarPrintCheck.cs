using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SAR.DAO.SarPrint
{
    partial class SarPrintCheck : EntityBase
    {
        public SarPrintCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_PRINT>();
        }

        private BridgeDAO<SAR_PRINT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
