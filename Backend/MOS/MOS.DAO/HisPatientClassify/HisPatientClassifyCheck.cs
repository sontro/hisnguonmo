using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPatientClassify
{
    partial class HisPatientClassifyCheck : EntityBase
    {
        public HisPatientClassifyCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_CLASSIFY>();
        }

        private BridgeDAO<HIS_PATIENT_CLASSIFY> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
