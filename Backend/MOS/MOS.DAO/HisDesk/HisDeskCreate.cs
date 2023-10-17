using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisDesk
{
    partial class HisDeskCreate : EntityBase
    {
        public HisDeskCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DESK>();
        }

        private BridgeDAO<HIS_DESK> bridgeDAO;

        public bool Create(HIS_DESK data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_DESK> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
