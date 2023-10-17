using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestType
{
    partial class HisExpMestTypeCreate : EntityBase
    {
        public HisExpMestTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_TYPE>();
        }

        private BridgeDAO<HIS_EXP_MEST_TYPE> bridgeDAO;

        public bool Create(HIS_EXP_MEST_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXP_MEST_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
