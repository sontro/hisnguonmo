using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSereServPttt
{
    partial class HisSereServPtttCheck : EntityBase
    {
        public HisSereServPtttCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_PTTT>();
        }

        private BridgeDAO<HIS_SERE_SERV_PTTT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
