using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSereServ
{
    partial class HisSereServCheck : EntityBase
    {
        public HisSereServCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV>();
        }

        private BridgeDAO<HIS_SERE_SERV> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
