using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;

namespace MOS.MANAGER.HisSereServ
{
    partial class HisSereServUpdate : BusinessBase
    {
        internal bool UpdateJsonPrintId(HIS_SERE_SERV data, ref HIS_SERE_SERV resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_SERE_SERV raw = null;
                HisSereServCheck checker = new HisSereServCheck(param);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                    this.beforeUpdateHisSereServs.Add(Mapper.Map<HIS_SERE_SERV>(raw));

                    if (!DAOWorker.HisSereServDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServ_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisSereServ that bai." + LogUtil.TraceData("data", data));
                    }
                    resultData = raw;
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
    }
}
