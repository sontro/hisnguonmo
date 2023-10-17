using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMaterial
{
    partial class HisMaterialCheck : EntityBase
    {
        public HisMaterialCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL>();
        }

        private BridgeDAO<HIS_MATERIAL> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
