using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarFormType
{
    partial class SarFormTypeTruncate : EntityBase
    {
        public SarFormTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_FORM_TYPE>();
        }

        private BridgeDAO<SAR_FORM_TYPE> bridgeDAO;

        public bool Truncate(SAR_FORM_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SAR_FORM_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
