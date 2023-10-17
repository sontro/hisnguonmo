using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBloodAbo
{
    partial class HisBloodAboUpdate : EntityBase
    {
        public HisBloodAboUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_ABO>();
        }

        private BridgeDAO<HIS_BLOOD_ABO> bridgeDAO;

        public bool Update(HIS_BLOOD_ABO data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BLOOD_ABO> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
