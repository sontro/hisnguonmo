using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisCarerCard
{
    partial class HisCarerCardCreate : EntityBase
    {
        public HisCarerCardCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CARER_CARD>();
        }

        private BridgeDAO<HIS_CARER_CARD> bridgeDAO;

        public bool Create(HIS_CARER_CARD data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CARER_CARD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
