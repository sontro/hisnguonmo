using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarRetyFofi
{
    partial class SarRetyFofiUpdate : EntityBase
    {
        public SarRetyFofiUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_RETY_FOFI>();
        }

        private BridgeDAO<SAR_RETY_FOFI> bridgeDAO;

        public bool Update(SAR_RETY_FOFI data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SAR_RETY_FOFI> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
