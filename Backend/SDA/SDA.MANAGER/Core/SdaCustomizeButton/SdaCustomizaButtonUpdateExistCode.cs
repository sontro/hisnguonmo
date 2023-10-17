using Inventec.Common.Logging;
using Inventec.Core;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDA.MANAGER.SdaCustomizaButton
{
    partial class SdaCustomizaButtonUpdate : BusinessBase
    {
		private List<SDA_CUSTOMIZA_BUTTON> beforeUpdateSdaCustomizaButtons = new List<SDA_CUSTOMIZA_BUTTON>();
		
        internal SdaCustomizaButtonUpdate()
            : base()
        {

        }

        internal SdaCustomizaButtonUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(SDA_CUSTOMIZA_BUTTON data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                SdaCustomizaButtonCheck checker = new SdaCustomizaButtonCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                SDA_CUSTOMIZA_BUTTON raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.CUSTOMIZA_BUTTON_CODE, data.ID);
                if (valid)
                {
					if (!DAOWorker.SdaCustomizaButtonDAO.Update(data))
                    {
                        SDA.MANAGER.Base.BugUtil.SetBugCode(param, SDA.LibraryBug.Bug.Enum.SdaCustomizaButton_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin SdaCustomizaButton that bai." + LogUtil.TraceData("data", data));
                    }
					
					this.beforeUpdateSdaCustomizaButtons.Add(raw);
                    
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool UpdateList(List<SDA_CUSTOMIZA_BUTTON> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                SdaCustomizaButtonCheck checker = new SdaCustomizaButtonCheck(param);
                List<SDA_CUSTOMIZA_BUTTON> listRaw = new List<SDA_CUSTOMIZA_BUTTON>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && checker.ExistsCode(data.CUSTOMIZA_BUTTON_CODE, data.ID);
                }
                if (valid)
                {
					if (!DAOWorker.SdaCustomizaButtonDAO.UpdateList(listData))
                    {
                        SDA.MANAGER.Base.BugUtil.SetBugCode(param, SDA.LibraryBug.Bug.Enum.SdaCustomizaButton_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin SdaCustomizaButton that bai." + LogUtil.TraceData("listData", listData));
                    }
					this.beforeUpdateSdaCustomizaButtons.AddRange(listRaw);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
		
		internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateSdaCustomizaButtons))
            {
                if (!DAOWorker.SdaCustomizaButtonDAO.UpdateList(this.beforeUpdateSdaCustomizaButtons))
                {
                    LogSystem.Warn("Rollback du lieu SdaCustomizaButton that bai, can kiem tra lai." + LogUtil.TraceData("SdaCustomizaButtons", this.beforeUpdateSdaCustomizaButtons));
                }
				this.beforeUpdateSdaCustomizaButtons = null;
            }
        }
    }
}
