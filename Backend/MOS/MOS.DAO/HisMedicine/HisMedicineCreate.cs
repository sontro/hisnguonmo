using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicine
{
    partial class HisMedicineCreate : EntityBase
    {
        public HisMedicineCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE>();
        }

        private BridgeDAO<HIS_MEDICINE> bridgeDAO;

        public bool Create(HIS_MEDICINE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDICINE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
