using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServTemp
{
    partial class HisSereServTempTruncate : EntityBase
    {
        public HisSereServTempTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_TEMP>();
        }

        private BridgeDAO<HIS_SERE_SERV_TEMP> bridgeDAO;

        public bool Truncate(HIS_SERE_SERV_TEMP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERE_SERV_TEMP> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
