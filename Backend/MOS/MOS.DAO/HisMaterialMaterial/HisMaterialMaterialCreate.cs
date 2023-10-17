using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMaterialMaterial
{
    partial class HisMaterialMaterialCreate : EntityBase
    {
        public HisMaterialMaterialCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL_MATERIAL>();
        }

        private BridgeDAO<HIS_MATERIAL_MATERIAL> bridgeDAO;

        public bool Create(HIS_MATERIAL_MATERIAL data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MATERIAL_MATERIAL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
