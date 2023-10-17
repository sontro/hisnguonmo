using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisTransfusionSum
{
    partial class HisTransfusionSumCreate : EntityBase
    {
        public HisTransfusionSumCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_TRANSFUSION_SUM>();
        }

        private BridgeDAO<HIS_TRANSFUSION_SUM> bridgeDAO;

        public bool Create(HIS_TRANSFUSION_SUM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_TRANSFUSION_SUM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
