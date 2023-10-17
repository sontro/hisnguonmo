using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMedicineUseForm
{
    partial class HisMedicineUseFormCreate : EntityBase
    {
        public HisMedicineUseFormCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_USE_FORM>();
        }

        private BridgeDAO<HIS_MEDICINE_USE_FORM> bridgeDAO;

        public bool Create(HIS_MEDICINE_USE_FORM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MEDICINE_USE_FORM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
