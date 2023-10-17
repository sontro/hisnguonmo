using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAR.DAO.SarPrintType
{
    partial class SarPrintTypeUpdate : EntityBase
    {
        public SarPrintTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_PRINT_TYPE>();
        }

        private BridgeDAO<SAR_PRINT_TYPE> bridgeDAO;

        public bool Update(SAR_PRINT_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<SAR_PRINT_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
