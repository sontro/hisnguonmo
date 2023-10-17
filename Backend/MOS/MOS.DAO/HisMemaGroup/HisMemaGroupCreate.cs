using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMemaGroup
{
    partial class HisMemaGroupCreate : EntityBase
    {
        public HisMemaGroupCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEMA_GROUP>();
        }

        private BridgeDAO<HIS_MEMA_GROUP> bridgeDAO;

        public bool Create(HIS_MEMA_GROUP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEMA_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
