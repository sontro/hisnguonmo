using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;

namespace MOS.MANAGER.HisExpMestTemplate
{
	class HisExpMestTemplateCheck : BusinessBase
	{
		internal HisExpMestTemplateCheck()
			: base()
		{

		}

		internal HisExpMestTemplateCheck(CommonParam paramCheck)
			: base(paramCheck) 
		{

		}

		internal bool VerifyRequireField(HIS_EXP_MEST_TEMPLATE data)
		{
			bool valid = true;
			try
			{
				if (data == null) throw new ArgumentNullException("data");
				if (!IsNotNullOrEmpty(data.EXP_MEST_TEMPLATE_CODE)) throw new ArgumentNullException("data.EXP_MEST_TEMPLATE_CODE");
				if (!IsNotNullOrEmpty(data.EXP_MEST_TEMPLATE_NAME)) throw new ArgumentNullException("data.EXP_MEST_TEMPLATE_NAME");
			}
			catch (ArgumentNullException ex)
			{
				MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
				LogSystem.Warn(ex);
				valid = false;
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				valid = false;
				param.HasException = true;
			}
			return valid;
		}

		internal bool ExistsCode(string code, long? id)
		{
			bool valid = true;
			try
			{
				if (DAOWorker.HisExpMestTemplateDAO.ExistsCode(code, id))
				{
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.MaDaTonTaiTrenHeThong, code);
					valid = false;
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				valid = false;
				param.HasException = true;
			}
			return valid;
		}

		internal bool IsUnLock(HIS_EXP_MEST_TEMPLATE data)
		{
			bool valid = true;
			try
			{
				if (IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE != data.IS_ACTIVE)
				{
					valid = false;
					MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDangBiKhoa);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				valid = false;
				param.HasException = true;
			}
			return valid;
		}

        /// <summary>
        /// Kiem tra su ton tai cua id dong thoi lay ve du lieu
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool VerifyId(long id, ref HIS_EXP_MEST_TEMPLATE data)
        {
            bool valid = true;
            try
            {
                data = new HisExpMestTemplateGet().GetById(id);
                if (data == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    Logging("Id invalid." + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => id), id), LogType.Error);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
		
		internal bool IsUnLock(long id)
		{
			bool valid = true;
			try
			{
				if (!DAOWorker.HisExpMestTemplateDAO.IsUnLock(id))
				{
					valid = false;
					MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDangBiKhoa);
				}
			}
			catch (Exception ex)
			{
				LogSystem.Error(ex);
				valid = false;
				param.HasException = true;
			}
			return valid;
		}
	}
}
