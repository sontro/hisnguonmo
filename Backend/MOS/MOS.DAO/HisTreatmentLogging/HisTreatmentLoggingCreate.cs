using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentLogging
{
    partial class HisTreatmentLoggingCreate : EntityBase
    {
        public HisTreatmentLoggingCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_LOGGING>();
        }

        private BridgeDAO<HIS_TREATMENT_LOGGING> bridgeDAO;

        public bool Create(HIS_TREATMENT_LOGGING data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TREATMENT_LOGGING> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
