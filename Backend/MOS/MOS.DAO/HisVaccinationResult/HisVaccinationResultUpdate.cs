using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccinationResult
{
    partial class HisVaccinationResultUpdate : EntityBase
    {
        public HisVaccinationResultUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_RESULT>();
        }

        private BridgeDAO<HIS_VACCINATION_RESULT> bridgeDAO;

        public bool Update(HIS_VACCINATION_RESULT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_VACCINATION_RESULT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
