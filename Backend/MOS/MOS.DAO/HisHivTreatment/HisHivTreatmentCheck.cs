using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisHivTreatment
{
    partial class HisHivTreatmentCheck : EntityBase
    {
        public HisHivTreatmentCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_HIV_TREATMENT>();
        }

        private BridgeDAO<HIS_HIV_TREATMENT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
