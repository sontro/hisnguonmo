using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSkinSurgeryDesc
{
    partial class HisSkinSurgeryDescTruncate : EntityBase
    {
        public HisSkinSurgeryDescTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SKIN_SURGERY_DESC>();
        }

        private BridgeDAO<HIS_SKIN_SURGERY_DESC> bridgeDAO;

        public bool Truncate(HIS_SKIN_SURGERY_DESC data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SKIN_SURGERY_DESC> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
