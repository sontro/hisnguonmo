using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisFormTypeCfgData
{
    partial class HisFormTypeCfgDataCheck : EntityBase
    {
        public HisFormTypeCfgDataCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FORM_TYPE_CFG_DATA>();
        }

        private BridgeDAO<HIS_FORM_TYPE_CFG_DATA> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
