using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisEmrForm
{
    partial class HisEmrFormCreate : EntityBase
    {
        public HisEmrFormCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_EMR_FORM>();
        }

        private BridgeDAO<HIS_EMR_FORM> bridgeDAO;

        public bool Create(HIS_EMR_FORM data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_EMR_FORM> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
