using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisExpMestMaterial
{
    partial class HisExpMestMaterialCreate : EntityBase
    {
        public HisExpMestMaterialCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EXP_MEST_MATERIAL>();
        }

        private BridgeDAO<HIS_EXP_MEST_MATERIAL> bridgeDAO;

        public bool Create(HIS_EXP_MEST_MATERIAL data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EXP_MEST_MATERIAL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
