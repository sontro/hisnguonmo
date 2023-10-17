using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccinationReact
{
    partial class HisVaccinationReactUpdate : EntityBase
    {
        public HisVaccinationReactUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_REACT>();
        }

        private BridgeDAO<HIS_VACCINATION_REACT> bridgeDAO;

        public bool Update(HIS_VACCINATION_REACT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_VACCINATION_REACT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
