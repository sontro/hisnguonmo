using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineUseForm
{
    partial class HisMedicineUseFormUpdate : EntityBase
    {
        public HisMedicineUseFormUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_USE_FORM>();
        }

        private BridgeDAO<HIS_MEDICINE_USE_FORM> bridgeDAO;

        public bool Update(HIS_MEDICINE_USE_FORM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MEDICINE_USE_FORM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
