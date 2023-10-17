using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPatientCase
{
    partial class HisPatientCaseCheck : EntityBase
    {
        public HisPatientCaseCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_CASE>();
        }

        private BridgeDAO<HIS_PATIENT_CASE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
