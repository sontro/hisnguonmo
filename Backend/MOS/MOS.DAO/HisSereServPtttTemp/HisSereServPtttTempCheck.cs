using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSereServPtttTemp
{
    partial class HisSereServPtttTempCheck : EntityBase
    {
        public HisSereServPtttTempCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SERE_SERV_PTTT_TEMP>();
        }

        private BridgeDAO<HIS_SERE_SERV_PTTT_TEMP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
