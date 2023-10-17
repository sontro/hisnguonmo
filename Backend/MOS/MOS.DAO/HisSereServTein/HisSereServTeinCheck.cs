using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSereServTein
{
    partial class HisSereServTeinCheck : EntityBase
    {
        public HisSereServTeinCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_TEIN>();
        }

        private BridgeDAO<HIS_SERE_SERV_TEIN> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
