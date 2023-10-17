using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSereServRation
{
    partial class HisSereServRationCheck : EntityBase
    {
        public HisSereServRationCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_RATION>();
        }

        private BridgeDAO<HIS_SERE_SERV_RATION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
