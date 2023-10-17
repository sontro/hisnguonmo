using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpSource
{
    partial class HisImpSourceUpdate : EntityBase
    {
        public HisImpSourceUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_SOURCE>();
        }

        private BridgeDAO<HIS_IMP_SOURCE> bridgeDAO;

        public bool Update(HIS_IMP_SOURCE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_IMP_SOURCE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
