using AutoMapper;
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

namespace MOS.MANAGER.HisImpMest.Moba.Blood
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

        internal bool Run(List<HIS_IMP_MEST_BLOOD> hisImpMestBloods, HIS_EXP_MEST expMest)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(hisImpMestBloods))
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

                    List<HIS_SERE_SERV> hisSereServs = new HisSereServGet().GetByServiceReqId(expMest.SERVICE_REQ_ID.Value);

                    if (!IsNotNullOrEmpty(hisSereServs))
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                        throw new Exception("Khong lay duoc HIS_SERE_SERV theo ServiceReqId: " + expMest.SERVICE_REQ_ID);
                    }

                    List<HIS_SERE_SERV> listUpdate = new List<HIS_SERE_SERV>();

                    foreach (var impMestBlood in hisImpMestBloods)
                    {
                        HIS_SERE_SERV ss = hisSereServs.FirstOrDefault(o => o.BLOOD_ID.HasValue && o.BLOOD_ID.Value == impMestBlood.BLOOD_ID);
                        if (ss == null)
                        {
                            BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                            throw new Exception("Khong lay duoc HIS_SERE_SERV cua BloodId: " + impMestBlood.BLOOD_ID);
                        }
                        listUpdate.Add(ss);
                    }

                    Mapper.CreateMap<HIS_SERE_SERV, HIS_SERE_SERV>();
                    List<HIS_SERE_SERV> beforeUpdates = Mapper.Map<List<HIS_SERE_SERV>>(listUpdate);
                    //kiem tra da co thanh toan hay chua
                    List<HIS_SERE_SERV_BILL> hasBills = new HisSereServBillGet().GetNoCancelBySereServIds(listUpdate.Select(s => s.ID).ToList());
                    if (IsNotNullOrEmpty(hasBills))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisSereServ_DichVuDaThanhToanKhongChoPhepHuy);
                        throw new Exception("Mau da duoc thanh toan, khong cho phep thu hoi");
                    }

                    List<HIS_SERE_SERV_DEPOSIT> hasDeposits = new HisSereServDepositGet().GetNoCancelBySereServIds(listUpdate.Select(s => s.ID).ToList());
                    List<long> ssDepoIds = hasDeposits != null ? hasDeposits.Select(s => s.ID).ToList() : null;
                    List<HIS_SESE_DEPO_REPAY> hasRepays = null;
                    if (IsNotNullOrEmpty(ssDepoIds))
                    {
                        hasRepays = new HisSeseDepoRepayGet().GetNoCancelBySereServDepositIds(ssDepoIds);
                    }
                    hasDeposits = hasDeposits != null ? hasDeposits.Where(o => hasRepays == null || !hasRepays.Exists(e => e.SERE_SERV_DEPOSIT_ID == o.ID)).ToList() : null;

                    if (IsNotNullOrEmpty(hasDeposits))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisSereServ_DichVuDaTamUngkhongChoPhepThuHoi);
                        throw new Exception("Mau da duoc tam ung, khong cho phep thu hoi");
                    }
                    listUpdate.ForEach(o => o.AMOUNT = 0);
                    if (!this.hisSereServUpdate.UpdateList(listUpdate, beforeUpdates, false))
                    {
                        throw new Exception("Xoa dich vu that bai, Rollback du lieu");
                    }

                    //Cap nhat ti le BHYT cho sere_serv
                    this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, false);
                    if (!hisSereServUpdateHein.UpdateDb())
                    {
                        throw new Exception("Du lieu se bi rollback. Ket thuc nghiep vu");
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
                if (this.hisSereServUpdateHein!=null)
                {
                    this.hisSereServUpdateHein.RollbackData();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
