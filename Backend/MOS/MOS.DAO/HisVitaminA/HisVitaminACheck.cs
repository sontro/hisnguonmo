using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisVitaminA
{
    partial class HisVitaminACheck : EntityBase
    {
        public HisVitaminACheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VITAMIN_A>();
        }

        private BridgeDAO<HIS_VITAMIN_A> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
