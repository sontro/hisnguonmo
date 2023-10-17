using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisVaccinationExam;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccination.Assign.Create
{
    /// <summary>
    /// Chi dinh tiem vaccin
    /// </summary>
    class HisVaccinationAssignCreate : BusinessBase
    {
        private List<HIS_VACCINATION> recentVaccinations;
        private List<HIS_EXP_MEST> recentExpMests;
        private List<HIS_EXP_MEST_MEDICINE> recentExpMestMedicines;

        private HisExpMestProcessor hisExpMestProcessor;
        private MedicineProcessor medicineProcessor;
        private VaccinationProcessor vaccinationProcessor;
        
        internal HisVaccinationAssignCreate()
            : base()
        {
            this.Init();
        }

        internal HisVaccinationAssignCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestProcessor = new HisExpMestProcessor(param);
            this.vaccinationProcessor = new VaccinationProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
        }

        internal bool Run(HisVaccinationAssignSDO data, ref VaccinationResultSDO resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationExamCheck vaccinationExamChecker = new HisVaccinationExamCheck(param);
                HisVaccinationAssignCheck checker = new HisVaccinationAssignCheck(param);
                HIS_VACCINATION_EXAM vaccinationExam = null;
                WorkPlaceSDO workPlace = null;
                valid = valid && checker.IsValidData(data);
                valid = valid && vaccinationExamChecker.VerifyId(data.VaccinationExamId, ref vaccinationExam);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && this.IsWorkingAtRoom(vaccinationExam.EXECUTE_ROOM_ID, data.WorkingRoomId);
                valid = valid && checker.IsValidServicePaty(vaccinationExam, data, workPlace);
                valid = valid && checker.IsAllowMediStock(data, workPlace);

                if (valid)
                {
                    List<string> sqls = new List<string>();

                    if (!this.vaccinationProcessor.Run(data, workPlace, vaccinationExam, ref this.recentVaccinations))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.hisExpMestProcessor.Run(this.recentVaccinations, ref this.recentExpMests))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (!this.medicineProcessor.Run(data.VaccinationMeties, this.recentVaccinations, this.recentExpMests, ref this.recentExpMestMedicines, ref sqls))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //Set TDL_TOTAL_PRICE
                    this.ProcessTdlTotalPrice(this.recentExpMests, this.recentExpMestMedicines, ref sqls);

                    //Can execute sql o cuoi de tranh rollback du lieu
                    if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Rollback");
                    }

                    this.PassResult(ref resultData);

                    HisVaccinationLog.Run(resultData.Vaccinations, resultData.ExpMests, resultData.Medicines, LibraryEventLog.EventLog.Enum.HisVaccination_ChiDinhTiem);
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

        private void PassResult(ref VaccinationResultSDO resultData)
        {
            resultData = new VaccinationResultSDO();
            resultData.ExpMests = this.recentExpMests;
            List<long> expMestMedicineIds = IsNotNullOrEmpty(this.recentExpMestMedicines) ? this.recentExpMestMedicines.Select(o => o.ID).ToList() : null;
            List<long> vaccinationIds = IsNotNullOrEmpty(this.recentVaccinations) ? this.recentVaccinations.Select(o => o.ID).ToList() : null;
            resultData.Medicines = new HisExpMestMedicineGet().GetViewByIds(expMestMedicineIds);
            resultData.Vaccinations = new HisVaccinationGet().GetViewByIds(vaccinationIds);
        }

        private void ProcessTdlTotalPrice(List<HIS_EXP_MEST> expMests, List<HIS_EXP_MEST_MEDICINE> expMestMedicines, ref List<string> sqls)
        {
            try
            {
                if (expMests == null) return;

                foreach (HIS_EXP_MEST exp in expMests)
                {
                    decimal? totalPrice = null;
                    List<HIS_EXP_MEST_MEDICINE> lstMedicine = expMestMedicines != null ? expMestMedicines.Where(o => o.EXP_MEST_ID == exp.ID).ToList() : null;
                    
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
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private void RollbackData()
        {
            this.medicineProcessor.Rollback();
            this.hisExpMestProcessor.Rollback();
            this.vaccinationProcessor.Rollback();
        }
    }
}
