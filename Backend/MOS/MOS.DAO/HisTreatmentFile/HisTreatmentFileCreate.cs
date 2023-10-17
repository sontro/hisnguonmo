using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentFile
{
    partial class HisTreatmentFileCreate : EntityBase
    {
        public HisTreatmentFileCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_FILE>();
        }

        private BridgeDAO<HIS_TREATMENT_FILE> bridgeDAO;

        public bool Create(HIS_TREATMENT_FILE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TREATMENT_FILE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
