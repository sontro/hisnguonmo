using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRehaTrainType
{
    partial class HisRehaTrainTypeUpdate : EntityBase
    {
        public HisRehaTrainTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REHA_TRAIN_TYPE>();
        }

        private BridgeDAO<HIS_REHA_TRAIN_TYPE> bridgeDAO;

        public bool Update(HIS_REHA_TRAIN_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_REHA_TRAIN_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
