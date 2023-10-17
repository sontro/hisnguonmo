using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServ
{
    partial class HisSereServTruncate : EntityBase
    {
        public HisSereServTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV>();
        }

        private BridgeDAO<HIS_SERE_SERV> bridgeDAO;

        public bool Truncate(HIS_SERE_SERV data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERE_SERV> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
