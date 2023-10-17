using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccinationVrty
{
    partial class HisVaccinationVrtyCreate : EntityBase
    {
        public HisVaccinationVrtyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_VRTY>();
        }

        private BridgeDAO<HIS_VACCINATION_VRTY> bridgeDAO;

        public bool Create(HIS_VACCINATION_VRTY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_VACCINATION_VRTY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
