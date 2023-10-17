using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SAR.DAO.SarRetyFofi
{
    partial class SarRetyFofiCheck : EntityBase
    {
        public SarRetyFofiCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_RETY_FOFI>();
        }

        private BridgeDAO<SAR_RETY_FOFI> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
