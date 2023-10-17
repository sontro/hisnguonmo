using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisDosageForm
{
    partial class HisDosageFormUpdate : EntityBase
    {
        public HisDosageFormUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_DOSAGE_FORM>();
        }

        private BridgeDAO<HIS_DOSAGE_FORM> bridgeDAO;

        public bool Update(HIS_DOSAGE_FORM data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_DOSAGE_FORM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
