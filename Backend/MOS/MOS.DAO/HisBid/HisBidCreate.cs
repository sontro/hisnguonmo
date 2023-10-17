using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBid
{
    partial class HisBidCreate : EntityBase
    {
        public HisBidCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BID>();
        }

        private BridgeDAO<HIS_BID> bridgeDAO;

        public bool Create(HIS_BID data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BID> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
