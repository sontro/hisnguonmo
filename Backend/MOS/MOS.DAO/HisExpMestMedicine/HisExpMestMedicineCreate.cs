using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestMedicine
{
    partial class HisExpMestMedicineCreate : EntityBase
    {
        public HisExpMestMedicineCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_MEDICINE>();
        }

        private BridgeDAO<HIS_EXP_MEST_MEDICINE> bridgeDAO;

        public bool Create(HIS_EXP_MEST_MEDICINE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXP_MEST_MEDICINE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
