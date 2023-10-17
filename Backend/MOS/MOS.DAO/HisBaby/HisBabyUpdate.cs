using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBaby
{
    partial class HisBabyUpdate : EntityBase
    {
        public HisBabyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BABY>();
        }

        private BridgeDAO<HIS_BABY> bridgeDAO;

        public bool Update(HIS_BABY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BABY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
