using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMaterialBean
{
    partial class HisMaterialBeanCreate : EntityBase
    {
        public HisMaterialBeanCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL_BEAN>();
        }

        private BridgeDAO<HIS_MATERIAL_BEAN> bridgeDAO;

        public bool Create(HIS_MATERIAL_BEAN data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MATERIAL_BEAN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
