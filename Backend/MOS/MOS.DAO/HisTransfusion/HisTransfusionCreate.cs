using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTransfusion
{
    partial class HisTransfusionCreate : EntityBase
    {
        public HisTransfusionCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSFUSION>();
        }

        private BridgeDAO<HIS_TRANSFUSION> bridgeDAO;

        public bool Create(HIS_TRANSFUSION data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TRANSFUSION> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
