using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTranPatiTemp
{
    partial class HisTranPatiTempCheck : EntityBase
    {
        public HisTranPatiTempCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRAN_PATI_TEMP>();
        }

        private BridgeDAO<HIS_TRAN_PATI_TEMP> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
