using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMedicineUseForm
{
    partial class HisMedicineUseFormTruncate : EntityBase
    {
        public HisMedicineUseFormTruncate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MEDICINE_USE_FORM>();
        }

        private BridgeDAO<HIS_MEDICINE_USE_FORM> bridgeDAO;

        public bool Truncate(HIS_MEDICINE_USE_FORM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Truncate(data.ID);
        }

        public bool TruncateList(List<HIS_MEDICINE_USE_FORM> listData)
        {
            return IsNotNullOrEmpty(listData) && bridgeDAO.TruncateListRaw(listData);
        }
    }
}
