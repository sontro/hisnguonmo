using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccinationResult
{
    partial class HisVaccinationResultCreate : EntityBase
    {
        public HisVaccinationResultCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_RESULT>();
        }

        private BridgeDAO<HIS_VACCINATION_RESULT> bridgeDAO;

        public bool Create(HIS_VACCINATION_RESULT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_VACCINATION_RESULT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
