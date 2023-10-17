using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace SAR.DAO.SarFormData
{
    partial class SarFormDataCheck : EntityBase
    {
        public SarFormDataCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_FORM_DATA>();
        }

        private BridgeDAO<SAR_FORM_DATA> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
