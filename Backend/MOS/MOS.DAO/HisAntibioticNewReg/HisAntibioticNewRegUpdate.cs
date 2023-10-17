using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAntibioticNewReg
{
    partial class HisAntibioticNewRegUpdate : EntityBase
    {
        public HisAntibioticNewRegUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIBIOTIC_NEW_REG>();
        }

        private BridgeDAO<HIS_ANTIBIOTIC_NEW_REG> bridgeDAO;

        public bool Update(HIS_ANTIBIOTIC_NEW_REG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ANTIBIOTIC_NEW_REG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
