using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEmteMedicineType
{
    partial class HisEmteMedicineTypeCreate : EntityBase
    {
        public HisEmteMedicineTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMTE_MEDICINE_TYPE>();
        }

        private BridgeDAO<HIS_EMTE_MEDICINE_TYPE> bridgeDAO;

        public bool Create(HIS_EMTE_MEDICINE_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EMTE_MEDICINE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
