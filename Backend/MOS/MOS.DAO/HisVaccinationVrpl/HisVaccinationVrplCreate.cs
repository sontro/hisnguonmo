using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccinationVrpl
{
    partial class HisVaccinationVrplCreate : EntityBase
    {
        public HisVaccinationVrplCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_VRPL>();
        }

        private BridgeDAO<HIS_VACCINATION_VRPL> bridgeDAO;

        public bool Create(HIS_VACCINATION_VRPL data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_VACCINATION_VRPL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
