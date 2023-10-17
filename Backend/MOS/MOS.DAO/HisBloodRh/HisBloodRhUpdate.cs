using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBloodRh
{
    partial class HisBloodRhUpdate : EntityBase
    {
        public HisBloodRhUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_RH>();
        }

        private BridgeDAO<HIS_BLOOD_RH> bridgeDAO;

        public bool Update(HIS_BLOOD_RH data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BLOOD_RH> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
