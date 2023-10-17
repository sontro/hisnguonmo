using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisEyeSurgryDesc
{
    partial class HisEyeSurgryDescTruncate : EntityBase
    {
        public HisEyeSurgryDescTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EYE_SURGRY_DESC>();
        }

        private BridgeDAO<HIS_EYE_SURGRY_DESC> bridgeDAO;

        public bool Truncate(HIS_EYE_SURGRY_DESC data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_EYE_SURGRY_DESC> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
