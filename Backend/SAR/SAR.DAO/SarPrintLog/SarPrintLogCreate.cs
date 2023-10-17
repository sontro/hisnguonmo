using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SAR.DAO.SarPrintLog
{
    partial class SarPrintLogCreate : EntityBase
    {
        public SarPrintLogCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_PRINT_LOG>();
        }

        private BridgeDAO<SAR_PRINT_LOG> bridgeDAO;

        public bool Create(SAR_PRINT_LOG data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SAR_PRINT_LOG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
