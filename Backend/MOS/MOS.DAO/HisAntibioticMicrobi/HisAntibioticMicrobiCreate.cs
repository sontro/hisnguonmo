using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAntibioticMicrobi
{
    partial class HisAntibioticMicrobiCreate : EntityBase
    {
        public HisAntibioticMicrobiCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIBIOTIC_MICROBI>();
        }

        private BridgeDAO<HIS_ANTIBIOTIC_MICROBI> bridgeDAO;

        public bool Create(HIS_ANTIBIOTIC_MICROBI data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ANTIBIOTIC_MICROBI> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
