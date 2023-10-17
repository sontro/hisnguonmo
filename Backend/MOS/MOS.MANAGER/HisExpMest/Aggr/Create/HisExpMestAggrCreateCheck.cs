using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMestBlood;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.Token;
using MOS.MANAGER.HisExpMest.Common.Get;

namespace MOS.MANAGER.HisExpMest.Aggr.Create
{
    partial class HisExpMestAggrCreateCheck : BusinessBase
    {
        internal HisExpMestAggrCreateCheck()
            : base()
        {

        }

        internal HisExpMestAggrCreateCheck(CommonParam paramCreate)
            : base(paramCreate)
        {
            
        }

        internal bool VerifyRequireField(HisExpMestAggrSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.ReqRoomId <= 0) throw new ArgumentNullException("data.ReqRoomId null");
                if (!IsNotNullOrEmpty(data.ExpMestIds)) throw new ArgumentNullException("data.ExpMestIds null");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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

        internal bool IsAllowed(HisExpMestAggrSDO data, WorkPlaceSDO workPlace, ref List<HIS_EXP_MEST> expMests)
        {
            try
            {
                List<HIS_EXP_MEST> tmp = new HisExpMestGet().GetByIds(data.ExpMestIds);
                if (tmp == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("ExpMestIds ko hop le");
                    return false;
                }

                //Kiem tra xem cac phieu xuat da thuoc cac phieu linh nao chua
                List<string> inAggrs = tmp.Where(o => o.AGGR_EXP_MEST_ID.HasValue).Select(o => o.EXP_MEST_CODE).ToList();
                if (IsNotNullOrEmpty(inAggrs))
                {
                    string inAggrStr = string.Join(",", inAggrs);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_CacPhieuDaDuocTongHop, inAggrStr);
                    return false;
                }

                //Kiem tra xem cac phieu xuat co phieu nao ko o trang thai y/c ko
                List<string> notRequests = tmp
                    .Where(o => o.EXP_MEST_STT_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST)
                    .Select(o => o.EXP_MEST_CODE).ToList();
                if (IsNotNullOrEmpty(notRequests))
                {
                    string notRequestStr = string.Join(",", notRequests);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_CacPhieuKhongOTrangThaiYeuCau, notRequestStr);
                    return false;
                }

                //Ko cho tao phieu linh voi cac phieu linh ko phai la don noi tru
                List<string> invalidTypes = tmp.Where(o => o.EXP_MEST_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DDT).Select(o => o.EXP_MEST_CODE).ToList();
                if (IsNotNullOrEmpty(invalidTypes))
                {
                    string invalidTypeStr = string.Join(",", invalidTypes);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_CacPhieuKhongPhaiDonNoiTru, invalidTypeStr);
                    return false;
                }

                //Kiem tra xem co phieu xuat nao ko phai do khoa ma nguoi dung dang lam viec tao ra hay khong
                List<string> notInReqDepartments = tmp.Where(o => o.REQ_DEPARTMENT_ID != workPlace.DepartmentId).Select(o => o.EXP_MEST_CODE).ToList();
                if (IsNotNullOrEmpty(notInReqDepartments))
                {
                    string notInReqDepartmentStr = string.Join(",", notInReqDepartments);
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisExpMest_CacPhieuKhongPhaiCuaKhoaDangLamViec, notInReqDepartmentStr);
                    return false;
                }

                expMests = tmp;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
            return true;
        }
    }
}
