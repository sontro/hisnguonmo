using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisFormTypeCfgData
{
    partial class HisFormTypeCfgDataCreate : EntityBase
    {
        public HisFormTypeCfgDataCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FORM_TYPE_CFG_DATA>();
        }

        private BridgeDAO<HIS_FORM_TYPE_CFG_DATA> bridgeDAO;

        public bool Create(HIS_FORM_TYPE_CFG_DATA data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_FORM_TYPE_CFG_DATA> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
