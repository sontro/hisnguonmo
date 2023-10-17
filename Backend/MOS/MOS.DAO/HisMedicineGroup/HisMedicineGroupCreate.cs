using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineGroup
{
    partial class HisMedicineGroupCreate : EntityBase
    {
        public HisMedicineGroupCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_GROUP>();
        }

        private BridgeDAO<HIS_MEDICINE_GROUP> bridgeDAO;

        public bool Create(HIS_MEDICINE_GROUP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDICINE_GROUP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
