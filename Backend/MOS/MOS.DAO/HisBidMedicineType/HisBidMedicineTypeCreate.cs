using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBidMedicineType
{
    partial class HisBidMedicineTypeCreate : EntityBase
    {
        public HisBidMedicineTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BID_MEDICINE_TYPE>();
        }

        private BridgeDAO<HIS_BID_MEDICINE_TYPE> bridgeDAO;

        public bool Create(HIS_BID_MEDICINE_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BID_MEDICINE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
