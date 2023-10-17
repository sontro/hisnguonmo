using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarPrintLog
{
    partial class SarPrintLogUpdate : EntityBase
    {
        public SarPrintLogUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_PRINT_LOG>();
        }

        private BridgeDAO<SAR_PRINT_LOG> bridgeDAO;

        public bool Update(SAR_PRINT_LOG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SAR_PRINT_LOG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
