using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarFormField
{
    partial class SarFormFieldTruncate : EntityBase
    {
        public SarFormFieldTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_FORM_FIELD>();
        }

        private BridgeDAO<SAR_FORM_FIELD> bridgeDAO;

        public bool Truncate(SAR_FORM_FIELD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SAR_FORM_FIELD> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
