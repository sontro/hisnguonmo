using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisImpMestMaterial
{
    partial class HisImpMestMaterialUpdate : EntityBase
    {
        public HisImpMestMaterialUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_IMP_MEST_MATERIAL>();
        }

        private BridgeDAO<HIS_IMP_MEST_MATERIAL> bridgeDAO;

        public bool Update(HIS_IMP_MEST_MATERIAL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_IMP_MEST_MATERIAL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
