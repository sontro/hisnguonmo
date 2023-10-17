using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBhytParam
{
    partial class HisBhytParamUpdate : EntityBase
    {
        public HisBhytParamUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BHYT_PARAM>();
        }

        private BridgeDAO<HIS_BHYT_PARAM> bridgeDAO;

        public bool Update(HIS_BHYT_PARAM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BHYT_PARAM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
