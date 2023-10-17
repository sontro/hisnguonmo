using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSereServReha
{
    partial class HisSereServRehaCheck : EntityBase
    {
        public HisSereServRehaCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_REHA>();
        }

        private BridgeDAO<HIS_SERE_SERV_REHA> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
