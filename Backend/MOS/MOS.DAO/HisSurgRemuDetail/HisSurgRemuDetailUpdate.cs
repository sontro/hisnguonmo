using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisSurgRemuDetail
{
    partial class HisSurgRemuDetailUpdate : EntityBase
    {
        public HisSurgRemuDetailUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_SURG_REMU_DETAIL>();
        }

        private BridgeDAO<HIS_SURG_REMU_DETAIL> bridgeDAO;

        public bool Update(HIS_SURG_REMU_DETAIL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_SURG_REMU_DETAIL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
