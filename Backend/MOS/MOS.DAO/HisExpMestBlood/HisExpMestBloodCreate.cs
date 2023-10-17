using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestBlood
{
    partial class HisExpMestBloodCreate : EntityBase
    {
        public HisExpMestBloodCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_BLOOD>();
        }

        private BridgeDAO<HIS_EXP_MEST_BLOOD> bridgeDAO;

        public bool Create(HIS_EXP_MEST_BLOOD data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXP_MEST_BLOOD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
