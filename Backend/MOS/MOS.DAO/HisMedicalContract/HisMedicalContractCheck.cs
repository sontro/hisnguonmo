using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMedicalContract
{
    partial class HisMedicalContractCheck : EntityBase
    {
        public HisMedicalContractCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICAL_CONTRACT>();
        }

        private BridgeDAO<HIS_MEDICAL_CONTRACT> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
