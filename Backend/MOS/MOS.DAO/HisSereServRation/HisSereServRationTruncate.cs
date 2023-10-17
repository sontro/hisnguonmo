using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSereServRation
{
    partial class HisSereServRationTruncate : EntityBase
    {
        public HisSereServRationTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_RATION>();
        }

        private BridgeDAO<HIS_SERE_SERV_RATION> bridgeDAO;

        public bool Truncate(HIS_SERE_SERV_RATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SERE_SERV_RATION> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
