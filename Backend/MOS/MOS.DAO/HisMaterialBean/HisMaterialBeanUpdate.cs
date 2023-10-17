using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisMaterialBean
{
    partial class HisMaterialBeanUpdate : EntityBase
    {
        public HisMaterialBeanUpdate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MATERIAL_BEAN>();
        }

        private BridgeDAO<HIS_MATERIAL_BEAN> bridgeDAO;

        public bool Update(HIS_MATERIAL_BEAN data)
        {
            return IsNotNull(data) && IsGreaterThanZero(data.ID) && bridgeDAO.Update(data);
        }

        public bool UpdateList(List<HIS_MATERIAL_BEAN> listData)
        {
            return IsNotNull(listData) && bridgeDAO.UpdateList(listData);
        }
    }
}
