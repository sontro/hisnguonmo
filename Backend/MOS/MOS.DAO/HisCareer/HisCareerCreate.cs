using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisCareer
{
    partial class HisCareerCreate : EntityBase
    {
        public HisCareerCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_CAREER>();
        }

        private BridgeDAO<HIS_CAREER> bridgeDAO;

        public bool Create(HIS_CAREER data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_CAREER> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
