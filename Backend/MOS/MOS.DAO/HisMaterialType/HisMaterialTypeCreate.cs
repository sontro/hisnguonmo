using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMaterialType
{
    partial class HisMaterialTypeCreate : EntityBase
    {
        public HisMaterialTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL_TYPE>();
        }

        private BridgeDAO<HIS_MATERIAL_TYPE> bridgeDAO;

        public bool Create(HIS_MATERIAL_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MATERIAL_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
