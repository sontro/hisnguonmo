using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTranPatiTech
{
    partial class HisTranPatiTechCheck : EntityBase
    {
        public HisTranPatiTechCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRAN_PATI_TECH>();
        }

        private BridgeDAO<HIS_TRAN_PATI_TECH> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
