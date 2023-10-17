using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServPttt
{
    partial class HisSereServPtttTruncate : EntityBase
    {
        public HisSereServPtttTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_PTTT>();
        }

        private BridgeDAO<HIS_SERE_SERV_PTTT> bridgeDAO;

        public bool Truncate(HIS_SERE_SERV_PTTT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERE_SERV_PTTT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
