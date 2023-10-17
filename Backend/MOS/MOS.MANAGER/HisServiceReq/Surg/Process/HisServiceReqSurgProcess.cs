using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisEkip;
using MOS.MANAGER.HisEkipUser;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisEyeSurgryDesc;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.MANAGER.HisSereServExt;
using MOS.MANAGER.HisSereServPttt;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisSkinSurgeryDesc;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisSesePtttMethod;
using MOS.MANAGER.HisServiceReq.CheckSurgSimultaneily;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.MANAGER.HisStentConclude;

namespace MOS.MANAGER.HisServiceReq.Surg.Process
{
    /// <summary>
    /// Xu ly phau thuat thu thuat
    /// </summary>
    class HisServiceReqSurgProcess : BusinessBase
    {
        private HIS_SERE_SERV recentSereServ;
        private HIS_SERE_SERV_PTTT recentSereServPttt;
        private List<HisSesePtttMethodSDO> recentSesePtttMethods;
        private HIS_SERE_SERV_EXT recentSereServExt;
        private HIS_EYE_SURGRY_DESC recentEyeSurgryDesc;
        private HIS_SKIN_SURGERY_DESC recentSkinSurgeryDesc;
        private HIS_EKIP recentEkip;
        private List<HIS_EKIP_USER> recentEkipUsers;
        private List<HIS_STENT_CONCLUDE> recentStentConcludes;

        private HisSereServPtttCreate hisSereServPtttCreate;
        private HisSereServPtttUpdate hisSereServPtttUpdate;
        private HisEyeSurgryDescCreate hisEyeSurgryDescCreate;
        private HisEyeSurgryDescUpdate hisEyeSurgryDescUpdate;
        private HisEyeSurgryDescTruncate hisEyeSurgryDescTruncate;

        private HisSkinSurgeryDescCreate hisSkinSurgeryDescCreate;
        private HisSkinSurgeryDescUpdate hisSkinSurgeryDescUpdate;
        private HisSkinSurgeryDescTruncate hisSkinSurgeryDescTruncate;

        private HisSereServExtCreate hisSereServExtCreate;
        private HisSereServExtUpdate hisSereServExtUpdate;
        private HisServiceReqUpdate hisServiceReqUpdate;
        private HisSereServUpdate hisSereServUpdate;
        private HisEkipUserCreate hisEkipUserCreate;
        private HisEkipUserTruncate hisEkipUserTruncate;
        private HisEkipUserUpdate hisEkipUserUpdate;
        private HisEkipCreate hisEkipCreate;
        private HisEkipTruncate hisEkipTruncate;
        private HisSereServUpdateHein hisSereServUpdateHein;
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;
        private HisExpMestMaterialUpdate hisExpMestMaterialUpdate;
        private HisSesePtttMethodCreate hisSesePtttMethodCreate;
        private HisSesePtttMethodUpdate hisSesePtttMethodUpdate;
        private HisSesePtttMethodTruncate hisSesePtttMethodTruncate;

        private HisStentConcludeCreate hisStentConcludeCreate;
        private HisStentConcludeTruncate hisStentConcludeTruncate;

        private bool hasUpdateFinishTime;

        internal HisServiceReqSurgProcess()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqSurgProcess(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisSereServPtttCreate = new HisSereServPtttCreate(param);
            this.hisSereServPtttUpdate = new HisSereServPtttUpdate(param);
            this.hisSereServExtCreate = new HisSereServExtCreate(param);
            this.hisSereServExtUpdate = new HisSereServExtUpdate(param);
            this.hisSereServUpdate = new HisSereServUpdate(param);
            this.hisEkipUserTruncate = new HisEkipUserTruncate(param);
            this.hisEkipUserUpdate = new HisEkipUserUpdate(param);
            this.hisEkipUserCreate = new HisEkipUserCreate(param);
            this.hisEkipCreate = new HisEkipCreate(param);
            this.hisEkipTruncate = new HisEkipTruncate(param);
            this.hisServiceReqUpdate = new HisServiceReqUpdate(param);
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
            this.hisExpMestMaterialUpdate = new HisExpMestMaterialUpdate(param);
            this.hisEyeSurgryDescCreate = new HisEyeSurgryDescCreate(param);
            this.hisEyeSurgryDescUpdate = new HisEyeSurgryDescUpdate(param);
            this.hisEyeSurgryDescTruncate = new HisEyeSurgryDescTruncate(param);

            this.hisSkinSurgeryDescCreate = new HisSkinSurgeryDescCreate(param);
            this.hisSkinSurgeryDescUpdate = new HisSkinSurgeryDescUpdate(param);
            this.hisSkinSurgeryDescTruncate = new HisSkinSurgeryDescTruncate(param);
            this.hisSesePtttMethodCreate = new HisSesePtttMethodCreate(param);
            this.hisSesePtttMethodUpdate = new HisSesePtttMethodUpdate(param);
            this.hisSesePtttMethodTruncate = new HisSesePtttMethodTruncate(param);
            this.hisStentConcludeCreate = new HisStentConcludeCreate(param);
            this.hisStentConcludeTruncate = new HisStentConcludeTruncate(param);
        }

