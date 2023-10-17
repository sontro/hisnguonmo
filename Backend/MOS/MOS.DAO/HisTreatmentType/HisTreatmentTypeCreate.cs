using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentType
{
    partial class HisTreatmentTypeCreate : EntityBase
    {
        public HisTreatmentTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_TYPE>();
        }

        private BridgeDAO<HIS_TREATMENT_TYPE> bridgeDAO;

        public bool Create(HIS_TREATMENT_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TREATMENT_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
