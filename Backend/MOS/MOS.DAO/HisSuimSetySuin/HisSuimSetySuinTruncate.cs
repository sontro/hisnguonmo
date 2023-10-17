using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSuimSetySuin
{
    partial class HisSuimSetySuinTruncate : EntityBase
    {
        public HisSuimSetySuinTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SUIM_SETY_SUIN>();
        }

        private BridgeDAO<HIS_SUIM_SETY_SUIN> bridgeDAO;

        public bool Truncate(HIS_SUIM_SETY_SUIN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SUIM_SETY_SUIN> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
