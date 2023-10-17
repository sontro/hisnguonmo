using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPatientProgram
{
    partial class HisPatientProgramCheck : EntityBase
    {
        public HisPatientProgramCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT_PROGRAM>();
        }

        private BridgeDAO<HIS_PATIENT_PROGRAM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
