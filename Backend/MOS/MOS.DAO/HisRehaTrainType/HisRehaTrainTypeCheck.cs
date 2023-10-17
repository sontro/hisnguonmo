using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRehaTrainType
{
    partial class HisRehaTrainTypeCheck : EntityBase
    {
        public HisRehaTrainTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REHA_TRAIN_TYPE>();
        }

        private BridgeDAO<HIS_REHA_TRAIN_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
