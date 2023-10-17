using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisExeServiceModule
{
    partial class HisExeServiceModuleCheck : EntityBase
    {
        public HisExeServiceModuleCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXE_SERVICE_MODULE>();
        }

        private BridgeDAO<HIS_EXE_SERVICE_MODULE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
