using SDA.DAO.Base;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace SDA.DAO.SdaHideControl
{
    partial class SdaHideControlCreate : EntityBase
    {
        public SdaHideControlCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<SDA_HIDE_CONTROL>();
        }

        private BridgeDAO<SDA_HIDE_CONTROL> bridgeDAO;

        public bool Create(SDA_HIDE_CONTROL data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<SDA_HIDE_CONTROL> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
