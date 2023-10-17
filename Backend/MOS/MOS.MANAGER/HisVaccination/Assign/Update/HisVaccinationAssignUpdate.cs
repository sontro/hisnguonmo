using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMediStock;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisServiceReq.AssignService;
using MOS.MANAGER.HisServiceReq.Common;
using MOS.MANAGER.HisServiceReqMaty;
using MOS.MANAGER.HisServiceReqMety;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisVaccinationExam;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccination.Assign.Update
{
    /// <summary>
    /// Xu ly cap nhat chi dinh tiem chung
    /// </summary>
    class HisVaccinationAssignUpdate : BusinessBase
    {
        private HIS_VACCINATION recentVaccination;
        private HIS_EXP_MEST recentExpMest;
        private List<HIS_EXP_MEST_MEDICINE> recentExpMestMedicines;

        private MedicineProcessor medicineProcessor;
        private VaccinationProcessor vaccinationProcessor;

        internal HisVaccinationAssignUpdate()
            : base()
        {
            this.Init();
        }

        internal HisVaccinationAssignUpdate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.vaccinationProcessor = new VaccinationProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
        }

        internal bool Run(HisVaccinationAssignSDO data, ref VaccinationResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;

                HisVaccinationCheck vaccinationChecker = new HisVaccinationCheck(param);
                HisVaccinationExamCheck vaccinationExamChecker = new HisVaccinationExamCheck(param);
                HisVaccinationAssignCheck checker = new HisVaccinationAssignCheck(param);
                HisExpMestCheck expMestChecker = new HisExpMestCheck(param);

                HIS_VACCINATION_EXAM vaccinationExam = null;
                HIS_VACCINATION vaccination = null;
                WorkPlaceSDO workPlace = null;
                valid = valid && checker.IsValidData(data);
                valid = valid && vaccinationChecker.VerifyId(data.VaccinationId.Value, ref vaccination);
                valid = valid && vaccinationChecker.IsNotFinish(vaccination);
                valid = valid && vaccinationChecker.IsNotFinishExpMest(vaccination, ref this.recentExpMest);
                valid = valid && vaccinationExamChecker.VerifyId(vaccination.VACCINATION_EXAM_ID, ref vaccinationExam);
                valid = valid && vaccinationChecker.HasNotBill(vaccination);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && this.IsWorkingAtRoom(vaccinationExam.EXECUTE_ROOM_ID, data.WorkingRoomId);
                valid = valid && checker.IsValidServicePaty(vaccinationExam, data, workPlace);
                valid = valid && checker.IsAllowMediStock(data, workPlace);

                if (valid)
                {
                    List<string> sqls = new List<string>();
                    List<HIS_EXP_MEST_MEDICINE> insertMedicines = null;
                    List<HIS_EXP_MEST_MEDICINE> deleteMedicines = null;

                    if (!this.vaccinationProcessor.Run(data, vaccination, ref this.recentVaccination))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.medicineProcessor.Run(data.VaccinationMeties, this.recentExpMest, ref insertMedicines, ref deleteMedicines, ref this.recentExpMestMedicines, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Set TDL_TOTAL_PRICE
                    this.ProcessTdlTotalPrice(this.recentExpMest, this.recentExpMestMedicines, ref sqls);

                    //Can execute sql o cuoi de tranh rollback du lieu
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback");
                    }

                    this.PassResult(ref resultData);

                    HisVaccinationLog.Run(resultData.Vaccinations, resultData.ExpMests, resultData.Medicines, LibraryEventLog.EventLog.Enum.HisVaccination_SuaChiDinhTiem);
                }
                result = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                this.RollbackData();
                param.HasException = true;
                resultData = null;
                result = false;
            }
            return result;
        }

        private void ProcessTdlTotalPrice(HIS_EXP_MEST exp, List<HIS_EXP_MEST_MEDICINE> lstMedicine, ref List<string> sqls)
        {
            try
            {
                if (exp == null) return;

                decimal? totalPrice = null;
                if (IsNotNullOrEmpty(lstMedicine))
                {
                    decimal mediPrice = 0;
                    foreach (HIS_EXP_MEST_MEDICINE medi in lstMedicine)
                    {
                        if (!medi.PRICE.HasValue)
                        {
                            continue;
                        }
                        mediPrice += (medi.AMOUNT * medi.PRICE.Value * (1 + (medi.VAT_RATIO ?? 0)));
                    }
                    if (mediPrice > 0)
                    {
                        totalPrice = (totalPrice ?? 0) + mediPrice;
                    }
                }
                if (totalPrice.HasValue)
                {
                    string updateSql = string.Format("UPDATE HIS_EXP_MEST SET TDL_TOTAL_PRICE = {0} WHERE ID = {1}", totalPrice.Value.ToString("G27", CultureInfo.InvariantCulture), exp.ID);
                    sqls.Add(updateSql);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void PassResult(ref VaccinationResultSDO resultData)
        {
            resultData = new VaccinationResultSDO();
            resultData.ExpMests = new List<HIS_EXP_MEST>() { this.recentExpMest };
            List<long> expMestMedicineIds = IsNotNullOrEmpty(this.recentExpMestMedicines) ? this.recentExpMestMedicines.Select(o => o.ID).ToList() : null;
            List<long> vaccinationIds = new List<long>() { this.recentVaccination.ID };

            resultData.Medicines = new HisExpMestMedicineGet().GetViewByIds(expMestMedicineIds);
            resultData.Vaccinations = new HisVaccinationGet().GetViewByIds(vaccinationIds);
        }

        private void RollbackData()
        {
            this.medicineProcessor.Rollback();
            this.vaccinationProcessor.Rollback();
        }
    }
}
