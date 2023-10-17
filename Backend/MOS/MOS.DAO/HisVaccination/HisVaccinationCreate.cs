using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccination
{
    partial class HisVaccinationCreate : EntityBase
    {
        public HisVaccinationCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION>();
        }

        private BridgeDAO<HIS_VACCINATION> bridgeDAO;

        public bool Create(HIS_VACCINATION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_VACCINATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
