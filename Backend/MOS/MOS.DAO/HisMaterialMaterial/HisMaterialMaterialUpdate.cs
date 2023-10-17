using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMaterialMaterial
{
    partial class HisMaterialMaterialUpdate : EntityBase
    {
        public HisMaterialMaterialUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL_MATERIAL>();
        }

        private BridgeDAO<HIS_MATERIAL_MATERIAL> bridgeDAO;

        public bool Update(HIS_MATERIAL_MATERIAL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MATERIAL_MATERIAL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
