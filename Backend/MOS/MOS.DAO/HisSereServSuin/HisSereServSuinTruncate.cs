using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServSuin
{
    partial class HisSereServSuinTruncate : EntityBase
    {
        public HisSereServSuinTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_SUIN>();
        }

        private BridgeDAO<HIS_SERE_SERV_SUIN> bridgeDAO;

        public bool Truncate(HIS_SERE_SERV_SUIN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERE_SERV_SUIN> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
