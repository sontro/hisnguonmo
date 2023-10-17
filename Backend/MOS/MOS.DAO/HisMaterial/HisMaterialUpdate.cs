using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMaterial
{
    partial class HisMaterialUpdate : EntityBase
    {
        public HisMaterialUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL>();
        }

        private BridgeDAO<HIS_MATERIAL> bridgeDAO;

        public bool Update(HIS_MATERIAL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MATERIAL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