        internal bool Run(HisSurgServiceReqUpdateSDO data, ref HisSurgServiceReqUpdateSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT hisTreatment = null;
                HIS_SERE_SERV raw = null;
                V_HIS_SERVICE service = null;

                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisSereServCheck sereServChecker = new HisSereServCheck(param);
                HisServiceReqCheck serviceReqChecker = new HisServiceReqCheck(param);
                HisServiceCheck svChecker = new HisServiceCheck(param);
                HisServiceReqSurgProcessCheck checker = new HisServiceReqSurgProcessCheck(param);
                CheckSurgSimultaneilyProcessor simultaneilyChecker = new CheckSurgSimultaneilyProcessor(param);

                valid = valid && sereServChecker.VerifyId(data.SereServId, ref raw);
                valid = valid && serviceReqChecker.IsNotAprovedSurgeryRemuneration(raw);
                valid = valid && raw.TDL_TREATMENT_ID.HasValue;
                valid = valid && treatmentChecker.VerifyId(raw.TDL_TREATMENT_ID.Value, ref hisTreatment);
                valid = valid && checker.IsValidUpdateInstructionTime(data.UpdateInstructionTimeByStartTime, data.SereServExt, hisTreatment);
                valid = valid && checker.IsValidTime(data.SereServExt);
                valid = valid && checker.IsNotDuplicateEkipUser(data.EkipUsers);
                valid = valid && checker.IsValidEyeSurgDescData(data.EyeSurgryDesc);
                valid = valid && svChecker.IsValidMinMaxServiceTime(new List<HIS_SERE_SERV_EXT>() { data.SereServExt }, new List<HIS_SERE_SERV>() { raw });
                if (HisServiceReqCFG.IS_CHECK_SIMULTANEITY && data.SereServExt != null && data.SereServExt.END_TIME.HasValue && data.SereServExt.END_TIME.HasValue)
                {
                    service = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == raw.SERVICE_ID);
                    if (service != null && (!service.ALLOW_SIMULTANEITY.HasValue || service.ALLOW_SIMULTANEITY.Value != Constant.IS_TRUE))
                    {
                        valid = simultaneilyChecker.CheckPatient(raw, hisTreatment.ID, data.SereServExt.BEGIN_TIME, data.SereServExt.END_TIME, null) && valid;
                        valid = simultaneilyChecker.CheckDoctor(data.EkipUsers, raw, data.SereServExt.BEGIN_TIME, data.SereServExt.END_TIME, null) && valid;
                    }
                }

                if (!HisServiceReqCFG.ALLOW_UPDATE_SURG_INFO_AFTER_LOCKING_TREATMENT)
                {
                    valid = valid && treatmentChecker.IsUnLock(hisTreatment);
                    valid = valid && treatmentChecker.IsUnpause(hisTreatment);
                    valid = valid && treatmentChecker.IsUnTemporaryLock(hisTreatment);
                    valid = valid && treatmentChecker.IsUnLockHein(hisTreatment);
                }

