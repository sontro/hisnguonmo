using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System.Collections.Generic;

namespace MOS.DAO.HisFileType
{
    partial class HisFileTypeCreate : EntityBase
    {
        public HisFileTypeCreate()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FILE_TYPE>();
        }

        private BridgeDAO<HIS_FILE_TYPE> bridgeDAO;

        public bool Create(HIS_FILE_TYPE data)
        {
            return IsNotNull(data) && bridgeDAO.Create(data);
        }

        public bool CreateList(List<HIS_FILE_TYPE> listData)
        {
            return IsNotNull(listData) && bridgeDAO.CreateList(listData);
        }
    }
}
