using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccinationVrty
{
    partial class HisVaccinationVrtyUpdate : EntityBase
    {
        public HisVaccinationVrtyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_VRTY>();
        }

        private BridgeDAO<HIS_VACCINATION_VRTY> bridgeDAO;

        public bool Update(HIS_VACCINATION_VRTY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_VACCINATION_VRTY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
