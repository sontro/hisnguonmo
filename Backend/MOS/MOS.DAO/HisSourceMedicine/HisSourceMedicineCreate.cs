using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisSourceMedicine
{
    partial class HisSourceMedicineCreate : EntityBase
    {
        public HisSourceMedicineCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SOURCE_MEDICINE>();
        }

        private BridgeDAO<HIS_SOURCE_MEDICINE> bridgeDAO;

        public bool Create(HIS_SOURCE_MEDICINE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_SOURCE_MEDICINE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
