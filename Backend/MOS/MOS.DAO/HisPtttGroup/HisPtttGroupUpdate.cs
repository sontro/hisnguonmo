using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttGroup
{
    partial class HisPtttGroupUpdate : EntityBase
    {
        public HisPtttGroupUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_GROUP>();
        }

        private BridgeDAO<HIS_PTTT_GROUP> bridgeDAO;

        public bool Update(HIS_PTTT_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PTTT_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
