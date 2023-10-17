using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisAssessmentMember;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisMedicalAssessment
{
    partial class HisMedicalAssessmentSave: BusinessBase
    {
        HisMedicalAssessmentUpdate medicalAssessmentUpdate { get; set; }
        HisMedicalAssessmentCreate medicalAssessmentCreate { get; set; }
        HisAssessmentMemberTruncate assessmentMemberTruncate { get; set; }
        HisAssessmentMemberCreate assessmentMemberCreate { get; set; }

        internal HisMedicalAssessmentSave(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.medicalAssessmentCreate = new HisMedicalAssessmentCreate(param);
            this.medicalAssessmentUpdate = new HisMedicalAssessmentUpdate(param);
            this.assessmentMemberTruncate = new HisAssessmentMemberTruncate(param);
            this.assessmentMemberCreate = new HisAssessmentMemberCreate(param);
        }

        public bool Run(HisMedicalAssessmentSDO data, ref HisMedicalAssessmentResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = valid && this.IsValidAssessmentMember(data);

                if (valid)
                {
                    if (data != null)
                    {
                        HisMedicalAssessmentFilterQuery filter = new HisMedicalAssessmentFilterQuery();
                        filter.TREATMENT_ID = data.TreatmentId;
                        List<HIS_MEDICAL_ASSESSMENT> MedicalAssessements = new HisMedicalAssessmentGet().Get(filter);
                        if (IsNotNullOrEmpty(MedicalAssessements))
                        {
                            HIS_MEDICAL_ASSESSMENT medical = MedicalAssessements.OrderByDescending(o => o.ID).FirstOrDefault();

                            if (data.HisMedicalAssessement != null)
                            {
                                data.HisMedicalAssessement.ID = medical.ID;
                                if (!this.medicalAssessmentUpdate.Update(data.HisMedicalAssessement))
                                {
                                    Inventec.Common.Logging.LogSystem.Error("Khong cap nhat duoc HIS_MEDICAL_ASSESSMENT. Ket thuc nghiep vu");
                                    return false;
                                }
                            }

                            HisAssessmentMemberFilterQuery filterMember = new HisAssessmentMemberFilterQuery();
                            filterMember.MEDICAL_ASSESSMENT_ID = medical.ID;

                            List<HIS_ASSESSMENT_MEMBER> members = new HisAssessmentMemberGet().Get(filterMember);
                            if (IsNotNullOrEmpty(members))
                            {
                                if (!this.assessmentMemberTruncate.TruncateList(members))
                                {
                                    Inventec.Common.Logging.LogSystem.Error("Xoa List<HIS_ASSESSMENT_MEMBER> that bai. Ket thuc nghiep vu");
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            if (data.HisMedicalAssessement != null)
                            {
                                if (!this.medicalAssessmentCreate.Create(data.HisMedicalAssessement))
                                {
                                    Inventec.Common.Logging.LogSystem.Error("Tao moi HIS_MEDICAL_ASSESSMENT. Ket thuc nghiep vu");
                                    return false;
                                }
                            }
                        }

                        if (IsNotNullOrEmpty(data.HisAssessmentMembers))
                        {
                            data.HisAssessmentMembers.ForEach(o => { o.MEDICAL_ASSESSMENT_ID = data.HisMedicalAssessement.ID; });
                            if (!this.assessmentMemberCreate.CreateList(data.HisAssessmentMembers))
                            {
                                Inventec.Common.Logging.LogSystem.Error("Tao moi List<HIS_ASSESSMENT_MEMBER> that bai. Ket thuc nghiep vu");
                                return false;
                            }
                        }

                        resultData = new HisMedicalAssessmentResultSDO();
                        if (IsNotNullOrEmpty(data.HisAssessmentMembers))
                        {
                            resultData.HisAssessmentMember = data.HisAssessmentMembers;
                        }
                        if (data.HisMedicalAssessement != null)
                        {
                            resultData.vHisMedicalAssessment = new HisMedicalAssessmentGet().GetViewById(data.HisMedicalAssessement.ID);
                        }

                        result = true;
                    }
                }
                
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                this.RollbackData();
                result = false;
            }

            return result;
        }

        internal bool IsValidAssessmentMember(HisMedicalAssessmentSDO data)
        {
            bool valid = true;
            try
            {
                if (data != null)
                {
                    long memberIsPre = data.HisAssessmentMembers.Where(o => o.IS_PRESIDENT == Constant.IS_TRUE).Count();
                    long memberIsSecre = data.HisAssessmentMembers.Where(o => o.IS_SECRETARY == Constant.IS_TRUE).Count();
                    long memberIs = data.HisAssessmentMembers.Where(o => (o.IS_PRESIDENT == Constant.IS_TRUE || o.IS_SECRETARY == Constant.IS_TRUE) && o.IS_GUEST != null).Count();
                    long memberIsGuest = data.HisAssessmentMembers.Where(o => o.IS_GUEST == Constant.IS_TRUE && o.IS_ABSENT != null && o.IS_DISAGREED != null && o.ON_BEHALF_TO_SIGN != null).Count();
                    long memberOnBehalfSign = data.HisAssessmentMembers.Where(o => o.ON_BEHALF_TO_SIGN == Constant.IS_TRUE).Count();
                    long memberOnAb = data.HisAssessmentMembers.Where(o => o.ON_BEHALF_TO_SIGN == Constant.IS_TRUE && o.IS_ABSENT == Constant.IS_TRUE).Count();

                    if (memberIs > 0)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicalAssessment_KhachMoiKhongDuocPhepKhaiBaoLaChuTriHoacThuKy);
                        Inventec.Common.Logging.LogSystem.Error("List<HIS_ASSESSMENT_MEMBER> co IS_SECRETARY = 1, hoặc IS_PRESIDENT = 1 va IS_GUEST khac NULL. Ket thuc nghiep vu");
                        return false;
                    }
                    if (memberIsGuest > 0)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicalAssessment_KhachMoiKhongDuocPhepKhaiBaoLaThanhVienThamGia);
                        Inventec.Common.Logging.LogSystem.Error("List<HIS_ASSESSMENT_MEMBER> co IS_GUEST = 1 va IS_ABSENT, IS_DISAGREED, ON_BEHALF_TO_SIGN khac NULL. Ket thuc nghiep vu");
                        return false;
                    }
                    if (memberOnAb > 0)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicalAssessment_NguoiDuocKhaiBaoVangMatKhongChoPhepKyThay);
                        Inventec.Common.Logging.LogSystem.Error("List<HIS_ASSESSMENT_MEMBER> ton tai ban ghi co ON_BEHALF_TO_SIGN = 1 và IS_ABSENT = 1. Ket thuc nghiep vu");
                        return false;
                    }

                    if (memberOnBehalfSign > 1)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicalAssessment_KhongDuocPhepKhaiBaoNhieuHon1NguoiKyThay);
                        Inventec.Common.Logging.LogSystem.Error("List<HIS_ASSESSMENT_MEMBER> co nhieu hon 1 ban ghi co ON_BEHALF_TO_SIGN = 1. Ket thuc nghiep vu");
                        return false;
                    }
                    List<string> fieldSet = new List<string>();
                    foreach (var item in data.HisAssessmentMembers)
                    {
                        if (fieldSet.Contains(item.LOGINNAME))
                        {
                            MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                            Inventec.Common.Logging.LogSystem.Error("List<HIS_ASSESSMENT_MEMBER> co nhieu hon 1 ban ghi co LOGINNAME trung nhau. Ket thuc nghiep vu");
                            return false;
                        }
                        else
                        {
                            fieldSet.Add(item.LOGINNAME);
                        }
                    }

                    if (memberIsPre != 1)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                        Inventec.Common.Logging.LogSystem.Error("List<HIS_ASSESSMENT_MEMBER> co nhieu hon hoac it hon 1 ban ghi co IS_PRESIDENT = 1. Ket thuc nghiep vu");
                        return false;
                    }
                    if (memberIsSecre != 1)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DuLieuDauVaoKhongHopLe);
                        Inventec.Common.Logging.LogSystem.Error("List<HIS_ASSESSMENT_MEMBER> co nhieu hon hoac it hon 1 ban ghi co IS_SECRETARY = 1. Ket thuc nghiep vu");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        private void RollbackData()
        {
            this.medicalAssessmentCreate.RollbackData();
            this.medicalAssessmentUpdate.RollbackData();
            this.assessmentMemberCreate.RollbackData();
        }
    }
}
