using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAntibioticOldReg
{
    partial class HisAntibioticOldRegCreate : EntityBase
    {
        public HisAntibioticOldRegCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIBIOTIC_OLD_REG>();
        }

        private BridgeDAO<HIS_ANTIBIOTIC_OLD_REG> bridgeDAO;

        public bool Create(HIS_ANTIBIOTIC_OLD_REG data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ANTIBIOTIC_OLD_REG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
