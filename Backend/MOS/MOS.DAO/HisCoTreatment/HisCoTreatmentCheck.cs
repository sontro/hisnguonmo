using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisCoTreatment
{
    partial class HisCoTreatmentCheck : EntityBase
    {
        public HisCoTreatmentCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CO_TREATMENT>();
        }

        private BridgeDAO<HIS_CO_TREATMENT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
