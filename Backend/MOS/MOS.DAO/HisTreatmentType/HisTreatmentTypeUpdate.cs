using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentType
{
    partial class HisTreatmentTypeUpdate : EntityBase
    {
        public HisTreatmentTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_TYPE>();
        }

        private BridgeDAO<HIS_TREATMENT_TYPE> bridgeDAO;

        public bool Update(HIS_TREATMENT_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TREATMENT_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
