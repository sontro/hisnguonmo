using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisPatient
{
    partial class HisPatientCheck : EntityBase
    {
        public HisPatientCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PATIENT>();
        }

        private BridgeDAO<HIS_PATIENT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
