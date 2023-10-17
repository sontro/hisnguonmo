using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisMrCheckSummary
{
    partial class HisMrCheckSummaryCreate : EntityBase
    {
        public HisMrCheckSummaryCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_MR_CHECK_SUMMARY>();
        }

        private BridgeDAO<HIS_MR_CHECK_SUMMARY> bridgeDAO;

        public bool Create(HIS_MR_CHECK_SUMMARY data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_MR_CHECK_SUMMARY> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