                if (valid)
                {
                    HIS_SERVICE_REQ serviceReq = null;
                    HIS_SERE_SERV_PTTT oldSereServPttt = new HisSereServPtttGet().GetBySereServId(data.SereServId);
                    List<HIS_EKIP_USER> oldEkipUsers = null;

                    this.ProcessEkip(data, raw, ref oldEkipUsers);
                    this.ProcessSereServExt(data, raw, ref serviceReq);
                    this.ProcessServiceReq(data, serviceReq, hisTreatment);
                    this.ProcessSereServ(hisTreatment, serviceReq, raw);
                    this.ProcessEyeSurgry(data, oldSereServPttt);
                    this.ProcessSkinSurgery(data, oldSereServPttt);
                    this.ProcessSereServPttt(data, serviceReq, oldSereServPttt);
                    this.ProcessSesePtttMethod(data, this.recentSereServPttt, serviceReq);
                    this.ProcessDeleteEyeSurgry(data, oldSereServPttt);
                    this.ProcessDeleteSkinSurgery(data, oldSereServPttt);
                    this.ProcessStentConclude(data, raw);
                    this.PassResult(ref resultData);
                    this.ProcessEventLogEkipUser(serviceReq, raw, oldEkipUsers, this.recentEkipUsers);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                this.RollbackData();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        private void ProcessSesePtttMethod(HisSurgServiceReqUpdateSDO data, HIS_SERE_SERV_PTTT sereServPttt, HIS_SERVICE_REQ serviceReq)
        {
            HisSesePtttMethodFilterQuery filter = new HisSesePtttMethodFilterQuery();
            filter.SERE_SERV_PTTT_ID = sereServPttt.ID;
            var exitsSesePtttMethods = new HisSesePtttMethodGet().Get(filter);

            List<HIS_SESE_PTTT_METHOD> createData = new List<HIS_SESE_PTTT_METHOD>();
            List<HIS_SESE_PTTT_METHOD> updateData = new List<HIS_SESE_PTTT_METHOD>();
            List<HIS_SESE_PTTT_METHOD> deleteData = new List<HIS_SESE_PTTT_METHOD>();

            if (data.SesePtttMethos != null && data.SesePtttMethos.Count > 0)
            {
                foreach (var ptttMethod in data.SesePtttMethos)
                {
                    if (ptttMethod.HisSesePtttMethod == null)
                        continue;

                    HIS_SESE_PTTT_METHOD hisSesePtttMethod = new HIS_SESE_PTTT_METHOD();
                    if (IsNotNullOrEmpty(exitsSesePtttMethods))
                    {
                        hisSesePtttMethod = exitsSesePtttMethods.FirstOrDefault(o => o.PTTT_METHOD_ID == ptttMethod.HisSesePtttMethod.PTTT_METHOD_ID) ?? new HIS_SESE_PTTT_METHOD();
                    }

                    ProcessEkipMethod(ptttMethod, hisSesePtttMethod);

                    if (hisSesePtttMethod.ID > 0)
                    {
                        hisSesePtttMethod.AMOUNT = ptttMethod.HisSesePtttMethod.AMOUNT;
                        hisSesePtttMethod.PTTT_GROUP_ID = ptttMethod.HisSesePtttMethod.PTTT_GROUP_ID;
                        updateData.Add(hisSesePtttMethod);
                    }
                    else
                    {
                        hisSesePtttMethod.AMOUNT = ptttMethod.HisSesePtttMethod.AMOUNT;
                        hisSesePtttMethod.PTTT_GROUP_ID = ptttMethod.HisSesePtttMethod.PTTT_GROUP_ID;
                        hisSesePtttMethod.PTTT_METHOD_ID = ptttMethod.HisSesePtttMethod.PTTT_METHOD_ID;
                        hisSesePtttMethod.SERE_SERV_PTTT_ID = sereServPttt.ID;
                        hisSesePtttMethod.TDL_SERE_SERV_ID = sereServPttt.SERE_SERV_ID;
                        hisSesePtttMethod.TDL_SERVICE_REQ_ID = serviceReq.ID;
                        createData.Add(hisSesePtttMethod);
                    }
                }
            }

            if (IsNotNullOrEmpty(exitsSesePtttMethods))
            {
                deleteData = exitsSesePtttMethods.Where(o => !updateData.Exists(e => e.ID == o.ID)).ToList();
            }

            if (IsNotNullOrEmpty(createData) && !this.hisSesePtttMethodCreate.CreateList(createData))
            {
                throw new Exception("Rollback du lieu");
            }

            if (IsNotNullOrEmpty(updateData) && !this.hisSesePtttMethodUpdate.UpdateList(updateData))
            {
                throw new Exception("Rollback du lieu");
            }

            if (IsNotNullOrEmpty(deleteData) && !this.hisSesePtttMethodTruncate.TruncateList(deleteData))
            {
                throw new Exception("Rollback du lieu");
            }

            this.recentSesePtttMethods = data.SesePtttMethos;
        }

        private void ProcessEkipMethod(HisSesePtttMethodSDO data, HIS_SESE_PTTT_METHOD raw)
        {
            if (data.EkipUsers != null)
            {
                //Neu sere_serv chua co ekip_id va client co gui len thong tin ekip thi thi tao moi ekip
                if (!raw.EKIP_ID.HasValue && data.EkipUsers.Count > 0)
                {
                    data.EkipUsers.ForEach(o => o.EKIP_ID = 0);//reset ID (de phong client gui len ID dan den loi)
                    HIS_EKIP ekip = new HIS_EKIP();
                    ekip.HIS_EKIP_USER = data.EkipUsers;
                    if (!this.hisEkipCreate.Create(ekip))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }

                    raw.EKIP_ID = ekip.ID;
                }
                //Neu sere_serv co id roi thi xoa du lieu cu va tao du lieu moi voi ekip_id la ekip_id cua sere_serv
                else if (raw.EKIP_ID.HasValue)
                {
                    //Xoa du lieu cu
                    if (!new HisEkipUserTruncate().TruncateByEkipId(raw.EKIP_ID.Value))
                    {
                        throw new Exception("Xoa du lieu cu that bai");
                    }
                    data.EkipUsers.ForEach(o => o.EKIP_ID = raw.EKIP_ID.Value);
                    if (!this.hisEkipUserCreate.CreateList(data.EkipUsers))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }
                }
            }
        }

