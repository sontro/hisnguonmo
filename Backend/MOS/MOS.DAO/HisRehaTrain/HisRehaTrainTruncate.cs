using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRehaTrain
{
    partial class HisRehaTrainTruncate : EntityBase
    {
        public HisRehaTrainTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REHA_TRAIN>();
        }

        private BridgeDAO<HIS_REHA_TRAIN> bridgeDAO;

        public bool Truncate(HIS_REHA_TRAIN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_REHA_TRAIN> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
