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
using MOS.MANAGER.HisSesePtttMethod;
using MOS.MANAGER.HisSkinSurgeryDesc;
using MOS.MANAGER.HisStentConclude;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.Token;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Surg.Process
{
    class EkipData
    {
        public List<HIS_EKIP_USER> NewEkipUsers { get; set; }
        public List<HIS_EKIP_USER> OldEkipUsers { get; set; }
        public HIS_SERE_SERV SereServ { get; set; }
    }

    /// <summary>
    /// Xu ly phau thuat thu thuat
    /// </summary>
    class HisServiceReqSurgProcessList : BusinessBase
    {
        private HisSereServPtttCreate hisSereServPtttCreate;
        private HisSereServPtttUpdate hisSereServPtttUpdate;
        private HisEyeSurgryDescCreate hisEyeSurgryDescCreate;
        private HisEyeSurgryDescUpdate hisEyeSurgryDescUpdate;
        private HisEyeSurgryDescTruncate hisEyeSurgryDescTruncate;
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
        private HisSkinSurgeryDescCreate hisSkinSurgeryDescCreate;
        private HisSkinSurgeryDescUpdate hisSkinSurgeryDescUpdate;
        private HisSkinSurgeryDescTruncate hisSkinSurgeryDescTruncate;
        private HisSesePtttMethodCreate hisSesePtttMethodCreate;
        private HisSesePtttMethodUpdate hisSesePtttMethodUpdate;
        private HisSesePtttMethodTruncate hisSesePtttMethodTruncate;
        private HisStentConcludeCreate hisStentConcludeCreate;
        private HisStentConcludeTruncate hisStentConcludeTruncate;

        private bool hasUpdateFinishTime;

        internal HisServiceReqSurgProcessList()
            : base()
        {
            this.Init();
        }

        internal HisServiceReqSurgProcessList(CommonParam paramUpdate)
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

        internal bool Run(HisSurgServiceReqUpdateListSDO data, ref HisSurgServiceReqUpdateListSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TREATMENT hisTreatment = null;
                HIS_SERVICE_REQ serviceReq = null;
                List<HIS_SERE_SERV> sereServs = null;

                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                HisSereServCheck sereServChecker = new HisSereServCheck(param);
                HisServiceReqCheck serviceReqChecker = new HisServiceReqCheck(param);
                HisServiceReqSurgProcessCheck checker = new HisServiceReqSurgProcessCheck(param);

                valid = valid && checker.IsValidData(data, ref sereServs, ref serviceReq, ref hisTreatment);
                valid = valid && serviceReqChecker.IsNotAprovedSurgeryRemuneration(serviceReq);

                if (!HisServiceReqCFG.ALLOW_UPDATE_SURG_INFO_AFTER_LOCKING_TREATMENT)
                {
                    valid = valid && treatmentChecker.IsUnLock(hisTreatment);
                    valid = valid && treatmentChecker.IsUnpause(hisTreatment);
                    valid = valid && treatmentChecker.IsUnTemporaryLock(hisTreatment);
                    valid = valid && treatmentChecker.IsUnLockHein(hisTreatment);
                }

                if (valid)
                {
                    List<HIS_SERE_SERV_PTTT> oldSereServPttts = new HisSereServPtttGet().GetBySereServIds(data.SurgUpdateSDOs.Select(o => o.SereServId).ToList());
                    //List<HIS_STENT_CONCLUDE> oldStentConcludes = new HisStentConcludeGet().GetBySereServIds(data.SurgUpdateSDOs.Select(o => o.SereServId).ToList());
                    List<HIS_SERE_SERV_EXT> sereServExts = new List<HIS_SERE_SERV_EXT>();

                    bool updatePrice = false;

                    List<EkipData> ekipLogs = new List<EkipData>();

                    foreach (SurgUpdateSDO sdo in data.SurgUpdateSDOs)
                    {
                        HIS_SERE_SERV sereServ = sereServs != null ? sereServs.Where(o => o.ID == sdo.SereServId).FirstOrDefault() : null;
                        HIS_SERE_SERV_PTTT oldSereServPttt = oldSereServPttts != null ? oldSereServPttts.Where(o => o.SERE_SERV_ID == sdo.SereServId).FirstOrDefault() : null;
                        HIS_EKIP ekip = null;
                        List<HIS_EKIP_USER> newEkipUsers = null;
                        List<HIS_EKIP_USER> oldEkipUsers = null;
                        HIS_SERE_SERV_PTTT newSereServPttt = null;
                        HIS_EYE_SURGRY_DESC newEyeSurgeryDesc = null;
                        HIS_SKIN_SURGERY_DESC newSkinSurgeryDesc = null;
                        List<HIS_STENT_CONCLUDE> recentStentConcludes = null;

                        bool? updateSerePrice = false;

                        if (sereServ != null)
                        {
                            this.ProcessEkip(sdo, sereServ, ref oldEkipUsers, ref ekip, ref newEkipUsers);
                            this.ProcessSereServExt(sdo, sereServ, serviceReq, ref sereServExts);
                            this.ProcessSereServ(hisTreatment, serviceReq, sereServ, ekip, ref updateSerePrice);
                            this.ProcessEyeSurgry(sdo, oldSereServPttt, ref newEyeSurgeryDesc);
                            this.ProcessSkinSurgery(sdo, oldSereServPttt, ref newSkinSurgeryDesc);
                            this.ProcessSereServPttt(sdo, serviceReq, oldSereServPttt, newEyeSurgeryDesc, newSkinSurgeryDesc, ref newSereServPttt);
                            this.ProcessDeleteEyeSurgry(sdo, oldSereServPttt, newSereServPttt);
                            this.ProcessDeleteSkinSurgery(oldSereServPttt, newSereServPttt);
                            this.ProcessSesePtttMethod(sdo, newSereServPttt, serviceReq);
                            this.ProcessStentConclude(sdo, ref recentStentConcludes, sereServ);
                        }
                        updatePrice = updatePrice || (updateSerePrice.HasValue && updateSerePrice.Value);

                        EkipData ekipData = new EkipData();
                        ekipData.NewEkipUsers = newEkipUsers;
                        ekipData.OldEkipUsers = oldEkipUsers;
                        ekipData.SereServ = sereServ;

                        ekipLogs.Add(ekipData);
                    }

                    this.ProcessServiceReq(data.UpdateInstructionTimeByStartTime, serviceReq, hisTreatment, sereServs, sereServExts, data.IsFinished);

                    //Neu co thay doi thong tin kip thi thuc hien cap nhat lai ti le BHYT
                    if (updatePrice)
                    {
                        this.hisSereServUpdateHein = new HisSereServUpdateHein(param, hisTreatment, true);
                        if (!this.hisSereServUpdateHein.UpdateDb())
                        {
                            throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                        }
                    }

                    this.ProcessEventLogEkipUser(serviceReq, ekipLogs);

                    this.PassResult(data, ref resultData);

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

        private void ProcessSkinSurgery(SurgUpdateSDO data, HIS_SERE_SERV_PTTT oldSereServPttt, ref HIS_SKIN_SURGERY_DESC newSkinSurgeryDesc)
        {
            if (oldSereServPttt != null && oldSereServPttt.SKIN_SURGERY_DESC_ID.HasValue && data.SkinSurgeryDesc != null)
            {
                data.SkinSurgeryDesc.ID = oldSereServPttt.SKIN_SURGERY_DESC_ID.Value;

                if (!this.hisSkinSurgeryDescUpdate.Update(data.SkinSurgeryDesc))
                {
                    throw new Exception("Rollback du lieu");
                }
            }
            else if (data.SkinSurgeryDesc != null)
            {
                if (!this.hisSkinSurgeryDescCreate.Create(data.SkinSurgeryDesc))
                {
                    throw new Exception("Rollback du lieu");
                }
            }
            newSkinSurgeryDesc = data.SkinSurgeryDesc;
        }

        private void ProcessEyeSurgry(SurgUpdateSDO data, HIS_SERE_SERV_PTTT oldSereServPttt, ref HIS_EYE_SURGRY_DESC newEyeSurgryDesc)
        {
            if (oldSereServPttt != null && oldSereServPttt.EYE_SURGRY_DESC_ID.HasValue && data.EyeSurgryDesc != null)
            {
                data.EyeSurgryDesc.ID = oldSereServPttt.EYE_SURGRY_DESC_ID.Value;

                if (!this.hisEyeSurgryDescUpdate.Update(data.EyeSurgryDesc))
                {
                    throw new Exception("Rollback du lieu");
                }
            }
            else if (data.EyeSurgryDesc != null)
            {
                if (!this.hisEyeSurgryDescCreate.Create(data.EyeSurgryDesc))
                {
                    throw new Exception("Rollback du lieu");
                }
            }
            newEyeSurgryDesc = data.EyeSurgryDesc;
        }

        private void ProcessDeleteSkinSurgery(HIS_SERE_SERV_PTTT oldSereServPttt, HIS_SERE_SERV_PTTT newSereServPttt)
        {
            if (oldSereServPttt != null && oldSereServPttt.SKIN_SURGERY_DESC_ID.HasValue && newSereServPttt != null && !newSereServPttt.SKIN_SURGERY_DESC_ID.HasValue)
            {
                if (!this.hisSkinSurgeryDescTruncate.Truncate(oldSereServPttt.SKIN_SURGERY_DESC_ID.Value))
                {
                    LogSystem.Warn("Xoa du lieu HIS_SKIN_SURGERY_DESC that bai");
                }
            }
        }

        private void ProcessServiceReq(bool updateInstructionTimeByStartTime, HIS_SERVICE_REQ hisServiceReq, HIS_TREATMENT treatment, List<HIS_SERE_SERV> hisSereServs, List<HIS_SERE_SERV_EXT> sereServExts, bool isFinished)
        {
            if (hisServiceReq == null)
            {
                return;
            }

            Mapper.CreateMap<HIS_SERVICE_REQ, HIS_SERVICE_REQ>();
            HIS_SERVICE_REQ old = Mapper.Map<HIS_SERVICE_REQ>(hisServiceReq);//clone de phuc vu rollback

            long? beginTime = null;
            long? endTime = null;

            sereServExts = sereServExts != null ? sereServExts.OrderByDescending(o => o.ID).ToList() : null;

            if (IsNotNullOrEmpty(sereServExts))
            {
                beginTime = sereServExts.OrderBy(o => o.BEGIN_TIME).FirstOrDefault().BEGIN_TIME;
            }
            if (IsNotNullOrEmpty(sereServExts) && sereServExts.Count == hisSereServs.Count && !sereServExts.Exists(e => !e.END_TIME.HasValue))
            {
                endTime = sereServExts.OrderByDescending(o => o.END_TIME).FirstOrDefault().END_TIME;
            }

            if (beginTime.HasValue)
            {
                hisServiceReq.START_TIME = beginTime;
            }
            if (endTime.HasValue)
            {
                if (isFinished)
                {
                    hisServiceReq.SERVICE_REQ_STT_ID = IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_STT.ID__HT;
                    hisServiceReq.FINISH_TIME = endTime;
                }
                hisServiceReq.EXECUTE_LOGINNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();
                hisServiceReq.EXECUTE_USERNAME = Inventec.Token.ResourceSystem.ResourceTokenManager.GetUserName();
                hisServiceReq.EXE_WORKING_SHIFT_ID = TokenManager.GetWorkingShift();
            }

            if (updateInstructionTimeByStartTime && hisServiceReq.START_TIME.HasValue)
            {
                hisServiceReq.INTRUCTION_TIME = hisServiceReq.START_TIME.Value;
            }

            this.hasUpdateFinishTime = hisServiceReq.FINISH_TIME != old.FINISH_TIME;

            if (!this.hisServiceReqUpdate.Update(hisServiceReq, old, false))
            {
                throw new Exception("Cap nhat thong tin His_service_req that bai." + LogUtil.TraceData("hisServiceReq", hisServiceReq));
            }
        }

        private void ProcessEkip(SurgUpdateSDO data, HIS_SERE_SERV sereServ, ref List<HIS_EKIP_USER> oldEkipUsers, ref HIS_EKIP ekip, ref List<HIS_EKIP_USER> newEkipUsers)
        {
            if (data.EkipUsers != null)
            {
                //Neu sere_serv chua co ekip_id va client co gui len thong tin ekip thi thi tao moi ekip
                if (!sereServ.EKIP_ID.HasValue && data.EkipUsers.Count > 0)
                {
                    data.EkipUsers.ForEach(o => o.EKIP_ID = 0);//reset ID (de phong client gui len ID dan den loi)
                    ekip = new HIS_EKIP();
                    ekip.HIS_EKIP_USER = data.EkipUsers;
                    if (!this.hisEkipCreate.Create(ekip))
                    {
                        throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                    }
                }
                //Neu sere_serv co id roi thi xoa du lieu cu va tao du lieu moi voi ekip_id la ekip_id cua sere_serv
                else if (sereServ.EKIP_ID.HasValue)
                {
                    oldEkipUsers = new HisEkipUserGet().GetByEkipId(sereServ.EKIP_ID.Value);
                    //Xoa du lieu cu
                    if (!new HisEkipUserTruncate().TruncateByEkipId(sereServ.EKIP_ID.Value))
                    {
                        throw new Exception("Xoa du lieu cu that bai");
                    }
                    data.EkipUsers.ForEach(o => o.EKIP_ID = sereServ.EKIP_ID.Value);
                    if (!this.hisEkipUserCreate.CreateList(data.EkipUsers))
                    {
                        throw new Exception("Ket thuc nghiep vu");
                    }
                }
                newEkipUsers = data.EkipUsers;
            }
        }

        private void ProcessSereServ(HIS_TREATMENT hisTreatment, HIS_SERVICE_REQ serviceReq, HIS_SERE_SERV sereServ, HIS_EKIP ekip, ref bool? updatePrice)
        {
            //co thong tin ekip_id
            bool changeEkipId = !sereServ.EKIP_ID.HasValue && ekip != null;
            if (changeEkipId)
            {
                sereServ.EKIP_ID = ekip.ID;

                HIS_SERE_SERV resultData = null;
                //Thuc hien cap nhat cac thong tin cho sere_Serv
                if (!this.hisSereServUpdate.UpdateResult(sereServ, ref resultData))
                {
                    throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                }
            }

            //Ho so chua khoa thi moi thuc hien xu ly cap nhat gia
            if (hisTreatment.IS_PAUSE != Constant.IS_TRUE
                && hisTreatment.IS_TEMPORARY_LOCK != Constant.IS_TRUE
                && hisTreatment.IS_LOCK_HEIN != Constant.IS_TRUE
                && hisTreatment.IS_ACTIVE == Constant.IS_TRUE)
            {
                this.AutoUpdate3day7day(sereServ, serviceReq);

                updatePrice = changeEkipId || this.hasUpdateFinishTime;
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

        private void ProcessSereServPttt(SurgUpdateSDO data, HIS_SERVICE_REQ serviceReq, HIS_SERE_SERV_PTTT oldSereServPttt, HIS_EYE_SURGRY_DESC eyeSurgryDesc, HIS_SKIN_SURGERY_DESC skinSurgeryDesc, ref HIS_SERE_SERV_PTTT newSereServPttt)
        {
            if (data.SereServPttt != null)
            {
                data.SereServPttt.TDL_TREATMENT_ID = serviceReq.TREATMENT_ID;
                data.SereServPttt.SERE_SERV_ID = data.SereServId;

                if (eyeSurgryDesc != null)
                {
                    data.SereServPttt.EYE_SURGRY_DESC_ID = eyeSurgryDesc.ID;
                }
                if (skinSurgeryDesc != null)
                {
                    data.SereServPttt.SKIN_SURGERY_DESC_ID = skinSurgeryDesc.ID;
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
                newSereServPttt = data.SereServPttt;
            }
        }
        private void ProcessStentConclude(SurgUpdateSDO data, ref List<HIS_STENT_CONCLUDE> recentStentConcludes, HIS_SERE_SERV sereServ)
        {
            HisStentConcludeFilterQuery filter = new HisStentConcludeFilterQuery();
            filter.SERE_SERV_ID = sereServ.ID;
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
                recentStentConcludes = data.StentConcludes;
            }
            else
            {
                recentStentConcludes = oldSetentConcludes;
            }
        }
        
        private void ProcessDeleteEyeSurgry(SurgUpdateSDO data, HIS_SERE_SERV_PTTT oldSereServPttt, HIS_SERE_SERV_PTTT newSereServPttt)
        {
            if (oldSereServPttt != null && oldSereServPttt.EYE_SURGRY_DESC_ID.HasValue && newSereServPttt != null && !newSereServPttt.EYE_SURGRY_DESC_ID.HasValue)
            {
                if (!this.hisEyeSurgryDescTruncate.Truncate(oldSereServPttt.EYE_SURGRY_DESC_ID.Value))
                {
                    LogSystem.Warn("Xoa du lieu HIS_EYE_SURGRY_DESC that bai");
                }
            }
        }

        private void ProcessSereServExt(SurgUpdateSDO data, HIS_SERE_SERV sereServ, HIS_SERVICE_REQ serviceReq, ref List<HIS_SERE_SERV_EXT> sereServExts)
        {
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
                sereServExts.Add(data.SereServExt);
            }
        }

        private void PassResult(HisSurgServiceReqUpdateListSDO data, ref HisSurgServiceReqUpdateListSDO resultData)
        {
            resultData = data;
        }

        private void ProcessEventLogEkipUser(HIS_SERVICE_REQ serviceReq, List<EkipData> ekipLogs)
        {
            try
            {
                if (!IsNotNullOrEmpty(ekipLogs))
                {
                    return;
                }

                foreach (EkipData ekip in ekipLogs)
                {
                    if (!IsNotNullOrEmpty(ekip.OldEkipUsers) && !IsNotNullOrEmpty(ekip.NewEkipUsers))
                    {
                        return;
                    }

                    List<string> logs = new List<string>();
                    if (IsNotNullOrEmpty(ekip.OldEkipUsers))
                    {
                        List<string> users = new List<string>();
                        foreach (HIS_EKIP_USER item in ekip.OldEkipUsers)
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

                    if (IsNotNullOrEmpty(ekip.NewEkipUsers))
                    {
                        List<string> users = new List<string>();
                        foreach (HIS_EKIP_USER item in ekip.NewEkipUsers)
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

                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisServiceReq_ChinhSuaEkipThucHien, ekip.SereServ.TDL_SERVICE_NAME, String.Join(" => ", logs))
                    .TreatmentCode(serviceReq.TDL_TREATMENT_CODE)
                    .ServiceReqCode(serviceReq.SERVICE_REQ_CODE)
                    .Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void ProcessSesePtttMethod(SurgUpdateSDO data, HIS_SERE_SERV_PTTT sereServPttt, HIS_SERVICE_REQ serviceReq)
        {
            List<HIS_SESE_PTTT_METHOD> exitsSesePtttMethods = new List<HIS_SESE_PTTT_METHOD>();
            if (IsNotNull(sereServPttt))
            {
                HisSesePtttMethodFilterQuery filter = new HisSesePtttMethodFilterQuery();
                filter.SERE_SERV_PTTT_ID = sereServPttt.ID;
                exitsSesePtttMethods = new HisSesePtttMethodGet().Get(filter);
            }

            List<HIS_SESE_PTTT_METHOD> createData = new List<HIS_SESE_PTTT_METHOD>();
            List<HIS_SESE_PTTT_METHOD> updateData = new List<HIS_SESE_PTTT_METHOD>();
            List<HIS_SESE_PTTT_METHOD> deleteData = new List<HIS_SESE_PTTT_METHOD>();

            if (IsNotNullOrEmpty(data.SesePtttMethos))
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

        private void RollbackData()
        {
            this.hisSesePtttMethodCreate.RollbackData();
            this.hisSesePtttMethodUpdate.RollbackData();
            this.hisSereServPtttCreate.RollbackData();
            this.hisSereServPtttUpdate.RollbackData();
            this.hisEyeSurgryDescCreate.RollbackData();
            this.hisEyeSurgryDescUpdate.RollbackData();
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
