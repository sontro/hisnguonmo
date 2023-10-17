using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRehaTrainType
{
    partial class HisRehaTrainTypeTruncate : EntityBase
    {
        public HisRehaTrainTypeTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REHA_TRAIN_TYPE>();
        }

        private BridgeDAO<HIS_REHA_TRAIN_TYPE> bridgeDAO;

        public bool Truncate(HIS_REHA_TRAIN_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_REHA_TRAIN_TYPE> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
