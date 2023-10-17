using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SAR.DAO.SarPrint
{
    partial class SarPrintCreate : EntityBase
    {
        public SarPrintCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_PRINT>();
        }

        private BridgeDAO<SAR_PRINT> bridgeDAO;

        public bool Create(SAR_PRINT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SAR_PRINT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
