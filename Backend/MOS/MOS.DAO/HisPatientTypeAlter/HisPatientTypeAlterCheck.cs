using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPatientTypeAlter
{
    partial class HisPatientTypeAlterCheck : EntityBase
    {
        public HisPatientTypeAlterCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_TYPE_ALTER>();
        }

        private BridgeDAO<HIS_PATIENT_TYPE_ALTER> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
