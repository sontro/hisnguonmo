using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisVaccinationExam
{
    partial class HisVaccinationExamCheck : EntityBase
    {
        public HisVaccinationExamCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_EXAM>();
        }

        private BridgeDAO<HIS_VACCINATION_EXAM> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
