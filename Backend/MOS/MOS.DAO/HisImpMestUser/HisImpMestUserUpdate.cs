using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMestUser
{
    partial class HisImpMestUserUpdate : EntityBase
    {
        public HisImpMestUserUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_USER>();
        }

        private BridgeDAO<HIS_IMP_MEST_USER> bridgeDAO;

        public bool Update(HIS_IMP_MEST_USER data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_IMP_MEST_USER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
