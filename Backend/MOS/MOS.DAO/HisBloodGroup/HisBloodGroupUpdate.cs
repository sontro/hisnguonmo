using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBloodGroup
{
    partial class HisBloodGroupUpdate : EntityBase
    {
        public HisBloodGroupUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_GROUP>();
        }

        private BridgeDAO<HIS_BLOOD_GROUP> bridgeDAO;

        public bool Update(HIS_BLOOD_GROUP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BLOOD_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
