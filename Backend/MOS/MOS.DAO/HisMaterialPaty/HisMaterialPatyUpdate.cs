using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMaterialPaty
{
    partial class HisMaterialPatyUpdate : EntityBase
    {
        public HisMaterialPatyUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL_PATY>();
        }

        private BridgeDAO<HIS_MATERIAL_PATY> bridgeDAO;

        public bool Update(HIS_MATERIAL_PATY data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MATERIAL_PATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
