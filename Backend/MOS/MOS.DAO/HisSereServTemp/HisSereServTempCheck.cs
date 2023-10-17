using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSereServTemp
{
    partial class HisSereServTempCheck : EntityBase
    {
        public HisSereServTempCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_TEMP>();
        }

        private BridgeDAO<HIS_SERE_SERV_TEMP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
