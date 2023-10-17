using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRehaTrainUnit
{
    partial class HisRehaTrainUnitUpdate : EntityBase
    {
        public HisRehaTrainUnitUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REHA_TRAIN_UNIT>();
        }

        private BridgeDAO<HIS_REHA_TRAIN_UNIT> bridgeDAO;

        public bool Update(HIS_REHA_TRAIN_UNIT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_REHA_TRAIN_UNIT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
