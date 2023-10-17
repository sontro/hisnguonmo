using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisAdrMedicineType
{
    partial class HisAdrMedicineTypeCreate : EntityBase
    {
        public HisAdrMedicineTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_ADR_MEDICINE_TYPE>();
        }

        private BridgeDAO<HIS_ADR_MEDICINE_TYPE> bridgeDAO;

        public bool Create(HIS_ADR_MEDICINE_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_ADR_MEDICINE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
