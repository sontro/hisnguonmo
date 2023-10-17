using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRehaTrainUnit
{
    partial class HisRehaTrainUnitCheck : EntityBase
    {
        public HisRehaTrainUnitCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REHA_TRAIN_UNIT>();
        }

        private BridgeDAO<HIS_REHA_TRAIN_UNIT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
