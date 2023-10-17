using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisMaterialBean
{
    partial class HisMaterialBeanCheck : EntityBase
    {
        public HisMaterialBeanCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL_BEAN>();
        }

        private BridgeDAO<HIS_MATERIAL_BEAN> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
