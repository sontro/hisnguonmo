using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SAR.DAO.SarFormType
{
    partial class SarFormTypeCheck : EntityBase
    {
        public SarFormTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_FORM_TYPE>();
        }

        private BridgeDAO<SAR_FORM_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
