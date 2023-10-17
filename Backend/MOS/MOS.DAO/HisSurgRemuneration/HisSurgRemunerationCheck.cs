using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisSurgRemuneration
{
    partial class HisSurgRemunerationCheck : EntityBase
    {
        public HisSurgRemunerationCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SURG_REMUNERATION>();
        }

        private BridgeDAO<HIS_SURG_REMUNERATION> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
