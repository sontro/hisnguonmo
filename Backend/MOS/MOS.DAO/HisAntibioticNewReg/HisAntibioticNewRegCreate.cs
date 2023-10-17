using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAntibioticNewReg
{
    partial class HisAntibioticNewRegCreate : EntityBase
    {
        public HisAntibioticNewRegCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIBIOTIC_NEW_REG>();
        }

        private BridgeDAO<HIS_ANTIBIOTIC_NEW_REG> bridgeDAO;

        public bool Create(HIS_ANTIBIOTIC_NEW_REG data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ANTIBIOTIC_NEW_REG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
