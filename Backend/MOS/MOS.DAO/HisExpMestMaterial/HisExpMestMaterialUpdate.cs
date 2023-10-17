using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisExpMestMaterial
{
    partial class HisExpMestMaterialUpdate : EntityBase
    {
        public HisExpMestMaterialUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_MATERIAL>();
        }

        private BridgeDAO<HIS_EXP_MEST_MATERIAL> bridgeDAO;

        public bool Update(HIS_EXP_MEST_MATERIAL data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_EXP_MEST_MATERIAL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
