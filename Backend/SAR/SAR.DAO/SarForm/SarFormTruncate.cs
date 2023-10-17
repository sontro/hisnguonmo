using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarForm
{
    partial class SarFormTruncate : EntityBase
    {
        public SarFormTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_FORM>();
        }

        private BridgeDAO<SAR_FORM> bridgeDAO;

        public bool Truncate(SAR_FORM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SAR_FORM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
