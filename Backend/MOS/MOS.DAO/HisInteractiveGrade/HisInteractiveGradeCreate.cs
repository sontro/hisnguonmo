using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisInteractiveGrade
{
    partial class HisInteractiveGradeCreate : EntityBase
    {
        public HisInteractiveGradeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_INTERACTIVE_GRADE>();
        }

        private BridgeDAO<HIS_INTERACTIVE_GRADE> bridgeDAO;

        public bool Create(HIS_INTERACTIVE_GRADE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_INTERACTIVE_GRADE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
