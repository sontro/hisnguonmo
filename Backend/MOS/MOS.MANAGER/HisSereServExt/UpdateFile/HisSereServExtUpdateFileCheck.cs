using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Fss.Client;
using Inventec.Fss.Utility;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisEkip;
using MOS.MANAGER.HisEkipUser;
using MOS.MANAGER.HisEmployee;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServFile;
using MOS.MANAGER.HisSereServPttt;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MOS.MANAGER.HisSereServExt.Update
{
    internal class HisSereServExtUpdateFileCheck : BusinessBase
    {
        public HisSereServExtUpdateFileCheck()
            : base()
        {
        }

        public HisSereServExtUpdateFileCheck(CommonParam param)
            : base(param)
        {
        }

        public bool IsValidData(HisSereServExtSDO data, ref HIS_SERVICE_REQ serviceReq, ref HIS_SERE_SERV sereServ, ref HIS_SERE_SERV_EXT sereServExt)
        {
            bool result = true;
            try
            {
                sereServ = data.HisSereServExt != null ? new HisSereServGet().GetById(data.HisSereServExt.SERE_SERV_ID) : null;
                if (sereServ == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.LoiDuLieu);
                    LogSystem.Warn("Khong lay duoc HisSereServ theo SereServId.");
                    return false;
                }

                serviceReq = new HisServiceReqGet().GetById(sereServ.SERVICE_REQ_ID.Value);
                if (serviceReq == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.LoiDuLieu);
                    LogSystem.Warn("Khong lay duoc serviceReq theo SereServ.");
                    return false;
                }

                sereServExt = new HisSereServExtGet().GetById(data.HisSereServExt.ID);
                if (sereServExt == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.LoiDuLieu);
                    LogSystem.Warn("Khong lay duoc sereServExt theo id.");
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Kiem tra co cho phep huy ket thuc trong truong hop nhap vien hay khong
        /// </summary>
        /// <param name="serviceReq"></param>
        /// <param name="treatment"></param>
        /// <returns></returns>
        public bool IsAllowUpdateResultSubclinical(HisSereServExtSDO data, HIS_SERVICE_REQ serviceReq, HIS_SERE_SERV_EXT old, HIS_SERE_SERV sereServ)
        {
            try
            {
                if (HisServiceReqCFG.IS_CHECKING_PERMISSION_OF_RESULTING_SUBCINICAL && data.HisSereServExt != null)
                {
                    string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                    HIS_EMPLOYEE employee = HisEmployeeUtil.GetEmployee();

                    //Chi cho phep nguoi dung co quyen moi duoc huy ket thuc CLS cua nguoi khac
                    if (HisServiceReqTypeCFG.SUBCLINICAL_TYPE_IDs.Contains(serviceReq.SERVICE_REQ_TYPE_ID)
                        && old != null && !string.IsNullOrWhiteSpace(old.SUBCLINICAL_RESULT_LOGINNAME) && old.SUBCLINICAL_RESULT_LOGINNAME != loginName
                        && (employee == null || employee.ALLOW_UPDATE_OTHER_SCLINICAL != Constant.IS_TRUE))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisServiceReq_DichVuDoNguoiKhacTraKetQua, sereServ.TDL_SERVICE_NAME, old.SUBCLINICAL_RESULT_LOGINNAME, old.SUBCLINICAL_RESULT_USERNAME);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        public bool IsNotDuplicateEkipUser(List<HIS_EKIP_USER> ekipUsers)
        {
            try
            {
                if (IsNotNullOrEmpty(ekipUsers))
                {
                    bool check = ekipUsers.GroupBy(o => new { o.LOGINNAME, o.EXECUTE_ROLE_ID }).Where(grp => grp.Count() > 1).Any();
                    if (check)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSurgServiceReq_TonTaiHaiDongDuLieuTrungNhau);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }
    }
}
