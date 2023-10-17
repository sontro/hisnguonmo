using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarRetyFofi
{
    partial class SarRetyFofiTruncate : EntityBase
    {
        public SarRetyFofiTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_RETY_FOFI>();
        }

        private BridgeDAO<SAR_RETY_FOFI> bridgeDAO;

        public bool Truncate(SAR_RETY_FOFI data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<SAR_RETY_FOFI> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
