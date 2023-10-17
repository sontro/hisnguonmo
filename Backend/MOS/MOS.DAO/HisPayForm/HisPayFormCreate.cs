using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisPayForm
{
    partial class HisPayFormCreate : EntityBase
    {
        public HisPayFormCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_PAY_FORM>();
        }

        private BridgeDAO<HIS_PAY_FORM> bridgeDAO;

        public bool Create(HIS_PAY_FORM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_PAY_FORM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
