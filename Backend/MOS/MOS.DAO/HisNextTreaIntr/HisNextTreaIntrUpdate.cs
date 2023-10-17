using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisNextTreaIntr
{
    partial class HisNextTreaIntrUpdate : EntityBase
    {
        public HisNextTreaIntrUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_NEXT_TREA_INTR>();
        }

        private BridgeDAO<HIS_NEXT_TREA_INTR> bridgeDAO;

        public bool Update(HIS_NEXT_TREA_INTR data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_NEXT_TREA_INTR> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