        private void ProcessEyeSurgry(HisSurgServiceReqUpdateSDO data, HIS_SERE_SERV_PTTT oldSereServPttt)
        {
            if (oldSereServPttt != null && oldSereServPttt.EYE_SURGRY_DESC_ID.HasValue && data.EyeSurgryDesc != null)
            {
                data.EyeSurgryDesc.ID = oldSereServPttt.EYE_SURGRY_DESC_ID.Value;

                if (!this.hisEyeSurgryDescUpdate.Update(data.EyeSurgryDesc))
                {
                    throw new Exception("Rollback du lieu");
                }
                this.recentEyeSurgryDesc = data.EyeSurgryDesc;
            }
            else if (data.EyeSurgryDesc != null)
            {
                if (!this.hisEyeSurgryDescCreate.Create(data.EyeSurgryDesc))
                {
                    throw new Exception("Rollback du lieu");
                }
                this.recentEyeSurgryDesc = data.EyeSurgryDesc;
            }
        }

        private void ProcessSkinSurgery(HisSurgServiceReqUpdateSDO data, HIS_SERE_SERV_PTTT oldSereServPttt)
        {
            if (oldSereServPttt != null && oldSereServPttt.SKIN_SURGERY_DESC_ID.HasValue && data.SkinSurgeryDesc != null)
            {
                data.SkinSurgeryDesc.ID = oldSereServPttt.SKIN_SURGERY_DESC_ID.Value;

                if (!this.hisSkinSurgeryDescUpdate.Update(data.SkinSurgeryDesc))
                {
                    throw new Exception("Rollback du lieu");
                }
                this.recentSkinSurgeryDesc = data.SkinSurgeryDesc;
            }
            else if (data.SkinSurgeryDesc != null)
            {
                if (!this.hisSkinSurgeryDescCreate.Create(data.SkinSurgeryDesc))
                {
                    throw new Exception("Rollback du lieu");
                }
                this.recentSkinSurgeryDesc = data.SkinSurgeryDesc;
            }
        }

