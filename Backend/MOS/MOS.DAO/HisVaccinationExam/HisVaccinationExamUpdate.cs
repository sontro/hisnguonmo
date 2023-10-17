using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccinationExam
{
    partial class HisVaccinationExamUpdate : EntityBase
    {
        public HisVaccinationExamUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_EXAM>();
        }

        private BridgeDAO<HIS_VACCINATION_EXAM> bridgeDAO;

        public bool Update(HIS_VACCINATION_EXAM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_VACCINATION_EXAM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
