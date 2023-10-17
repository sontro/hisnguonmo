using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccineType
{
    partial class HisVaccineTypeUpdate : EntityBase
    {
        public HisVaccineTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINE_TYPE>();
        }

        private BridgeDAO<HIS_VACCINE_TYPE> bridgeDAO;

        public bool Update(HIS_VACCINE_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_VACCINE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
