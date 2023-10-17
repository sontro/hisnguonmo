using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccinationVrpl
{
    partial class HisVaccinationVrplUpdate : EntityBase
    {
        public HisVaccinationVrplUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_VRPL>();
        }

        private BridgeDAO<HIS_VACCINATION_VRPL> bridgeDAO;

        public bool Update(HIS_VACCINATION_VRPL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_VACCINATION_VRPL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
