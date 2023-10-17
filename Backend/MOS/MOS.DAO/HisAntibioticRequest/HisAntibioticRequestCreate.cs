using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAntibioticRequest
{
    partial class HisAntibioticRequestCreate : EntityBase
    {
        public HisAntibioticRequestCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ANTIBIOTIC_REQUEST>();
        }

        private BridgeDAO<HIS_ANTIBIOTIC_REQUEST> bridgeDAO;

        public bool Create(HIS_ANTIBIOTIC_REQUEST data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ANTIBIOTIC_REQUEST> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
