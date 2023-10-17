using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisTreatment
{
    partial class HisTreatmentCheck : EntityBase
    {
        public HisTreatmentCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT>();
        }

        private BridgeDAO<HIS_TREATMENT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