        private void ProcessServiceReq(HisSurgServiceReqUpdateSDO data, HIS_SERVICE_REQ hisServiceReq, HIS_TREATMENT treatment)
        {
            if (hisServiceReq == null)
            {
                return;
            }

            Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
            HIS_SERVICE_REQ old = Mapper.Map<HIS_SERVICE_REQ>(hisServiceReq);//clone de phuc vu rollback

            long? beginTime = null;
            long? endTime = null;

            List<HIS_SERE_SERV_EXT> sereServExts = new HisSereServExtGet().GetByServiceReqId(hisServiceReq.ID);
            List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByServiceReqId(hisServiceReq.ID);

            hisSereServs = hisSereServs != null ? hisSereServs.Where(o => !o.IS_NO_EXECUTE.HasValue || o.IS_NO_EXECUTE.Value != Constant.IS_TRUE).ToList() : null;
            sereServExts = sereServExts != null ? sereServExts.OrderByDescending(o => o.ID).ToList() : null;

            List<HIS_SERE_SERV_EXT> currentSSExts = new List<HIS_SERE_SERV_EXT>();
            if (sereServExts != null)
            {
                foreach (HIS_SERE_SERV_EXT ssExt in sereServExts)
                {
                    if (hisSereServs == null || !hisSereServs.Exists(e => e.ID == ssExt.SERE_SERV_ID))
                    {
                        continue;
                    }
                    if (currentSSExts.Exists(e => e.SERE_SERV_ID == ssExt.SERE_SERV_ID))
                    {
                        continue;
                    }
                    currentSSExts.Add(ssExt);
                }
            }
            if (currentSSExts.Count > 0)
            {
                beginTime = currentSSExts.OrderBy(o => o.BEGIN_TIME).FirstOrDefault().BEGIN_TIME;
            }
            if (currentSSExts.Count > 0 && currentSSExts.Count == hisSereServs.Count && !currentSSExts.Exists(e => !e.END_TIME.HasValue))
            {
                endTime = currentSSExts.OrderByDescending(o => o.END_TIME).FirstOrDefault().END_TIME;
            }

            if (beginTime.HasValue)
            {
                hisServiceReq.START_TIME = beginTime;
            }
            if (endTime.HasValue && !hisSereServs.Exists(o => currentSSExts != null && currentSSExts.Exists(t => t.SERE_SERV_ID == o.ID && !t.END_TIME.HasValue)))
            {
                if (data.IsFinished)
                {
                    hisServiceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT;
                    hisServiceReq.FINISH_TIME = this.recentSereServExt.END_TIME;
                }
                hisServiceReq.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                hisServiceReq.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                hisServiceReq.EXE_WORKING_SHIFT_ID = TokenManager.GetWorkingShift();
            }

            if (data.UpdateInstructionTimeByStartTime && hisServiceReq.START_TIME.HasValue)
            {
                hisServiceReq.INTRUCTION_TIME = hisServiceReq.START_TIME.Value;
            }

            this.hasUpdateFinishTime = hisServiceReq.FINISH_TIME != old.FINISH_TIME;

            if (!this.hisServiceReqUpdate.Update(hisServiceReq, old, false))
            {
                throw new Exception("Cap nhat thong tin His_service_req that bai." + LogUtil.TraceData("hisServiceReq", hisServiceReq));
            }
        }

