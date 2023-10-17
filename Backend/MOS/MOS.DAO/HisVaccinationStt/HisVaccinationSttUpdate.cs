using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisVaccinationStt
{
    partial class HisVaccinationSttUpdate : EntityBase
    {
        public HisVaccinationSttUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_STT>();
        }

        private BridgeDAO<HIS_VACCINATION_STT> bridgeDAO;

        public bool Update(HIS_VACCINATION_STT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_VACCINATION_STT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
