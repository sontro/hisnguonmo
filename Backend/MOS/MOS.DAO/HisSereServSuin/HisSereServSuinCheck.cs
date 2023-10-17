using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSereServSuin
{
    partial class HisSereServSuinCheck : EntityBase
    {
        public HisSereServSuinCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_SUIN>();
        }

        private BridgeDAO<HIS_SERE_SERV_SUIN> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
