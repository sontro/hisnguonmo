using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisFormTypeCfg
{
    partial class HisFormTypeCfgCreate : EntityBase
    {
        public HisFormTypeCfgCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FORM_TYPE_CFG>();
        }

        private BridgeDAO<HIS_FORM_TYPE_CFG> bridgeDAO;

        public bool Create(HIS_FORM_TYPE_CFG data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_FORM_TYPE_CFG> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
