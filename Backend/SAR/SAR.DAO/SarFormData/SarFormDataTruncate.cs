using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarFormData
{
    partial class SarFormDataTruncate : EntityBase
    {
        public SarFormDataTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_FORM_DATA>();
        }

        private BridgeDAO<SAR_FORM_DATA> bridgeDAO;

        public bool Truncate(SAR_FORM_DATA data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SAR_FORM_DATA> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
