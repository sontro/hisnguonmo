using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarFormType
{
    partial class SarFormTypeUpdate : EntityBase
    {
        public SarFormTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_FORM_TYPE>();
        }

        private BridgeDAO<SAR_FORM_TYPE> bridgeDAO;

        public bool Update(SAR_FORM_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SAR_FORM_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
