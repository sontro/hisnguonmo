using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.ObjectChecker;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryHein.Bhyt;
using MOS.LibraryHein.Common;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisKskContract;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.MANAGER.HisSereServ.Delete;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisServiceReq;
using MOS.MANAGER.HisTreatment;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisSereServ
{
    internal partial class HisSereServUpdateHein : BusinessBase
    {
        private HisSereServCreate hisSereServCreate;
        private HisSereServUpdateSql hisSereServUpdate;
        private HisSereServDeleteSql hisSereServDelete;
        private SetPriceWithBhytPolicy setPriceWithBhytPolicy;
        private HisSereServSetInfo addInfo;
        private List<HIS_SERE_SERV> beforeUpdates;
        private List<HIS_PATIENT_TYPE_ALTER> patientTypeAlters;
        private long? recentEmergencyDepartmentId;
        private long? recentEmergencyDepartmentInTime;
        private long? recentEmergencyDepartmentOutTime;

        private bool verifyTreatment;
        private HIS_TREATMENT treatment;

        internal HisSereServUpdateHein(CommonParam paramUpdate, HIS_TREATMENT treatment, bool verifyTreatment)
            : base(paramUpdate)
        {
            this.patientTypeAlters = new HisPatientTypeAlterGet().GetByTreatmentId(treatment.ID);
            this.Init(treatment, verifyTreatment);
        }

        internal HisSereServUpdateHein(CommonParam paramUpdate, HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> ptas, bool verifyTreatment)
            : base(paramUpdate)
        {
            this.patientTypeAlters = ptas;
            this.Init(treatment, verifyTreatment);
        }

        internal HisSereServUpdateHein(CommonParam paramUpdate, HIS_TREATMENT treatment, List<HIS_PATIENT_TYPE_ALTER> ptas, bool verifyTreatment, long? recentEmergencyDepartmentId, long? recentEmergencyDepartmentInTime, long? recentEmergencyDepartmentOutTime)
            : base(paramUpdate)
        {
            this.patientTypeAlters = ptas;
            this.Init(treatment, verifyTreatment);
            this.recentEmergencyDepartmentId = recentEmergencyDepartmentId;
            this.recentEmergencyDepartmentInTime = recentEmergencyDepartmentInTime;
            this.recentEmergencyDepartmentOutTime = recentEmergencyDepartmentOutTime;
        }

        private void Init(HIS_TREATMENT treatment, bool verifyTreatment)
        {
            this.hisSereServCreate = new HisSereServCreate(param);
            this.hisSereServUpdate = new HisSereServUpdateSql(param);
            this.hisSereServDelete = new HisSereServDeleteSql(param);
            this.setPriceWithBhytPolicy = new SetPriceWithBhytPolicy(param);
            this.addInfo = new HisSereServSetInfo(param, this.patientTypeAlters);
            this.verifyTreatment = verifyTreatment;
            this.treatment = treatment;
        }

        /// <summary>
        /// Cap nhat toan bo thong tin lien quan den gia va thong tin BHYT cua tat ca cac
        /// sere_serv lien quan trong cung treatment
        /// </summary>
        /// <returns></returns>
        public bool UpdateDb()
        {
            List<HIS_SERE_SERV> listRaw = new HisSereServGet().GetHasExecuteByTreatmentId(this.treatment.ID);
            return this.UpdateDb(listRaw);
        }

        /// <summary>
        /// Cap nhat toan bo thong tin lien quan den gia va thong tin BHYT cua tat ca cac
        /// sere_serv lien quan trong cung treatment
        /// </summary>
        /// <returns></returns>
        public bool UpdateDb(List<HIS_SERE_SERV> listRaw)
        {
            Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
            List<HIS_SERE_SERV> olds = Mapper.Map<List<HIS_SERE_SERV>>(listRaw);

            List<long> appliedSereServIds = listRaw != null ? listRaw.Select(o => o.ID).ToList() : null;
            SereServPriceUtil.ReloadPrice(param, treatment, listRaw, appliedSereServIds);

            return this.UpdateDb(olds, listRaw);
        }

        /// <summary>
        /// Cap nhat toan bo thong tin lien quan den gia va thong tin BHYT cua tat ca cac
        /// sere_serv lien quan trong cung treatment
        /// </summary>
        /// <returns></returns>
        public bool UpdateDb(List<HIS_SERE_SERV> olds, List<HIS_SERE_SERV> listSereServ)
        {
            bool result = true;
            try
            {
                List<HIS_SERE_SERV> changeRecords = null;
                List<HIS_SERE_SERV> oldOfChangeRecords = null;
                if (this.Update(olds, listSereServ, ref changeRecords, ref oldOfChangeRecords))
                {
                    if (IsNotNullOrEmpty(changeRecords))
                    {
                        this.beforeUpdates = oldOfChangeRecords;//phuc vu rollback

                        List<HIS_SERE_SERV> listToUpdate = changeRecords.Where(o => o.IS_DELETE != 1).ToList();
                        List<HIS_SERE_SERV> listToDelete = changeRecords.Where(o => o.IS_DELETE == 1).ToList();

                        List<HIS_SERE_SERV> allChanges = new List<HIS_SERE_SERV>();
                        if (IsNotNullOrEmpty(listToUpdate) || IsNotNullOrEmpty(listToDelete))
                        {
                            List<long> sereServIds = new List<long>();
                            if (IsNotNullOrEmpty(listToUpdate))
                            {
                                allChanges.AddRange(listToUpdate);
                            }
                            if (IsNotNullOrEmpty(listToDelete))
                            {
                                allChanges.AddRange(listToDelete);
                            }
                        }

                        if (IsNotNullOrEmpty(listToUpdate) && !this.hisSereServUpdate.Run(listToUpdate))
                        {
                            throw new Exception("Ket thuc nghiep vu");
                        }

                        if (IsNotNullOrEmpty(listToDelete) && !this.hisSereServDelete.Run(listToDelete))
                        {
                            throw new Exception("Ket thuc nghiep vu");
                        }
                    }
                }
                else
                {
                    result = false;
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

        public bool Update(List<HIS_SERE_SERV> sereServs)
        {
            List<HIS_SERE_SERV> changeRecords = null;
            List<HIS_SERE_SERV> oldOfChangeRecords = null;
            return this.Update(sereServs, ref changeRecords, ref oldOfChangeRecords);
        }

        public bool Update(List<HIS_SERE_SERV> sereServs, ref List<HIS_SERE_SERV> changeRecords, ref List<HIS_SERE_SERV> oldOfChangeRecords)
        {
            return this.Update(null, sereServs, ref changeRecords, ref oldOfChangeRecords);
        }

        public bool Update(List<HIS_SERE_SERV> olds, List<HIS_SERE_SERV> sereServs, ref List<HIS_SERE_SERV> changeRecords, ref List<HIS_SERE_SERV> oldOfChangeRecords)
        {
            return this.Update(olds, sereServs, null, ref changeRecords, ref oldOfChangeRecords);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="olds"></param>
        /// <param name="sereServs"></param>
        /// <param name="newSereServs">Cac sere_serv them moi (phuc vu nghiep vu "quy chi tra")</param>
        /// <param name="changeRecords"></param>
        /// <param name="oldOfChangeRecords"></param>
        /// <returns></returns>
        public bool Update(List<HIS_SERE_SERV> olds, List<HIS_SERE_SERV> sereServs, List<HIS_SERE_SERV> toSetFundAcceptedSereServs, ref List<HIS_SERE_SERV> changeRecords, ref List<HIS_SERE_SERV> oldOfChangeRecords)
        {
            bool result = false;
            try
            {
                //neu danh sach sere_serv rong thi ket thuc xu ly
                if (!IsNotNullOrEmpty(sereServs))
                {
                    return true;
                }

                bool valid = true;

                //chi check treatment khi co y/c.
                //Tranh check treatment nhieu lan trong 1 nghiep vu (treatment, service_req, sere_serv, ...)
                if (this.verifyTreatment)
                {
                    HisTreatmentCheck treatmentCheck = new HisTreatmentCheck(param);
                    valid = valid && treatmentCheck.IsUnLock(this.treatment);
                    valid = valid && treatmentCheck.IsUnTemporaryLock(treatment);
                    valid = valid && treatmentCheck.IsUnLockHein(this.treatment);
                }

                if (valid)
                {
                    //Xac dinh loai dieu tri cua BN hien tai la j (kham hay dieu tri)
                    HIS_PATIENT_TYPE_ALTER lastPta = this.GetLastPta();

                    List<HIS_SERE_SERV> beforeChanges = null;
                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();

                    //Trong truong hop ben ngoai co truyen vao du lieu olds va sereServs la 2 danh sach doc lap
                    //==> khi do duoc hieu, olds la d/s lay ban dau tu DB va sere_serv la da duoc xu ly 1 phan
                    //==> khi do, se co nhu cau lay d/s thay doi theo d/s lay tu trong DB
                    if (olds == null)
                    {
                        beforeChanges = Mapper.Map<List<HIS_SERE_SERV>>(sereServs);
                    }
                    else
                    {
                        beforeChanges = Mapper.Map<List<HIS_SERE_SERV>>(olds);
                    }

                    if (lastPta != null)
                    {
                        //Ko xu ly cac du lieu "ko thuc hien"
                        var hasExecuteSereServs = sereServs
                            .Where(o => o.IS_NO_EXECUTE == null || o.IS_NO_EXECUTE != MOS.UTILITY.Constant.IS_TRUE)
                            .Where(o => o.SERVICE_REQ_ID.HasValue && o.IS_DELETE != Constant.IS_TRUE)
                            .ToList();
                        if (IsNotNullOrEmpty(hasExecuteSereServs))
                        {   
                            //this.UpdatePrice(sereServs);

                            //Xu ly nghiep vu dinh muc hao phi
                            this.UpdateExpendQuota(hasExecuteSereServs);

                            //lay cac dich vu ko tinh dich vu luu so tien vuot hao phi de xu ly tiep
                            List<HIS_SERE_SERV> noExpendOvers = hasExecuteSereServs.Where(o => o.SERVICE_ID != HisServiceCFG.SERVICE_ID__OVER_EXPEND).ToList();

                            //Set lai gia va cac thong tin lien quan cho sere_serv
                            this.AddInfo(noExpendOvers);

                            //Cap nhat cho cac dich vu BHYT
                            this.UpdateBhyt(noExpendOvers, treatment, lastPta);

                            //Cap nhat cho cac dich vu con lai
                            this.UpdateOther(noExpendOvers, lastPta, treatment);

                            //Luu y: can chay lenh nay sau khi xu ly BHYT (do class nay luc tinh toan co su dung
                            //cac ket qua dau ra cua phan xu ly BHYT
                            this.SetOtherSourcePrice(noExpendOvers, treatment, lastPta);

                            //Cap nhat gia phụ thu cho sere_serv
                            //(luu y: cap nhat gia phu thu can xu ly cuoi cung, sau khi xu ly cac tinh toan tien BHYT, KSK, ...)
                            this.UpdateAddprice(noExpendOvers);

                            //Bo sung nghiep vu tu dong nhap so tien mien giam
                            this.SetDiscount(hasExecuteSereServs, treatment);

                            new HisSereServSetFundAccepted(param).Run(toSetFundAcceptedSereServs, hasExecuteSereServs, treatment);
                        }
                        
                        List<HIS_SERE_SERV> afterChanges = Mapper.Map<List<HIS_SERE_SERV>>(sereServs);
                        HisSereServUtil.GetChangeRecord(beforeChanges, afterChanges, ref changeRecords, ref oldOfChangeRecords);

                        if (new HisSereServCheck(param).AllowUpdate(changeRecords, beforeChanges))
                        {
                            result = true;
                        }
                    }
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

        private void UpdateExpendQuota(List<HIS_SERE_SERV> sereServs)
        {
            //neu co cau hinh dich vu de luu so tien trong truong hop vuot qua dinh muc hao phi
            //thi moi tiep tuc xu ly
            if (HisServiceCFG.SERVICE_ID__OVER_EXPEND > 0 && IsNotNullOrEmpty(sereServs))
            {
                //lay dich vu chinh ma co chua cac dich vu hao phi
                List<long> parentIds = sereServs.Where(o => o.IS_EXPEND == MOS.UTILITY.Constant.IS_TRUE && o.PARENT_ID.HasValue).Select(o => o.PARENT_ID.Value).Distinct().ToList();
                if (IsNotNullOrEmpty(parentIds))
                {
                    foreach (long parentId in parentIds)
                    {
                        HIS_SERE_SERV parent = sereServs.Where(o => o.ID == parentId).FirstOrDefault();
                        //kiem tra xem da co dich vu nao de luu so tien vuot hao phi chua
                        HIS_SERE_SERV overExpendService = sereServs.Where(o => o.SERVICE_ID == HisServiceCFG.SERVICE_ID__OVER_EXPEND && o.PARENT_ID == parentId).FirstOrDefault();
                        V_HIS_SERVICE parentService = HisServiceCFG.DATA_VIEW.Where(o => o.ID == parent.SERVICE_ID).FirstOrDefault();
                        if (parent != null && parentService != null && parentService.MAX_EXPEND.HasValue)
                        {
                            //Lay ra gia cac dich vu hao phi gan voi dich vu
                            decimal totalExpendChildren = sereServs
                                .Where(o => o.PARENT_ID.HasValue
                                    && o.PARENT_ID.Value == parentId
                                    && o.SERVICE_ID != HisServiceCFG.SERVICE_ID__OVER_EXPEND
                                    && o.IS_EXPEND == MOS.UTILITY.Constant.IS_TRUE).Sum(o => o.PRICE * o.AMOUNT);

                            //neu tong so tien hao phi vuot qua dinh muc hao phi cua dich vu chinh
                            decimal exceedPrice = totalExpendChildren - parentService.MAX_EXPEND.Value;

                            if (exceedPrice > 0)
                            {
                                //neu co dich vu nay, thi gan lai so tien
                                if (overExpendService != null)
                                {
                                    overExpendService.PRICE = exceedPrice;
                                }
                                else
                                {
                                    HIS_SERVICE_REQ serviceReq = new HisServiceReqGet().GetById(parent.SERVICE_REQ_ID.Value);
                                    HIS_SERE_SERV overExpendSereServ = new HIS_SERE_SERV();
                                    overExpendSereServ.PARENT_ID = parent.ID;
                                    overExpendSereServ.SERVICE_REQ_ID = parent.SERVICE_REQ_ID;
                                    overExpendSereServ.PRICE = exceedPrice;
                                    overExpendSereServ.ORIGINAL_PRICE = exceedPrice;
                                    overExpendSereServ.SERVICE_ID = HisServiceCFG.SERVICE_ID__OVER_EXPEND;
                                    overExpendSereServ.PATIENT_TYPE_ID = HisPatientTypeCFG.PATIENT_TYPE_ID__OVER_EXPEND;
                                    overExpendSereServ.AMOUNT = 1;
                                    this.hisSereServCreate.Create(overExpendSereServ, serviceReq, false);
                                }
                            }
                            else if (overExpendService != null) //neu so tien chua vuot, nhung ton tai dich vu luu so tien hao phi
                            {
                                overExpendService.IS_DELETE = 1;//de xoa dich vu nay
                            }
                        }
                        //neu dich vu cha bi xoa hoac ko co cau hinh gioi han hao phi nua thi xoa
                        else if (overExpendService != null)
                        {
                            overExpendService.IS_DELETE = 1;//de xoa dich vu nay
                        }
                    }
                }
            }
        }

        private void UpdateAddprice(List<HIS_SERE_SERV> listRaw)
        {
            if (IsNotNullOrEmpty(listRaw))
            {
                if (!new HisSereServSetAddPrice(param).UpdateAddprice(listRaw))
                {
                    throw new Exception("Ket thuc nghiep vu, rollback du lieu");
                }
            }
        }

        /// <summary>
        /// Set gia va cac thong tin lien quan cho sere_serv
        /// </summary>
        /// <param name="hisSereServs"></param>
        /// <param name="treatmentId"></param>
        private void AddInfo(List<HIS_SERE_SERV> hisSereServs)
        {
            if (IsNotNullOrEmpty(hisSereServs))
            {
                foreach (HIS_SERE_SERV hisSereServ in hisSereServs)
                {
                    if (!addInfo.AddInfo(hisSereServ))
                    {
                        throw new Exception("Ket thuc nghiep vu, rollback du lieu");
                    }
                }
            }
        }

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdates))
            {
                if (!this.hisSereServUpdate.Run(this.beforeUpdates))
                {
                    LogSystem.Error("Rollback sere_serv that bai");
                }
            }
        }

        private HIS_PATIENT_TYPE_ALTER GetLastPta()
        {
            if (IsNotNullOrEmpty(this.patientTypeAlters))
            {
                return this.patientTypeAlters
                    .OrderByDescending(o => o.LOG_TIME)
                    .ThenByDescending(o => o.ID)
                    .FirstOrDefault();
            }
            return null;
        }
    }
}
