using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMaterialTypeMap
{
    partial class HisMaterialTypeMapUpdate : EntityBase
    {
        public HisMaterialTypeMapUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL_TYPE_MAP>();
        }

        private BridgeDAO<HIS_MATERIAL_TYPE_MAP> bridgeDAO;

        public bool Update(HIS_MATERIAL_TYPE_MAP data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MATERIAL_TYPE_MAP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
