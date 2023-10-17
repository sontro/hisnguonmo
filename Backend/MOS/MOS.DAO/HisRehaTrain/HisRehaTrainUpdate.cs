using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisRehaTrain
{
    partial class HisRehaTrainUpdate : EntityBase
    {
        public HisRehaTrainUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REHA_TRAIN>();
        }

        private BridgeDAO<HIS_REHA_TRAIN> bridgeDAO;

        public bool Update(HIS_REHA_TRAIN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_REHA_TRAIN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
