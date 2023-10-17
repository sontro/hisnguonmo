using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentLogType
{
    partial class HisTreatmentLogTypeCreate : EntityBase
    {
        public HisTreatmentLogTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_LOG_TYPE>();
        }

        private BridgeDAO<HIS_TREATMENT_LOG_TYPE> bridgeDAO;

        public bool Create(HIS_TREATMENT_LOG_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TREATMENT_LOG_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
