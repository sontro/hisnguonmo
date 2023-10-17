using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRehaTrainUnit
{
    partial class HisRehaTrainUnitTruncate : EntityBase
    {
        public HisRehaTrainUnitTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REHA_TRAIN_UNIT>();
        }

        private BridgeDAO<HIS_REHA_TRAIN_UNIT> bridgeDAO;

        public bool Truncate(HIS_REHA_TRAIN_UNIT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_REHA_TRAIN_UNIT> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
