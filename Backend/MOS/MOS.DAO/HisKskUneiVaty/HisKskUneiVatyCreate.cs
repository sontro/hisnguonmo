using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisKskUneiVaty
{
    partial class HisKskUneiVatyCreate : EntityBase
    {
        public HisKskUneiVatyCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_KSK_UNEI_VATY>();
        }

        private BridgeDAO<HIS_KSK_UNEI_VATY> bridgeDAO;

        public bool Create(HIS_KSK_UNEI_VATY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_KSK_UNEI_VATY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
