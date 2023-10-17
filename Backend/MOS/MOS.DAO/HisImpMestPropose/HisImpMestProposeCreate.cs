using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMestPropose
{
    partial class HisImpMestProposeCreate : EntityBase
    {
        public HisImpMestProposeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_PROPOSE>();
        }

        private BridgeDAO<HIS_IMP_MEST_PROPOSE> bridgeDAO;

        public bool Create(HIS_IMP_MEST_PROPOSE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_IMP_MEST_PROPOSE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
