using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineTypeTut
{
    partial class HisMedicineTypeTutCreate : EntityBase
    {
        public HisMedicineTypeTutCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_TYPE_TUT>();
        }

        private BridgeDAO<HIS_MEDICINE_TYPE_TUT> bridgeDAO;

        public bool Create(HIS_MEDICINE_TYPE_TUT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDICINE_TYPE_TUT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
