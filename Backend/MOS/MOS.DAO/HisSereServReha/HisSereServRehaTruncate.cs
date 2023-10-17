using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServReha
{
    partial class HisSereServRehaTruncate : EntityBase
    {
        public HisSereServRehaTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_REHA>();
        }

        private BridgeDAO<HIS_SERE_SERV_REHA> bridgeDAO;

        public bool Truncate(HIS_SERE_SERV_REHA data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERE_SERV_REHA> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
