using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarForm
{
    partial class SarFormUpdate : EntityBase
    {
        public SarFormUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_FORM>();
        }

        private BridgeDAO<SAR_FORM> bridgeDAO;

        public bool Update(SAR_FORM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SAR_FORM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
