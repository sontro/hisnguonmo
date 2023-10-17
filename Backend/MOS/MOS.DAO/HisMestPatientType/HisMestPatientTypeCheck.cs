using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMestPatientType
{
    partial class HisMestPatientTypeCheck : EntityBase
    {
        public HisMestPatientTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEST_PATIENT_TYPE>();
        }

        private BridgeDAO<HIS_MEST_PATIENT_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