        private void ProcessEkip(HisSurgServiceReqUpdateSDO data, HIS_SERE_SERV raw, ref List<HIS_EKIP_USER> oldEkipUsers)
        {
            if (data.EkipUsers != null)
            {
                //Neu sere_serv chua co ekip_id va client co gui len thong tin ekip thi thi tao moi ekip
                if (!raw.EKIP_ID.HasValue && data.EkipUsers.Count > 0)
                {
                    data.EkipUsers.ForEach(o => o.EKIP_ID = 0);//reset ID (de phong client gui len ID dan den loi)
                    HIS_EKIP ekip = new HIS_EKIP();
                    ekip.HIS_EKIP_USER = data.EkipUsers;
                    if (!this.hisEkipCreate.Create(ekip))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                    this.recentEkip = ekip;
                }
                //Neu sere_serv co id roi thi xoa du lieu cu va tao du lieu moi voi ekip_id la ekip_id cua sere_serv
                else if (raw.EKIP_ID.HasValue)
                {
                    oldEkipUsers = new HisEkipUserGet().GetByEkipId(raw.EKIP_ID.Value);
                    //Xoa du lieu cu
                    if (!new HisEkipUserTruncate().TruncateByEkipId(raw.EKIP_ID.Value))
                    {
                        throw new Exception("Xoa du lieu cu that bai");
                    }
                    data.EkipUsers.ForEach(o => o.EKIP_ID = raw.EKIP_ID.Value);
                    if (!this.hisEkipUserCreate.CreateList(data.EkipUsers))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }
                }
                this.recentEkipUsers = data.EkipUsers;
            }
        }

        private void ProcessSereServ(HIS_TREATMENT hisTreatment, HIS_SERVICE_REQ serviceReq, HIS_SERE_SERV sereServ)
        {
            //co thong tin ekip_id
            bool changeEkipId = !sereServ.EKIP_ID.HasValue && this.recentEkip != null;
            if (changeEkipId)
            {
                sereServ.EKIP_ID = this.recentEkip.ID;

                HIS_SERE_SERV resultData = null;
                //Thuc hien cap nhat cac thong tin cho sere_Serv
                if (!this.hisSereServUpdate.UpdateResult(sereServ, ref resultData))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
                this.recentSereServ = resultData;
            }

            //Ho so chua khoa thi moi thuc hien xu ly cap nhat gia
            if (hisTreatment.IS_PAUSE != Constant.IS_TRUE
                && hisTreatment.IS_TEMPORARY_LOCK != Constant.IS_TRUE
                && hisTreatment.IS_LOCK_HEIN != Constant.IS_TRUE
                && hisTreatment.IS_ACTIVE == Constant.IS_TRUE)
            {
                this.AutoUpdate3day7day(sereServ, serviceReq);

                //Neu co thay doi thong tin kip thi thuc hien cap nhat lai ti le BHYT
                if (changeEkipId || this.hasUpdateFinishTime)
                {
                    this.hisSereServUpdateHein = new HisSereServUpdateHein(param, hisTreatment, true);
                    if (!this.hisSereServUpdateHein.UpdateDb())
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                }
            }
        }

        //Tu dong cap nhat du lieu goi 3/7 (nghiep vu dac thu cua vien Tim)
        private void AutoUpdate3day7day(HIS_SERE_SERV raw, HIS_SERVICE_REQ serviceReq)
        {
            List<HIS_SERE_SERV> changeRecords = null;
            List<HIS_SERE_SERV> oldOfChangeRecords = null;
            List<HIS_EXP_MEST_MEDICINE> changeRecordMedicines = null;
            List<HIS_EXP_MEST_MEDICINE> oldOfChangeRecordMedicines = null;
            List<HIS_EXP_MEST_MATERIAL> changeRecordMaterials = null;
            List<HIS_EXP_MEST_MATERIAL> oldOfChangeRecordMaterials = null;

            new HisSereServPackage37(param).Update3Day7Day(serviceReq, raw, ref changeRecords, ref oldOfChangeRecords,
                ref changeRecordMedicines, ref oldOfChangeRecordMedicines,
                ref changeRecordMaterials, ref oldOfChangeRecordMaterials);

            //Neu co su thay doi du lieu thi thuc hien cap nhat
            if (IsNotNullOrEmpty(changeRecords))
            {
                if (!this.hisSereServUpdate.UpdateList(changeRecords, oldOfChangeRecords, false))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }

            //Neu co su thay doi du lieu thi thuc hien cap nhat
            if (IsNotNullOrEmpty(changeRecordMedicines))
            {
                if (!this.hisExpMestMedicineUpdate.UpdateList(changeRecordMedicines, oldOfChangeRecordMedicines))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }

            //Neu co su thay doi du lieu thi thuc hien cap nhat
            if (IsNotNullOrEmpty(changeRecordMaterials))
            {
                if (!this.hisExpMestMaterialUpdate.UpdateList(changeRecordMaterials, oldOfChangeRecordMaterials))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }
        }

        private void ProcessSereServPttt(HisSurgServiceReqUpdateSDO data, HIS_SERVICE_REQ serviceReq, HIS_SERE_SERV_PTTT oldSereServPttt)
        {
            if (data.SereServPttt != null)
            {
                data.SereServPttt.TDL_TREATMENT_ID = serviceReq.TREATMENT_ID;
                data.SereServPttt.SERE_SERV_ID = data.SereServId;
                if (this.recentEyeSurgryDesc != null)
                {
                    data.SereServPttt.EYE_SURGRY_DESC_ID = this.recentEyeSurgryDesc.ID;
                }

                if (oldSereServPttt == null)
                {
                    data.SereServPttt.ID = 0;
                    if (!hisSereServPtttCreate.Create(data.SereServPttt))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                }
                else
                {
                    data.SereServPttt.ID = oldSereServPttt.ID;
                    if (!hisSereServPtttUpdate.Update(data.SereServPttt))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                }
                this.recentSereServPttt = data.SereServPttt;
            }
        }

        private void ProcessDeleteEyeSurgry(HisSurgServiceReqUpdateSDO data, HIS_SERE_SERV_PTTT oldSereServPttt)
        {
            if (oldSereServPttt != null && oldSereServPttt.EYE_SURGRY_DESC_ID.HasValue && this.recentSereServPttt != null && !this.recentSereServPttt.EYE_SURGRY_DESC_ID.HasValue)
            {
                if (!this.hisEyeSurgryDescTruncate.Truncate(oldSereServPttt.EYE_SURGRY_DESC_ID.Value))
                {
                    LogSystem.Warn("Xoa du lieu HIS_EYE_SURGRY_DESC that bai");
                }
            }
        }

        private void ProcessDeleteSkinSurgery(HisSurgServiceReqUpdateSDO data, HIS_SERE_SERV_PTTT oldSereServPttt)
        {
            if (oldSereServPttt != null && oldSereServPttt.SKIN_SURGERY_DESC_ID.HasValue && this.recentSereServPttt != null && !this.recentSereServPttt.SKIN_SURGERY_DESC_ID.HasValue)
            {
                if (!this.hisSkinSurgeryDescTruncate.Truncate(oldSereServPttt.SKIN_SURGERY_DESC_ID.Value))
                {
                    LogSystem.Warn("Xoa du lieu HIS_SKIN_SURGERY_DESC that bai");
                }
            }
        }

        private void ProcessSereServExt(HisSurgServiceReqUpdateSDO data, HIS_SERE_SERV sereServ, ref HIS_SERVICE_REQ serviceReq)
        {
            if (!sereServ.SERVICE_REQ_ID.HasValue)
            {
                return;
            }
            serviceReq = new HisServiceReqGet().GetById(sereServ.SERVICE_REQ_ID.Value);

            if (data.SereServExt != null)
            {
                var sse = new HisSereServExtGet().GetBySereServId(data.SereServId);

                HisSereServExtUtil.SetTdl(data.SereServExt, serviceReq);
                data.SereServExt.SERE_SERV_ID = data.SereServId;

                if (sse == null)
                {
                    data.SereServExt.ID = 0;
                    if (!hisSereServExtCreate.Create(data.SereServExt))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                }
                else
                {
                    data.SereServExt.ID = sse.ID;
                    if (!hisSereServExtUpdate.Update(data.SereServExt, true))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                }
                this.recentSereServExt = data.SereServExt;
            }
        }

        private void ProcessStentConclude(HisSurgServiceReqUpdateSDO data, HIS_SERE_SERV raw)
        {
            HisStentConcludeFilterQuery filter = new HisStentConcludeFilterQuery();
            filter.SERE_SERV_ID = raw.ID;
            List<HIS_STENT_CONCLUDE> oldSetentConcludes = new HisStentConcludeGet().Get(filter);
            
            if (IsNotNullOrEmpty(oldSetentConcludes))
            {
                //Xoa du lieu cu
                if (!hisStentConcludeTruncate.TruncateList(oldSetentConcludes))
                {
                    throw new Exception("Xoa du lieu List<HIS_STENT_CONCLUDE> cu that bai");
                }
            }

            if (IsNotNullOrEmpty(data.StentConcludes))
            {
                if (!this.hisStentConcludeCreate.CreateList(data.StentConcludes))
                {
                    LogSystem.Warn("tao moi List<HIS_STENT_CONCLUDE> that bai");
                    throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                }
                this.recentStentConcludes = data.StentConcludes;
            }
            else
            {
                this.recentStentConcludes = oldSetentConcludes;
            }
        }
       
        private void PassResult(ref HisSurgServiceReqUpdateSDO resultData)
        {
            resultData = new HisSurgServiceReqUpdateSDO();
            resultData.SereServPttt = this.recentSereServPttt;
            resultData.EkipUsers = this.recentEkipUsers;
            resultData.SereServExt = this.recentSereServExt;
            resultData.EyeSurgryDesc = this.recentEyeSurgryDesc;
            resultData.SesePtttMethos = this.recentSesePtttMethods;
            resultData.StentConcludes = this.recentStentConcludes;
        }

        private void ProcessEventLogEkipUser(HIS_SERVICE_REQ serviceReq, HIS_SERE_SERV sereServ, List<HIS_EKIP_USER> oldEkipUsers, List<HIS_EKIP_USER> newEkipUsers)
        {
            try
            {
                if (!IsNotNullOrEmpty(oldEkipUsers) && !IsNotNullOrEmpty(newEkipUsers)) return;

                List<string> logs = new List<string>();
                if (IsNotNullOrEmpty(oldEkipUsers))
                {
                    List<string> users = new List<string>();
                    foreach (HIS_EKIP_USER item in oldEkipUsers)
                    {
                        HIS_EXECUTE_ROLE role = HisExecuteRoleCFG.DATA.FirstOrDefault(o => o.ID == item.EXECUTE_ROLE_ID);
                        string roleName = role != null ? role.EXECUTE_ROLE_NAME : "";
                        users.Add(String.Format("{0}-{1}({2})", item.LOGINNAME ?? "", item.USERNAME ?? "", roleName));
                    }
                    logs.Add(String.Format("({0})", String.Join(", ", users)));
                }
                else
                {
                    logs.Add("()");
                }

                if (IsNotNullOrEmpty(newEkipUsers))
                {
                    List<string> users = new List<string>();
                    foreach (HIS_EKIP_USER item in newEkipUsers)
                    {
                        HIS_EXECUTE_ROLE role = HisExecuteRoleCFG.DATA.FirstOrDefault(o => o.ID == item.EXECUTE_ROLE_ID);
                        string roleName = role != null ? role.EXECUTE_ROLE_NAME : "";
                        users.Add(String.Format("{0}-{1}({2})", item.LOGINNAME ?? "", item.USERNAME ?? "", roleName));
                    }
                    logs.Add(String.Format("({0})", String.Join(", ", users)));
                }
                else
                {
                    logs.Add("()");
                }

                new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisServiceReq_ChinhSuaEkipThucHien, sereServ.TDL_SERVICE_NAME, String.Join(" => ", logs))
                .TreatmentCode(serviceReq.TDL_TREATMENT_CODE)
                .ServiceReqCode(serviceReq.SERVICE_REQ_CODE)
                .Run();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void RollbackData()
        {
            this.hisSesePtttMethodUpdate.RollbackData();
            this.hisSereServPtttCreate.RollbackData();
            this.hisSereServPtttUpdate.RollbackData();
            this.hisEyeSurgryDescCreate.RollbackData();
            this.hisEyeSurgryDescUpdate.RollbackData();
            this.hisSesePtttMethodCreate.RollbackData();
            if (this.hisSereServUpdateHein != null)
            {
                this.hisSereServUpdateHein.RollbackData();
            }
            this.hisSereServUpdate.RollbackData();
            this.hisExpMestMedicineUpdate.RollbackData();
            this.hisExpMestMaterialUpdate.RollbackData();
            this.hisEkipCreate.RollbackData();
            this.hisEkipUserUpdate.RollbackData();
            this.hisEkipUserCreate.RollbackData();
            this.hisSereServExtCreate.RollbackData();
            this.hisSereServExtUpdate.RollbackData();
            this.hisServiceReqUpdate.RollbackData();
            this.hisStentConcludeCreate.RollbackData();
        }
    }
}
