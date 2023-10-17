using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccinationReact
{
    partial class HisVaccinationReactCreate : EntityBase
    {
        public HisVaccinationReactCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_REACT>();
        }

        private BridgeDAO<HIS_VACCINATION_REACT> bridgeDAO;

        public bool Create(HIS_VACCINATION_REACT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_VACCINATION_REACT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
