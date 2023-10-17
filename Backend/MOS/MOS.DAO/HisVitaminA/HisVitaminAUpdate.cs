using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVitaminA
{
    partial class HisVitaminAUpdate : EntityBase
    {
        public HisVitaminAUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VITAMIN_A>();
        }

        private BridgeDAO<HIS_VITAMIN_A> bridgeDAO;

        public bool Update(HIS_VITAMIN_A data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_VITAMIN_A> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
