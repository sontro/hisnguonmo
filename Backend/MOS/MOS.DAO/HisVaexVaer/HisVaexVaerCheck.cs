using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisVaexVaer
{
    partial class HisVaexVaerCheck : EntityBase
    {
        public HisVaexVaerCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VAEX_VAER>();
        }

        private BridgeDAO<HIS_VAEX_VAER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
