using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Common.Delete
{
    class SereServProcessor : BusinessBase
    {
        private HisSereServUpdate hisSereServUpdate;
        private HisSereServUpdateHein hisSereServUpdateHein;

        internal SereServProcessor(CommonParam param)
            : base(param)
        {
            this.hisSereServUpdate = new HisSereServUpdate(param);
        }

        internal bool Run(HIS_IMP_MEST impMest, HIS_EXP_MEST expMest, List<HIS_SERE_SERV> hisSereServs, Dictionary<HIS_EXP_MEST_MEDICINE, decimal> dicMedicineThAmount, Dictionary<HIS_EXP_MEST_MATERIAL, decimal> dicMaterialThAmount, List<HIS_IMP_MEST_BLOOD> impMestBloods)
        {
            bool result = false;
            try
            {
                if (HisImpMestContanst.TYPE_MOBA_PRES_IDS.Contains(impMest.IMP_MEST_TYPE_ID))
                {
                    List<HIS_SERE_SERV> updateList = new List<HIS_SERE_SERV>();
                    List<HIS_SERE_SERV> Befores = new List<HIS_SERE_SERV>();
                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                    if (dicMedicineThAmount != null && dicMedicineThAmount.Count > 0)
                    {
                        foreach (var dic in dicMedicineThAmount)
                        {
                            HIS_SERE_SERV ss = null;
                            if (hisSereServs.Exists(e => e.MEDICINE_ID.HasValue && e.EXP_MEST_MEDICINE_ID.HasValue))
                            {
                                ss = hisSereServs.Where(o => o.EXP_MEST_MEDICINE_ID == dic.Key.ID).FirstOrDefault();
                            }
                            else
                            {
                                ss = hisSereServs != null ? hisSereServs
                                .FirstOrDefault(o => o.MEDICINE_ID == dic.Key.MEDICINE_ID
                                    && o.PATIENT_TYPE_ID == dic.Key.PATIENT_TYPE_ID
                                    && o.IS_EXPEND == dic.Key.IS_EXPEND
                                    && o.IS_OUT_PARENT_FEE == dic.Key.IS_OUT_PARENT_FEE) : null;
                            }
                            if (ss == null)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                                throw new Exception("Khong lay duoc HIS_SERE_SERV tuong ung voi EXP_MEST_MEDICINE" + LogUtil.TraceData("ExpMestMedicine", dic.Key));
                            }
                            Befores.Add(Mapper.Map<HIS_SERE_SERV>(ss));
                            ss.AMOUNT += dic.Value;
                            updateList.Add(ss);
                        }
                    }

                    if (dicMaterialThAmount != null && dicMaterialThAmount.Count > 0)
                    {
                        foreach (var dic in dicMaterialThAmount)
                        {
                            HIS_SERE_SERV ss = null;
                            if (hisSereServs.Exists(e => e.MATERIAL_ID.HasValue && e.EXP_MEST_MATERIAL_ID.HasValue))
                            {
                                ss = hisSereServs.Where(o => o.EXP_MEST_MATERIAL_ID == dic.Key.ID).FirstOrDefault();
                            }
                            else
                            {
                                ss = hisSereServs != null ? hisSereServs
                                .FirstOrDefault(o => o.MATERIAL_ID == dic.Key.MATERIAL_ID
                                    && o.PATIENT_TYPE_ID == dic.Key.PATIENT_TYPE_ID
                                    && o.IS_EXPEND == dic.Key.IS_EXPEND
                                    && o.IS_OUT_PARENT_FEE == dic.Key.IS_OUT_PARENT_FEE
                                    && o.STENT_ORDER == dic.Key.STENT_ORDER
                                    && o.EQUIPMENT_SET_ID == dic.Key.EQUIPMENT_SET_ID
                                    && o.EQUIPMENT_SET_ORDER == dic.Key.EQUIPMENT_SET_ORDER
                                    ) : null;
                            }
                            if (ss == null)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                                throw new Exception("Khong lay duoc HIS_SERE_SERV tuong ung voi EXP_MEST_MEDICINE" + LogUtil.TraceData("ExpMestMedicine", dic.Key));
                            }
                            Befores.Add(Mapper.Map<HIS_SERE_SERV>(ss));
                            ss.AMOUNT += dic.Value;
                            updateList.Add(ss);
                        }
                    }
                    if (IsNotNullOrEmpty(impMestBloods))
                    {
                        foreach (var impBlood in impMestBloods)
                        {
                            HIS_SERE_SERV ss = hisSereServs != null ? hisSereServs.FirstOrDefault(o => o.BLOOD_ID == impBlood.BLOOD_ID) : null;
                            if (ss == null)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                                throw new Exception("Khong co SereServ tuong ung voi ImpMestBlood thu hoi Blood_id: " + impBlood.BLOOD_ID);
                            }
                            if (ss.AMOUNT > 0)
                            {
                                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                                throw new Exception("SereServ tuong ung voi ImpMestBlood thu hoi co so luong > 0" + LogUtil.TraceData("SereServ", ss));
                            }
                            Befores.Add(Mapper.Map<HIS_SERE_SERV>(ss));
                            ss.AMOUNT = 1;
                            updateList.Add(ss);
                        }
                    }

                    if (updateList.Count > 0)
                    {
                        HIS_TREATMENT treatment = null;
                        HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                        bool valid = true;
                        valid = valid && treatmentChecker.IsUnLock(expMest.TDL_TREATMENT_ID.Value, ref treatment);
                        valid = valid && treatmentChecker.IsUnTemporaryLock(treatment);
                        valid = valid && treatmentChecker.IsUnLockHein(treatment);
                        if (!valid)
                        {
                            throw new Exception("Ket thuc nghiep vu. Rollback du lieu");
                        }

                        List<HIS_SERE_SERV_BILL> hasBills = new HisSereServBillGet().GetNoCancelBySereServIds(updateList.Select(s => s.ID).ToList());
                        if (IsNotNullOrEmpty(hasBills))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisSereServ_DichVuDaThanhToanKhongChoPhepHuyThuHoi);
                            throw new Exception("dich vu da duoc thanh toan, khong cho phep huy thu hoi" + LogUtil.TraceData("SereServs", updateList));
                        }

                        List<HIS_SERE_SERV_DEPOSIT> hasDeposits = new HisSereServDepositGet().GetNoCancelBySereServIds(updateList.Select(s => s.ID).ToList());
                        List<long> depositIds = hasDeposits != null ? hasDeposits.Select(s => s.ID).ToList() : null;
                        List<HIS_SESE_DEPO_REPAY> hasRepays = null;

                        if (IsNotNullOrEmpty(depositIds))
                        {
                            hasRepays = new HisSeseDepoRepayGet().GetNoCancelBySereServDepositIds(depositIds);
                        }

                        hasDeposits = hasDeposits != null ? hasDeposits.Where(o => hasRepays == null || !hasRepays.Exists(e => e.SERE_SERV_DEPOSIT_ID == o.ID)).ToList() : null;
                        if (IsNotNullOrEmpty(hasDeposits))
                        {
                            MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisSereServ_DichVuDaTamUngkhongChoPhepHuyThuHoi);
                            throw new Exception("dich vu da duoc tam ung, khong cho phep Huy thu hoi" + LogUtil.TraceData("SereServs", updateList));
                        }

                        if (!this.hisSereServUpdate.UpdateList(updateList, Befores, false))
                        {
                            throw new Exception("Update HIS_SERE_SERV that bai");
                        }

                        this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, false);
                        if (!hisSereServUpdateHein.UpdateDb())
                        {
                            throw new Exception("hisSereServUpdateHein that bai");
                        }
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                this.hisSereServUpdate.RollbackData();
                if (this.hisSereServUpdateHein != null) this.hisSereServUpdateHein.RollbackData();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
