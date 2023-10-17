using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisVaccinationExam
{
    partial class HisVaccinationExamCreate : EntityBase
    {
        public HisVaccinationExamCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_VACCINATION_EXAM>();
        }

        private BridgeDAO<HIS_VACCINATION_EXAM> bridgeDAO;

        public bool Create(HIS_VACCINATION_EXAM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_VACCINATION_EXAM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
