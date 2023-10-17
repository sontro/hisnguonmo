using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatmentEndTypeExt
{
    partial class HisTreatmentEndTypeExtUpdate : EntityBase
    {
        public HisTreatmentEndTypeExtUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT_END_TYPE_EXT>();
        }

        private BridgeDAO<HIS_TREATMENT_END_TYPE_EXT> bridgeDAO;

        public bool Update(HIS_TREATMENT_END_TYPE_EXT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TREATMENT_END_TYPE_EXT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
