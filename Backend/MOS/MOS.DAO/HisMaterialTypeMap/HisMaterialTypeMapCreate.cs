using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMaterialTypeMap
{
    partial class HisMaterialTypeMapCreate : EntityBase
    {
        public HisMaterialTypeMapCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL_TYPE_MAP>();
        }

        private BridgeDAO<HIS_MATERIAL_TYPE_MAP> bridgeDAO;

        public bool Create(HIS_MATERIAL_TYPE_MAP data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MATERIAL_TYPE_MAP> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
