using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisPtttGroupBest
{
    partial class HisPtttGroupBestUpdate : EntityBase
    {
        public HisPtttGroupBestUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PTTT_GROUP_BEST>();
        }

        private BridgeDAO<HIS_PTTT_GROUP_BEST> bridgeDAO;

        public bool Update(HIS_PTTT_GROUP_BEST data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_PTTT_GROUP_BEST> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
