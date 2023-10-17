using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMestType
{
    partial class HisImpMestTypeUpdate : EntityBase
    {
        public HisImpMestTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_TYPE>();
        }

        private BridgeDAO<HIS_IMP_MEST_TYPE> bridgeDAO;

        public bool Update(HIS_IMP_MEST_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_IMP_MEST_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
