using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMixedMedicine
{
    partial class HisMixedMedicineCreate : EntityBase
    {
        public HisMixedMedicineCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MIXED_MEDICINE>();
        }

        private BridgeDAO<HIS_MIXED_MEDICINE> bridgeDAO;

        public bool Create(HIS_MIXED_MEDICINE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MIXED_MEDICINE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
