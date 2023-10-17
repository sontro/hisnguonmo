using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBloodAbo
{
    partial class HisBloodAboTruncate : EntityBase
    {
        public HisBloodAboTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BLOOD_ABO>();
        }

        private BridgeDAO<HIS_BLOOD_ABO> bridgeDAO;

        public bool Truncate(HIS_BLOOD_ABO data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_BLOOD_ABO> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
