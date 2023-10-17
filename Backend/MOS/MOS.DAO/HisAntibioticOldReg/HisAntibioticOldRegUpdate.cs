using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisAntibioticOldReg
{
    partial class HisAntibioticOldRegUpdate : EntityBase
    {
        public HisAntibioticOldRegUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIBIOTIC_OLD_REG>();
        }

        private BridgeDAO<HIS_ANTIBIOTIC_OLD_REG> bridgeDAO;

        public bool Update(HIS_ANTIBIOTIC_OLD_REG data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_ANTIBIOTIC_OLD_REG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
