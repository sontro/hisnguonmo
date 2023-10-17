using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSurgRemuneration
{
    partial class HisSurgRemunerationUpdate : EntityBase
    {
        public HisSurgRemunerationUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SURG_REMUNERATION>();
        }

        private BridgeDAO<HIS_SURG_REMUNERATION> bridgeDAO;

        public bool Update(HIS_SURG_REMUNERATION data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SURG_REMUNERATION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
