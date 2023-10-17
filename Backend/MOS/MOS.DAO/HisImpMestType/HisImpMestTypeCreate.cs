using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMestType
{
    partial class HisImpMestTypeCreate : EntityBase
    {
        public HisImpMestTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_TYPE>();
        }

        private BridgeDAO<HIS_IMP_MEST_TYPE> bridgeDAO;

        public bool Create(HIS_IMP_MEST_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_IMP_MEST_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
