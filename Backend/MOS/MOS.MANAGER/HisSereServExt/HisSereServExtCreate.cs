using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisEkip;
using MOS.MANAGER.HisEkipUser;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServPttt;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServExt
{
    partial class HisSereServExtCreate : BusinessBase
    {
        private List<HIS_SERE_SERV_EXT> recentHisSereServExts = new List<HIS_SERE_SERV_EXT>();
        private HIS_EKIP recentEkip;
        private List<HIS_EKIP_USER> recentEkipUsers;
        private HIS_SERE_SERV recentSereServ;
        private HIS_SERVICE_REQ recentServiceReq;
        private HIS_SERE_SERV_PTTT recentSereServPttt;

        private HisEkipUserCreate hisEkipUserCreate;
        private HisEkipUserTruncate hisEkipUserTruncate;
        private HisEkipUserUpdate hisEkipUserUpdate;
        private HisEkipCreate hisEkipCreate;
        private HisSereServUpdate hisSereServUpdate;
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisSereServPtttCreate hisSereServPtttCreate;
        private HisSereServPtttUpdate hisSereServPtttUpdate;

        internal HisSereServExtCreate()
            : base()
        {

        }

        internal HisSereServExtCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisEkipUserTruncate = new HisEkipUserTruncate(param);
            this.hisEkipUserUpdate = new HisEkipUserUpdate(param);
            this.hisEkipUserCreate = new HisEkipUserCreate(param);
            this.hisEkipCreate = new HisEkipCreate(param);
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisSereServPtttCreate = new HisSereServPtttCreate(param);
            this.hisSereServPtttUpdate = new HisSereServPtttUpdate(param);
        }

        internal bool Create(HIS_SERE_SERV_EXT data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServExtCheck checker = new HisSereServExtCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                if (valid)
                {
                    if (!DAOWorker.HisSereServExtDAO.Create(data))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServExt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServExt that bai." + LogUtil.TraceData("data", data));
                    }
                    this.recentHisSereServExts.Add(data);
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

        internal bool Create(HisSereServExtSDO data, ref HisSereServExtWithFileSDO hisSereServExtWithFileSDO)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisSereServExtCheck checker = new HisSereServExtCheck(param);
                HisSereServCheck sereServChecker = new HisSereServCheck(param);
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);

                valid = valid && checker.VerifyRequireField(data.HisSereServExt);
                valid = valid && sereServChecker.VerifyId(data.HisSereServExt.SERE_SERV_ID, ref recentSereServ);
                valid = valid && treatmentChecker.CheckRecordInspection(recentSereServ.SERVICE_REQ_ID);
                valid = valid && checker.IsValidLoginName(data, this.recentSereServ);
                
                if (valid)
                {
                    hisSereServExtWithFileSDO = new HisSereServExtWithFileSDO();
                    bool hasSelectedMachine = false;
                    this.ProcessSereServExt(data, ref hasSelectedMachine);
                    this.ProcessSereServPttt(data);
                    this.ProcessEkip(data);
                    this.ProcessSereServ(data);
                    this.ProcessServiceReq(hasSelectedMachine);
                    HisSereServExtUtil.Log(this.recentServiceReq, this.recentSereServ, data.HisSereServExt);
                    if (hasSelectedMachine)
                    {
                        new EventLogGenerator(EventLog.Enum.ChonMayXuLyChoDichVu, this.recentServiceReq.MACHINE_NAMES)
                        .TreatmentCode(this.recentServiceReq.TDL_TREATMENT_CODE)
                        .ServiceReqCode(this.recentServiceReq.SERVICE_REQ_CODE)
                        .Run();
                    }
                    
                    hisSereServExtWithFileSDO.SereServExt = this.recentHisSereServExts != null ? this.recentHisSereServExts.FirstOrDefault() : null;
                    hisSereServExtWithFileSDO.SereServPttt = this.recentSereServPttt;
                    hisSereServExtWithFileSDO.EkipUsers = this.recentEkipUsers;
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

        private void ProcessServiceReq(bool hasSelectedMachine)
        {
            if (this.recentSereServ != null && this.recentSereServ.SERVICE_REQ_ID.HasValue)
            {
                string loginName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                string userName = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                this.recentServiceReq = new HisServiceReqGet().GetById(this.recentSereServ.SERVICE_REQ_ID.Value);
                if (this.recentServiceReq != null)
                {
                    Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
                    HIS_SERVICE_REQ before = Mapper.Map<HIS_SERVICE_REQ>(this.recentServiceReq);
                    this.recentServiceReq.EXECUTE_LOGINNAME = loginName;
                    this.recentServiceReq.EXECUTE_USERNAME = userName;
                    this.recentServiceReq.EXE_WORKING_SHIFT_ID = TokenManager.GetWorkingShift();
                    if (hasSelectedMachine)
                        ChangeMachineNames();
                    if (!this.hisServiceReqUpdate.Update(this.recentServiceReq, before, false))
                    {
                        throw new Exception("Cap nhat thong tin nguoi xu ly that bai");
                    }
                }
            }
        }

        private void ChangeMachineNames()
        {
            var SereServExts = new HisSereServExtGet().GetByServiceReqId(this.recentServiceReq.ID);
            if (!IsNotNullOrEmpty(SereServExts)) return;
            List<long> machineIds = SereServExts.Select(o => o.MACHINE_ID ?? 0).Distinct().ToList();
            List<HIS_MACHINE> machineAlls = new HisMachine.HisMachineGet().GetByIds(machineIds);
            List<string> machineNames = new List<string>();
            bool isUpdate = false;
            foreach (var item in SereServExts)
            {
                HIS_MACHINE machine = machineAlls.FirstOrDefault(o => o.ID == item.MACHINE_ID);
                if (machine != null)
                {
                    machineNames.Add(machine.MACHINE_NAME);
                }
            }
            if (IsNotNullOrEmpty(machineNames))
            {
                this.recentServiceReq.MACHINE_NAMES = string.Join(",", machineNames);
            }
        }

        void ProcessSereServExt(HisSereServExtSDO data, ref bool hasSelectedMachine)
        {
            HisSereServExtUtil.SetTdl(data.HisSereServExt, this.recentSereServ);
            data.HisSereServExt.SUBCLINICAL_RESULT_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
            data.HisSereServExt.SUBCLINICAL_RESULT_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
            data.HisSereServExt.SUBCLINICAL_NURSE_LOGINNAME = TokenManager.GetNurseLoginName();
            data.HisSereServExt.SUBCLINICAL_NURSE_USERNAME = TokenManager.GetNurseUserName();

            var existedSereServExt = new HisSereServExtGet().GetBySereServId(this.recentSereServ.ID);
            if (this.recentSereServ != null && this.recentSereServ.IS_DELETE != 1
                && IsNotNull(existedSereServExt) && existedSereServExt.IS_DELETE == IMSys.DbConfig.HIS_RS.COMMON.IS_DELETE__TRUE)
            {
                DAOWorker.HisSereServExtDAO.Truncate(existedSereServExt);
            }
            if (data.HisSereServExt.MACHINE_ID.HasValue)
            {
                hasSelectedMachine = true;
            }

            if (!DAOWorker.HisSereServExtDAO.Create(data.HisSereServExt))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServExt_ThemMoiThatBai);
                throw new Exception("Them moi thong tin HisSereServExt that bai." + LogUtil.TraceData("SereServExt", data.HisSereServExt));
            }
            this.recentHisSereServExts.Add(data.HisSereServExt);
        }

        void CheckDuplicateEkipUser(List<HIS_EKIP_USER> ekipUsers)
        {
            if (IsNotNullOrEmpty(ekipUsers))
            {
                bool check = ekipUsers.GroupBy(o => new { o.LOGINNAME, o.EXECUTE_ROLE_ID }).Where(grp => grp.Count() > 1).Any();
                if (check)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisSurgServiceReq_TonTaiHaiDongDuLieuTrungNhau);
                    throw new Exception();
                }
            }
        }

        void ProcessEkip(HisSereServExtSDO data)
        {
            if (data.HisEkipUsers != null && this.recentSereServ != null)
            {
                this.CheckDuplicateEkipUser(data.HisEkipUsers);

                //Neu sere_serv chua co ekip_id va client co gui len thong tin ekip thi thi tao moi ekip
                if (!this.recentSereServ.EKIP_ID.HasValue && data.HisEkipUsers.Count > 0)
                {
                    //Neu sere_serv ko co ekip_id ma thong tin ekip_user lai co ekip_id ==> du lieu ko hop le
                    if (data.HisEkipUsers.Where(o => o.EKIP_ID > 0).Any())
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("ekip_users co thong tin ekip_id trong khi sere_serv chua co ekip_id." + LogUtil.TraceData("data", data));
                    }

                    HIS_EKIP ekip = new HIS_EKIP();
                    ekip.HIS_EKIP_USER = data.HisEkipUsers;
                    if (!this.hisEkipCreate.Create(ekip))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                    
                    this.recentEkip = ekip;

                    this.recentEkipUsers = new List<HIS_EKIP_USER>();
                    this.recentEkipUsers.AddRange(ekip.HIS_EKIP_USER);
                }
                else if (this.recentSereServ.EKIP_ID.HasValue)
                {
                    //Neu ton tai ekip_user co ekip_id khac voi ekip_id trong sere_serv ==> du lieu ko hop le
                    if (data.HisEkipUsers != null && data.HisEkipUsers.Where(o => o.EKIP_ID != this.recentSereServ.EKIP_ID.Value).Any())
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("ton tai ekip_user co ekip_id khac voi ekip_id trong sere_serv" + LogUtil.TraceData("data", data));
                    }

                    List<HIS_EKIP_USER> exists = new HisEkipUserGet().GetByEkipId(this.recentSereServ.EKIP_ID.Value);

                    //Danh sach can xoa la danh sach co trong exists nhung ko co trong danh sach client gui len
                    List<HIS_EKIP_USER> toTruncates = exists == null ? null : exists.Where(o => data.HisEkipUsers != null && !data.HisEkipUsers.Where(t => t.ID == o.ID).Any()).ToList();
                    //Danh sach can them moi la danh sach ko co ID
                    List<HIS_EKIP_USER> toInserts = data.HisEkipUsers == null ? null : data.HisEkipUsers.Where(o => o.ID <= 0).ToList();
                    //Danh sach can cap nhat la danh sach co ID
                    List<HIS_EKIP_USER> toUpdates = data.HisEkipUsers == null ? null : data.HisEkipUsers.Where(o => o.ID > 0).ToList();

                    if (IsNotNullOrEmpty(toTruncates))
                    {
                        if (!this.hisEkipUserTruncate.TruncateList(toTruncates))
                        {
                            throw new Exception("Ket thuc nghiep vu");
                        }
                    }

                    this.recentEkipUsers = new List<HIS_EKIP_USER>();
                    if (IsNotNullOrEmpty(toInserts))
                    {
                        if (!this.hisEkipUserCreate.CreateList(toInserts))
                        {
                            throw new Exception("Ket thuc nghiep vu");
                        }
                        this.recentEkipUsers.AddRange(toInserts);
                    }

                    if (IsNotNullOrEmpty(toUpdates))
                    {
                        if (!this.hisEkipUserUpdate.UpdateList(toUpdates))
                        {
                            throw new Exception("Ket thuc nghiep vu");
                        }
                        this.recentEkipUsers.AddRange(toUpdates);
                    }
                }
            }
        }

        void ProcessSereServ(HisSereServExtSDO data)
        {
            if (this.recentSereServ != null)
            {
                //Neu co tao moi thong tin kip thi cap nhat ekip_id cho sere_serv
                if (this.recentEkip != null)
                {
                    this.recentSereServ.EKIP_ID = this.recentEkip.ID;
                }

                HIS_SERE_SERV sereServ = null;
                //Thuc hien cap nhat cac thong tin cho sere_Serv
                if (!this.hisSereServUpdate.UpdateResult(this.recentSereServ, ref sereServ))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessSereServPttt(HisSereServExtSDO data)
        {
            try
            {
                if (data.HisSereServPttt != null)
                {
                    data.HisSereServPttt.TDL_TREATMENT_ID = this.recentSereServ.TDL_TREATMENT_ID;
                    data.HisSereServPttt.SERE_SERV_ID = this.recentSereServ.ID;

                    HIS_SERE_SERV_PTTT exist = new HisSereServPtttGet().GetBySereServId(this.recentSereServ.ID);

                    if (exist == null)
                    {
                        if (!hisSereServPtttCreate.Create(data.HisSereServPttt))
                        {
                            throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                        }
                    }
                    else
                    {
                        data.HisSereServPttt.ID = exist.ID;

                        if (!hisSereServPtttUpdate.Update(data.HisSereServPttt))
                        {
                            throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                        }
                    }
                    this.recentSereServPttt = data.HisSereServPttt;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        

        internal bool CreateList(List<HIS_SERE_SERV_EXT> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisSereServExtCheck checker = new HisSereServExtCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                }
                if (valid)
                {
                    if (!DAOWorker.HisSereServExtDAO.CreateList(listData))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisSereServExt_ThemMoiThatBai);
                        throw new Exception("Them moi thong tin HisSereServExt that bai." + LogUtil.TraceData("listData", listData));
                    }
                    this.recentHisSereServExts.AddRange(listData);
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
            this.hisServiceReqUpdate.RollbackData();
            this.hisEkipUserUpdate.RollbackData();
            this.hisEkipUserCreate.RollbackData();
            this.hisEkipCreate.RollbackData();
            this.hisSereServPtttCreate.RollbackData();
            this.hisSereServPtttUpdate.RollbackData();

            if (IsNotNullOrEmpty(this.recentHisSereServExts))
            {
                if (!DAOWorker.HisSereServExtDAO.TruncateList(this.recentHisSereServExts))
                {
                    LogSystem.Warn("Rollback du lieu HisSereServExt that bai, can kiem tra lai." + LogUtil.TraceData("recentHisSereServExts", this.recentHisSereServExts));
                }
            }
        }
    }
}
