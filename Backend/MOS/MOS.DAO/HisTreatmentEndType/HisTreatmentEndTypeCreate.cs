using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentEndType
{
    partial class HisTreatmentEndTypeCreate : EntityBase
    {
        public HisTreatmentEndTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_END_TYPE>();
        }

        private BridgeDAO<HIS_TREATMENT_END_TYPE> bridgeDAO;

        public bool Create(HIS_TREATMENT_END_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TREATMENT_END_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
