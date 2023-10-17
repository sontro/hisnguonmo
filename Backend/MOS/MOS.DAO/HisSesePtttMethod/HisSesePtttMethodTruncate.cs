using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSesePtttMethod
{
    partial class HisSesePtttMethodTruncate : EntityBase
    {
        public HisSesePtttMethodTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SESE_PTTT_METHOD>();
        }

        private BridgeDAO<HIS_SESE_PTTT_METHOD> bridgeDAO;

        public bool Truncate(HIS_SESE_PTTT_METHOD data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_SESE_PTTT_METHOD> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
