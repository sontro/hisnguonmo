using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisQcType
{
    partial class HisQcTypeCreate : EntityBase
    {
        public HisQcTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_QC_TYPE>();
        }

        private BridgeDAO<HIS_QC_TYPE> bridgeDAO;

        public bool Create(HIS_QC_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_QC_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
