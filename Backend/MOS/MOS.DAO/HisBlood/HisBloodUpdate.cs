using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBlood
{
    partial class HisBloodUpdate : EntityBase
    {
        public HisBloodUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD>();
        }

        private BridgeDAO<HIS_BLOOD> bridgeDAO;

        public bool Update(HIS_BLOOD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BLOOD> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
