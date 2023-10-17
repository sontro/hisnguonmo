using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SAR.DAO.SarPrintType
{
    partial class SarPrintTypeCreate : EntityBase
    {
        public SarPrintTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_PRINT_TYPE>();
        }

        private BridgeDAO<SAR_PRINT_TYPE> bridgeDAO;

        public bool Create(SAR_PRINT_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SAR_PRINT_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
