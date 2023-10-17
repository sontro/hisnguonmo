using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisRehaTrain
{
    partial class HisRehaTrainCheck : EntityBase
    {
        public HisRehaTrainCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REHA_TRAIN>();
        }

        private BridgeDAO<HIS_REHA_TRAIN> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
