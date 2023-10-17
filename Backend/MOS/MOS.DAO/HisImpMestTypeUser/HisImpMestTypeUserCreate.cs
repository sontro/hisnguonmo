using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMestTypeUser
{
    partial class HisImpMestTypeUserCreate : EntityBase
    {
        public HisImpMestTypeUserCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_TYPE_USER>();
        }

        private BridgeDAO<HIS_IMP_MEST_TYPE_USER> bridgeDAO;

        public bool Create(HIS_IMP_MEST_TYPE_USER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_IMP_MEST_TYPE_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
