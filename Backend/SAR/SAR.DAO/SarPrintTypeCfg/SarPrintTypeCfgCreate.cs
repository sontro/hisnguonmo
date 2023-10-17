using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SAR.DAO.SarPrintTypeCfg
{
    partial class SarPrintTypeCfgCreate : EntityBase
    {
        public SarPrintTypeCfgCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_PRINT_TYPE_CFG>();
        }

        private BridgeDAO<SAR_PRINT_TYPE_CFG> bridgeDAO;

        public bool Create(SAR_PRINT_TYPE_CFG data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SAR_PRINT_TYPE_CFG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
