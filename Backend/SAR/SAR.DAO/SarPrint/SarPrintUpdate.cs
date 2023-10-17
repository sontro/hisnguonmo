using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarPrint
{
    partial class SarPrintUpdate : EntityBase
    {
        public SarPrintUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_PRINT>();
        }

        private BridgeDAO<SAR_PRINT> bridgeDAO;

        public bool Update(SAR_PRINT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SAR_PRINT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
