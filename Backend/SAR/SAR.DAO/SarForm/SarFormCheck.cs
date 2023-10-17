using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SAR.DAO.SarForm
{
    partial class SarFormCheck : EntityBase
    {
        public SarFormCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_FORM>();
        }

        private BridgeDAO<SAR_FORM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
