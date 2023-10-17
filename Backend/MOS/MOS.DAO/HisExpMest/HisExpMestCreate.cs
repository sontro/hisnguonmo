using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMest
{
    partial class HisExpMestCreate : EntityBase
    {
        public HisExpMestCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST>();
        }

        private BridgeDAO<HIS_EXP_MEST> bridgeDAO;

        public bool Create(HIS_EXP_MEST data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXP_MEST> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
