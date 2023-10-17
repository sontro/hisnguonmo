using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisImpMestMedicine
{
    partial class HisImpMestMedicineCreate : EntityBase
    {
        public HisImpMestMedicineCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_MEDICINE>();
        }

        private BridgeDAO<HIS_IMP_MEST_MEDICINE> bridgeDAO;

        public bool Create(HIS_IMP_MEST_MEDICINE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_IMP_MEST_MEDICINE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
