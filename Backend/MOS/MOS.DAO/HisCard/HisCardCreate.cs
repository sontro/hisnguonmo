using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisCard
{
    partial class HisCardCreate : EntityBase
    {
        public HisCardCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARD>();
        }

        private BridgeDAO<HIS_CARD> bridgeDAO;

        public bool Create(HIS_CARD data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CARD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
