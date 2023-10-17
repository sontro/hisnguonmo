using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisReportTypeCat
{
    partial class HisReportTypeCatCreate : EntityBase
    {
        public HisReportTypeCatCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_REPORT_TYPE_CAT>();
        }

        private BridgeDAO<HIS_REPORT_TYPE_CAT> bridgeDAO;

        public bool Create(HIS_REPORT_TYPE_CAT data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_REPORT_TYPE_CAT> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
