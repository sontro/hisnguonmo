using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServMaty
{
    partial class HisSereServMatyTruncate : EntityBase
    {
        public HisSereServMatyTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_MATY>();
        }

        private BridgeDAO<HIS_SERE_SERV_MATY> bridgeDAO;

        public bool Truncate(HIS_SERE_SERV_MATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERE_SERV_MATY> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
