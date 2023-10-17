using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMetyProduct
{
    partial class HisMetyProductCreate : EntityBase
    {
        public HisMetyProductCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_METY_PRODUCT>();
        }

        private BridgeDAO<HIS_METY_PRODUCT> bridgeDAO;

        public bool Create(HIS_METY_PRODUCT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_METY_PRODUCT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
