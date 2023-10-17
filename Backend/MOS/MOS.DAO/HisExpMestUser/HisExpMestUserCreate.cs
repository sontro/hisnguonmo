using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestUser
{
    partial class HisExpMestUserCreate : EntityBase
    {
        public HisExpMestUserCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_USER>();
        }

        private BridgeDAO<HIS_EXP_MEST_USER> bridgeDAO;

        public bool Create(HIS_EXP_MEST_USER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXP_MEST_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
