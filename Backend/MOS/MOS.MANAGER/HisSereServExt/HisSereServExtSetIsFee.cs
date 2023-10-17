using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisPatient;
using MOS.MANAGER.HisRoom;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.UTILITY;
using MOS.MANAGER.HisEkipUser;
using MOS.MANAGER.HisSereServPttt;
using MOS.MANAGER.HisSurgRemuneration;
using MOS.MANAGER.HisPtttGroup;
using MOS.MANAGER.HisEmotionlessMethod;
using MOS.MANAGER.HisSurgRemuDetail;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisExecuteRole;
using MOS.MANAGER.HisEkip;

namespace MOS.MANAGER.HisSereServExt
{
    partial class HisSereServExtSetIsFee : BusinessBase
    {
        private HisEkipUserUpdate hisEkipUserUpdate;
        private HisEkipUserCreate hisEkipUserCreate;
        private HisEkipCreate hisEkipCreate;
        private HIS_SERE_SERV_EXT recentInsertExt;
        private HIS_SERE_SERV_EXT beforeUpdateExt;
        private HisSereServUpdate hisSereServUpdate;

        internal HisSereServExtSetIsFee()
            : base()
        {
            this.Init();
        }

        internal HisSereServExtSetIsFee(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisEkipUserUpdate = new HisEkipUserUpdate(param);
            this.hisEkipUserCreate = new HisEkipUserCreate(param);
            this.hisEkipCreate = new HisEkipCreate(param);
            this.hisSereServUpdate = new HisSereServUpdate(param);
        }

