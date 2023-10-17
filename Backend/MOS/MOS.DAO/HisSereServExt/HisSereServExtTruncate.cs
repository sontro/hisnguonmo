using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServExt
{
    partial class HisSereServExtTruncate : EntityBase
    {
        public HisSereServExtTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_EXT>();
        }

        private BridgeDAO<HIS_SERE_SERV_EXT> bridgeDAO;

        public bool Truncate(HIS_SERE_SERV_EXT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERE_SERV_EXT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
