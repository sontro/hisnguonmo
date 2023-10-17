using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEmrCoverType
{
    partial class HisEmrCoverTypeCreate : EntityBase
    {
        public HisEmrCoverTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMR_COVER_TYPE>();
        }

        private BridgeDAO<HIS_EMR_COVER_TYPE> bridgeDAO;

        public bool Create(HIS_EMR_COVER_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EMR_COVER_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
