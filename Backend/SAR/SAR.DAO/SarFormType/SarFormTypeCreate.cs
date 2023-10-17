using SAR.DAO.Base;
using SAR.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SAR.DAO.SarFormType
{
    partial class SarFormTypeCreate : EntityBase
    {
        public SarFormTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SAR_FORM_TYPE>();
        }

        private BridgeDAO<SAR_FORM_TYPE> bridgeDAO;

        public bool Create(SAR_FORM_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SAR_FORM_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
