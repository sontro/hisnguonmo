using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SAR.DAO.SarPrintType
{
    partial class SarPrintTypeCheck : EntityBase
    {
        public SarPrintTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_PRINT_TYPE>();
        }

        private BridgeDAO<SAR_PRINT_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
