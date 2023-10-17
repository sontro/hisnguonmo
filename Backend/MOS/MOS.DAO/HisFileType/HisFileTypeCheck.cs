using MOS.DAO.Base;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Linq;

namespace MOS.DAO.HisFileType
{
    partial class HisFileTypeCheck : EntityBase
    {
        public HisFileTypeCheck()
            : base()
        {
            bridgeDAO = new BridgeDAO<HIS_FILE_TYPE>();
        }

        private BridgeDAO<HIS_FILE_TYPE> bridgeDAO;

        public bool IsUnLock(long id)
        {
            return bridgeDAO.IsUnLock(id);
        }
    }
}
