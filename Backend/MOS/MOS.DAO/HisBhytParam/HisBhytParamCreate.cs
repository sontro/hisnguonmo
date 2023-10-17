using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisBhytParam
{
    partial class HisBhytParamCreate : EntityBase
    {
        public HisBhytParamCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_BHYT_PARAM>();
        }

        private BridgeDAO<HIS_BHYT_PARAM> bridgeDAO;

        public bool Create(HIS_BHYT_PARAM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_BHYT_PARAM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
