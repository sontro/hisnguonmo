using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMest
{
    partial class HisImpMestCreate : EntityBase
    {
        public HisImpMestCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST>();
        }

        private BridgeDAO<HIS_IMP_MEST> bridgeDAO;

        public bool Create(HIS_IMP_MEST data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_IMP_MEST> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
