using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisBidMaterialType
{
    partial class HisBidMaterialTypeUpdate : EntityBase
    {
        public HisBidMaterialTypeUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BID_MATERIAL_TYPE>();
        }

        private BridgeDAO<HIS_BID_MATERIAL_TYPE> bridgeDAO;

        public bool Update(HIS_BID_MATERIAL_TYPE data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_BID_MATERIAL_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
