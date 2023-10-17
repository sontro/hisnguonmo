using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Aggr
{
    class HisImpMestRemoveAggr : BusinessBase
    {
        private HIS_IMP_MEST beforeUpdateHisImpMest;

        internal HisImpMestRemoveAggr()
            : base()
        {

        }

        internal HisImpMestRemoveAggr(CommonParam param)
            : base(param)
        {

        }

        internal bool RemoveAggr(long impMestId)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_IMP_MEST raw = null;
                HisImpMestCheck checker = new HisImpMestCheck(param);
                valid = valid && checker.VerifyId(impMestId, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    if (!raw.AGGR_IMP_MEST_ID.HasValue)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMest_PhieuXuatDangKhongThuocPhieuTraNao);
                        throw new Exception("Phieu xuat dang khong thuoc phieu tra nao." + LogUtil.TraceData("raw", raw));
                    }

                    //Kiem tra xem phieu linh da duoc duyet chua, neu da duyet thi ko cho phep sua
                    HIS_IMP_MEST aggrImpMest = new HisImpMestGet().GetById(raw.AGGR_IMP_MEST_ID.Value);
                    if (aggrImpMest.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__APPROVAL
                        || aggrImpMest.IMP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_IMP_MEST_STT.ID__IMPORT)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisImpMest_ChiChoPhepSuaKhiPhieuTraChuaDuocDuyet);
                        return false;
                    }

                    //Luu lai phuc vu rollback
                    Mapper.CreateMap<HIS_IMP_MEST, HIS_IMP_MEST>();
                    this.beforeUpdateHisImpMest = Mapper.Map<HIS_IMP_MEST>(raw);

                    //Xoa thong tin phieu tong hop
                    raw.AGGR_IMP_MEST_ID = null;
                    raw.TDL_AGGR_IMP_MEST_CODE = null;

                    if (!DAOWorker.HisImpMestDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisImpMest_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisImpMest that bai." + LogUtil.TraceData("raw", raw));
                    }

                    new EventLogGenerator(EventLog.Enum.HisImpMest_XoaKhoiPhieuTra).AggrImpMestCode(this.beforeUpdateHisImpMest.TDL_AGGR_IMP_MEST_CODE).ImpMestCode(raw.IMP_MEST_CODE).Run();

                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
