using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineMedicine
{
    partial class HisMedicineMedicineCreate : EntityBase
    {
        public HisMedicineMedicineCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_MEDICINE>();
        }

        private BridgeDAO<HIS_MEDICINE_MEDICINE> bridgeDAO;

        public bool Create(HIS_MEDICINE_MEDICINE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDICINE_MEDICINE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
