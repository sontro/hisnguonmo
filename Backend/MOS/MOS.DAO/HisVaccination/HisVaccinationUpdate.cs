using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccination
{
    partial class HisVaccinationUpdate : EntityBase
    {
        public HisVaccinationUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION>();
        }

        private BridgeDAO<HIS_VACCINATION> bridgeDAO;

        public bool Update(HIS_VACCINATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_VACCINATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
