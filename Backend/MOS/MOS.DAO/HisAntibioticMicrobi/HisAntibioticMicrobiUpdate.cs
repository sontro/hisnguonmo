using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAntibioticMicrobi
{
    partial class HisAntibioticMicrobiUpdate : EntityBase
    {
        public HisAntibioticMicrobiUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIBIOTIC_MICROBI>();
        }

        private BridgeDAO<HIS_ANTIBIOTIC_MICROBI> bridgeDAO;

        public bool Update(HIS_ANTIBIOTIC_MICROBI data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ANTIBIOTIC_MICROBI> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
