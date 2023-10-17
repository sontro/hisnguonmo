using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTreatmentUnlimit
{
    partial class HisTreatmentUnlimitCreate : EntityBase
    {
        public HisTreatmentUnlimitCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_UNLIMIT>();
        }

        private BridgeDAO<HIS_TREATMENT_UNLIMIT> bridgeDAO;

        public bool Create(HIS_TREATMENT_UNLIMIT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TREATMENT_UNLIMIT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