        internal bool Run(HisSereServExtIsFeeSDO data, ref HIS_SERE_SERV_EXT resultData)
        {
            bool result = false;

            try
            {
                bool valid = true;
                HIS_SERE_SERV sereServ = null;
                HIS_SURG_REMUNERATION surgRemuneration = null;
                HIS_SERE_SERV_PTTT sereServPttt = null;
                HIS_SERE_SERV_EXT ssExt = new HisSereServExtGet().GetBySereServId(data.SereServId);

                HisSereServExtCheck checker = new HisSereServExtCheck(param);
                HisSereServCheck sereServChecker = new HisSereServCheck(param);
                valid = valid && sereServChecker.VerifyId(data.SereServId, ref sereServ);
                valid = valid && (!data.IsFee || this.HasSereServPttt(sereServ, ref sereServPttt));
                valid = valid && (!data.IsFee || this.HasSurgRemuneration(sereServ, sereServPttt, ref surgRemuneration, ssExt));

                if (valid)
                {
                    bool check = false;

                    if (ssExt == null)
                    {
                        ssExt = new HIS_SERE_SERV_EXT();
                        ssExt.IS_FEE = data.IsFee ? (short?)Constant.IS_TRUE : null;
                        if (data.IsFee)
                        {
                            ssExt.IS_GATHER_DATA = Constant.IS_TRUE;//neu "co lay chi phi" thi bat buoc phai vao bao cao
                        }
                        HisSereServExtUtil.SetTdl(ssExt, sereServ);
                        if (DAOWorker.HisSereServExtDAO.Create(ssExt))
                        {
                            this.recentInsertExt = ssExt;
                            check = true;
                        }
                    }
                    else
                    {
                        Mapper.CreateMap<HIS_SERE_SERV_EXT, HIS_SERE_SERV_EXT>();
                        HIS_SERE_SERV_EXT before = Mapper.Map<HIS_SERE_SERV_EXT>(ssExt);

                        ssExt.IS_FEE = data.IsFee ? (short?)Constant.IS_TRUE : null;
                        if (data.IsFee)
                        {
                            ssExt.IS_GATHER_DATA = Constant.IS_TRUE;//neu "co lay chi phi" thi bat buoc phai vao bao cao
                        }
                        result = DAOWorker.HisSereServExtDAO.Update(ssExt);
                        if (DAOWorker.HisSereServExtDAO.Update(ssExt))
                        {
                            this.beforeUpdateExt = before;
                            check = true;
                        }
                    }
                    if (check)
                    {
                        this.SetEkipSurgRemuneration(sereServ, ssExt, data.IsFee, surgRemuneration);
                        resultData = result ? ssExt : null;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                result = false;
            }
            
            return result;
        }

        private void Rollback()
        {
            this.hisSereServUpdate.RollbackData();
            this.hisEkipUserCreate.RollbackData();
            this.hisEkipUserUpdate.RollbackData();
            this.hisEkipCreate.RollbackData();

            if (this.recentInsertExt != null)
            {
                try
                {
                    if (!DAOWorker.HisSereServExtDAO.Truncate(this.recentInsertExt))
                    {
                        LogSystem.Warn("rollback HisSereServExt that bai");
                    }
                }
                catch (Exception ex)
                {
                    LogSystem.Warn(ex);
                }
            }
            if (this.beforeUpdateExt != null)
            {
                try
                {
                    if (!DAOWorker.HisSereServExtDAO.Update(this.beforeUpdateExt))
                    {
                        LogSystem.Warn("rollback HisSereServExt that bai");
                    }
                }
                catch (Exception ex)
                {
                    LogSystem.Warn(ex);
                }
            }
        }

        private void SetEkipSurgRemuneration(HIS_SERE_SERV sereServ, HIS_SERE_SERV_EXT ssExt, bool isFee, HIS_SURG_REMUNERATION surgRemuneration)
        {
            try
            {
                List<HIS_EKIP_USER> toUpdateEkipUsers = sereServ.EKIP_ID.HasValue ? new HisEkipUserGet().GetByEkipId(sereServ.EKIP_ID.Value) : null;
                HIS_EKIP_USER toInsertEkipUser = null;
                HIS_EKIP ekip = null;

                //Neu ko tinh chi phi thi clear du lieu tien
                if (!isFee && IsNotNullOrEmpty(toUpdateEkipUsers))
                {
                    toUpdateEkipUsers.ForEach(o => o.REMUNERATION_PRICE = 0);
                }
                //Neu la tinh chi phi thi update tien cong theo du lieu da cau hinh
                else if (isFee)
                {
                    List<HIS_SURG_REMU_DETAIL> details = new HisSurgRemuDetailGet().GetBySurgRemunerationId(surgRemuneration.ID);

                    if (IsNotNullOrEmpty(toUpdateEkipUsers))
                    {
                        foreach (HIS_EKIP_USER ekipUser in toUpdateEkipUsers)
                        {
                            HIS_SURG_REMU_DETAIL d = IsNotNullOrEmpty(details) ? details.Where(o => o.EXECUTE_ROLE_ID == ekipUser.EXECUTE_ROLE_ID).OrderByDescending(o => o.PRICE).FirstOrDefault() : null;
                            ekipUser.REMUNERATION_PRICE = d != null ? d.PRICE : 0;
                        }
                    }
                    //Da ket thuc xu ly ma ko co thong tin kip thi tu dong tao thong tin kip theo vai tro la PTV chinh va nguoi thuc hien y lenh
                    else if (ssExt != null && ssExt.END_TIME.HasValue && sereServ.SERVICE_REQ_ID.HasValue)
                    {
                        HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(sereServ.SERVICE_REQ_ID.Value);
                        
                        HisExecuteRoleFilterQuery roleFilter = new HisExecuteRoleFilterQuery();
                        roleFilter.IS_SURG_MAIN = true;
                        List<HIS_EXECUTE_ROLE> mainRoles = new HisExecuteRoleGet().Get(roleFilter);
                        if (!IsNotNullOrEmpty(mainRoles))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServExt_ChuaCauHinhVaiTroPhauThuatVienChinh);
                        }
                        else
                        {
                            ekip = new HIS_EKIP();
                            if (!this.hisEkipCreate.Create(ekip))
                            {
                                throw new Exception("Tu dong tao HIS_EKIP that bai");
                            }

                            HIS_EXECUTE_ROLE mainRole = mainRoles.OrderByDescending(o => o.MODIFY_TIME).FirstOrDefault();
                            toInsertEkipUser = new HIS_EKIP_USER();
                            toInsertEkipUser.LOGINNAME = serviceReq.EXECUTE_LOGINNAME;
                            toInsertEkipUser.USERNAME = serviceReq.EXECUTE_USERNAME;
                            toInsertEkipUser.EXECUTE_ROLE_ID = mainRole.ID;
                            toInsertEkipUser.EKIP_ID = ekip.ID;

                            HIS_SURG_REMU_DETAIL d = IsNotNullOrEmpty(details) ? details
                                .Where(o => o.EXECUTE_ROLE_ID == toInsertEkipUser.EXECUTE_ROLE_ID)
                                .OrderByDescending(o => o.PRICE).FirstOrDefault() : null;
                            toInsertEkipUser.REMUNERATION_PRICE = d != null ? d.PRICE : 0;
                        }
                    }
                }

                if (IsNotNullOrEmpty(toUpdateEkipUsers) && !this.hisEkipUserUpdate.UpdateList(toUpdateEkipUsers))
                {
                    throw new Exception("Cap nhat lai tien cong PTTT (REMUNERATION_PRICE trong HIS_EKIP_USER) that bai");
                }
                if (toInsertEkipUser != null)
                {
                    if (!this.hisEkipUserCreate.Create(toInsertEkipUser))
                    {
                        throw new Exception("Tu dong tao HIS_EKIP_USER de luu tien cong PTTT that bai");
                    }
                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                    var before = Mapper.Map<HIS_SERE_SERV>(sereServ);
                    sereServ.EKIP_ID = ekip.ID;

                    if (!this.hisSereServUpdate.Update(sereServ, before))
                    {
                        throw new Exception("Cap nhat thong tin kip cho HIS_SERE_SERV de luu tien cong PTTT that bai");
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private bool HasSereServPttt(HIS_SERE_SERV sereServ, ref HIS_SERE_SERV_PTTT sereServPttt)
        {
            try
            {
                sereServPttt = new HisSereServPtttGet().GetBySereServId(sereServ.ID);
                if (sereServPttt == null)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServExt_ChuaCoThongTinXuLyPttt, sereServ.TDL_SERVICE_NAME);
                    return false;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return false;
            }
        }

        private bool HasSurgRemuneration(HIS_SERE_SERV sereServ, HIS_SERE_SERV_PTTT sereServPttt, ref HIS_SURG_REMUNERATION surgRemuneration, HIS_SERE_SERV_EXT ssExt)
        {
            try
            {

                if (sereServPttt == null || !sereServPttt.PTTT_GROUP_ID.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServPttt_ChuaNhapThongTinLoaiPttt, sereServ.TDL_SERVICE_NAME);
                    return false;
                }

                HisSurgRemunerationFilterQuery filter = new HisSurgRemunerationFilterQuery();
                filter.PTTT_GROUP_ID = sereServPttt.PTTT_GROUP_ID.Value;
                filter.SERVICE_TYPE_ID = sereServ.TDL_SERVICE_TYPE_ID;
                
                List<HIS_SURG_REMUNERATION> surgRemunerations = new HisSurgRemunerationGet().Get(filter);

                surgRemuneration = IsNotNullOrEmpty(surgRemunerations) ? surgRemunerations.Where(o => !o.EMOTIONLESS_METHOD_ID.HasValue || (sereServPttt != null && sereServPttt.EMOTIONLESS_METHOD_ID.HasValue && o.EMOTIONLESS_METHOD_ID.Value == sereServPttt.EMOTIONLESS_METHOD_ID.Value))
                    .Where(o => (!o.SURG_FROM_TIME.HasValue || o.SURG_FROM_TIME <= ssExt.END_TIME) && (!o.SURG_TO_TIME.HasValue || o.SURG_TO_TIME >= ssExt.END_TIME))
                    .FirstOrDefault() : null;

                if (surgRemuneration == null)
                {
                    HIS_PTTT_GROUP ptttGroup = new HisPtttGroupGet().GetById(sereServPttt.PTTT_GROUP_ID.Value);
                    HIS_EMOTIONLESS_METHOD emotionlessMethod = sereServPttt.EMOTIONLESS_METHOD_ID.HasValue ? new HisEmotionlessMethodGet().GetById(sereServPttt.EMOTIONLESS_METHOD_ID.Value) : null;
                    HIS_SERVICE_TYPE serviceType = HisServiceTypeCFG.DATA.Where(o => o.ID == sereServ.TDL_SERVICE_TYPE_ID).FirstOrDefault();

                    string emotionlessMethodName = emotionlessMethod != null ? emotionlessMethod.EMOTIONLESS_METHOD_NAME : "";
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSereServExt_ChuaThietLapTienCongPttt, ptttGroup.PTTT_GROUP_NAME, serviceType.SERVICE_TYPE_NAME, emotionlessMethodName);
                    return false;
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
