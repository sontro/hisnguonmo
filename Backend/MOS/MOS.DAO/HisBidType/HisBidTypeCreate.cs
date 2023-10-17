using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBidType
{
    partial class HisBidTypeCreate : EntityBase
    {
        public HisBidTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BID_TYPE>();
        }

        private BridgeDAO<HIS_BID_TYPE> bridgeDAO;

        public bool Create(HIS_BID_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BID_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
