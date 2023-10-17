using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisDiimType
{
    partial class HisDiimTypeCheck : EntityBase
    {
        public HisDiimTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DIIM_TYPE>();
        }

        private BridgeDAO<HIS_DIIM_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
