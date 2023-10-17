using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisImpMest.Moba.Pres
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

        internal bool Run(HIS_TREATMENT treatment, List<HIS_SERE_SERV> allSereServs, List<HIS_SERE_SERV> lisSereServUpdates, List<HIS_SERE_SERV> sereServBeforeUpdates)
        {
            bool result = false;
            try
            {
                if (IsNotNullOrEmpty(lisSereServUpdates))
                {
                    List<long> sereServUpdateIds = lisSereServUpdates.Select(s => s.ID).ToList();

                    //Kiem tra dich vu da duoc thanh toan hay tam ung chua
                    List<HIS_SERE_SERV_BILL> hasBills = new HisSereServBillGet().GetNoCancelBySereServIds(sereServUpdateIds);

                    if (IsNotNullOrEmpty(hasBills))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisSereServ_DichVuDaThanhToanKhongChoPhepThuHoi);
                        return false;
                    }

                    List<HIS_SERE_SERV_DEPOSIT> hasDeposits = new HisSereServDepositGet().GetNoCancelBySereServIds(sereServUpdateIds);
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
                        return false;
                    }

                    //Trong truong hop danh sach thu hoi co stent thi thuc hien gan lai gia cho cac stent con lai
                    if (lisSereServUpdates != null && lisSereServUpdates.Exists(t => HisMaterialTypeCFG.IsStentByServiceId(t.SERVICE_ID)))
                    {
                        List<long> materialIds = allSereServs != null ? allSereServs
                            .Where(o => o.MATERIAL_ID.HasValue && HisMaterialTypeCFG.IsStentByServiceId(o.SERVICE_ID)).Select(o => o.MATERIAL_ID.Value).ToList() : null;

                        HisSereServSetPrice priceAdder = new HisSereServSetPrice(param, treatment, null, materialIds);

                        foreach (HIS_SERE_SERV s in allSereServs)
                        {
                            if (HisMaterialTypeCFG.IsStentByServiceId(s.SERVICE_ID))
                            {
                                priceAdder.AddPrice(s, s.TDL_INTRUCTION_TIME, s.TDL_EXECUTE_BRANCH_ID, s.TDL_REQUEST_ROOM_ID, s.TDL_REQUEST_DEPARTMENT_ID, s.TDL_EXECUTE_ROOM_ID);
                            }
                        }
                    }

                    //Cap nhat ti le BHYT cho sere_serv
                    this.hisSereServUpdateHein = new HisSereServUpdateHein(param, treatment, false);

                    if (!this.hisSereServUpdateHein.Update(allSereServs))
                    {
                        throw new Exception("hisSereServUpdateHein faild");
                    }

                    List<HIS_SERE_SERV> changes = null;
                    List<HIS_SERE_SERV> oldOfChanges = null;

                    HisSereServUtil.GetChangeRecord(sereServBeforeUpdates, allSereServs, ref changes, ref oldOfChanges);

                    if (IsNotNullOrEmpty(changes))
                    {
                        if (!this.hisSereServUpdate.UpdateList(changes, oldOfChanges, false))
                        {
                            throw new Exception("Update HIS_SERE_SERV that bai");
                        }
                    }
                    else
                    {
                        result = true;
                    }

                }
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                this.hisSereServUpdate.RollbackData();
                if (this.hisSereServUpdateHein != null)
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
