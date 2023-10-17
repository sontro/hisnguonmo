using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisTreatment
{
    partial class HisTreatmentUpdate : EntityBase
    {
        public HisTreatmentUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TREATMENT>();
        }

        private BridgeDAO<HIS_TREATMENT> bridgeDAO;

        public bool Update(HIS_TREATMENT data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_TREATMENT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
