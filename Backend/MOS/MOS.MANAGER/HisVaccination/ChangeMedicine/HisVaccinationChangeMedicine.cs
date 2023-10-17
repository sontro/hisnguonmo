using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccination.ChangeMedicine
{
    class HisVaccinationChangeMedicine : BusinessBase
    {
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;
        private HisMedicineBeanSplit hisMedicineBeanSplit;

        internal HisVaccinationChangeMedicine()
            : base()
        {
            this.Init();
        }

        internal HisVaccinationChangeMedicine(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
            this.hisMedicineBeanSplit = new HisMedicineBeanSplit(param);
        }

        internal bool Run(HisVaccinationChangeMedicineSDO data, ref HIS_EXP_MEST_MEDICINE resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisVaccinationChangeMedicineCheck checker = new HisVaccinationChangeMedicineCheck(param);
                WorkPlaceSDO workPlace = null;
                HIS_EXP_MEST_MEDICINE expMestMedicine = null;
                HIS_VACCINATION vaccination = null;
                HIS_EXP_MEST expMest = null;
                HIS_MEDICINE oldMedicine = null;
                HIS_MEDICINE newMedicine = null;

                HisExpMestCheck expMestCheck = new HisExpMestCheck(param);
                HisExpMestMedicineCheck expMestMedicineCheck = new HisExpMestMedicineCheck(param);
                HisVaccinationCheck vaccinationCheck = new HisVaccinationCheck(param);

                valid = valid && checker.IsValidData(data);
                valid = valid && expMestMedicineCheck.VerifyId(data.ExpMestMedicineId, ref expMestMedicine);
                valid = valid && expMestCheck.VerifyId(expMestMedicine.EXP_MEST_ID.Value, ref expMest);
                valid = valid && (expMest.VACCINATION_ID.HasValue && vaccinationCheck.VerifyId(expMest.VACCINATION_ID.Value, ref vaccination));
                valid = valid && vaccinationCheck.IsNotFinish(vaccination);
                valid = valid && checker.IsNotApproval(expMestMedicine);
                valid = valid && checker.IsTheSamePrice(data.NewMedicineId, expMestMedicine, ref newMedicine, ref oldMedicine);
                valid = valid && this.HasWorkPlaceInfo(data.WorkingRoomId, ref workPlace);
                valid = valid && checker.IsWorkingAtRoom(vaccination.EXECUTE_ROOM_ID, vaccination.REQUEST_ROOM_ID, data.WorkingRoomId);

                if (valid)
                {
                    ExpMedicineSDO sdo = new ExpMedicineSDO();
                    sdo.Amount = expMestMedicine.AMOUNT;
                    sdo.MedicineId = data.NewMedicineId;

                    List<HIS_MEDICINE_BEAN> beanToUses = new List<HIS_MEDICINE_BEAN>();
                    //Thuc hien tach bean theo medicine_id moi
                    if (!this.hisMedicineBeanSplit.SplitByMedicine(new List<ExpMedicineSDO>() { sdo }, expMestMedicine.TDL_MEDI_STOCK_ID.Value, ref beanToUses))
                    {
                        throw new Exception("Tach bean that bai");
                    }

                    if (IsNotNullOrEmpty(beanToUses))
                    {
                        //mo khoa bean cu
                        string releaseOldBeanSql = new HisMedicineBeanUnlockByExpMest(param).GenSql(new List<long>() { expMestMedicine.ID });
                        //Cap nhat bean moi gan vao exp_mest_medicine
                        List<string> addNewBeanToExpMestMedicineSql = new HisMedicineBeanAddToExpMest(param).GenSql(expMestMedicine.ID, beanToUses.Select(o => o.ID).ToList());
                        //Cap nhat lai medicine_id cua exp_mest_medicine
                        string updateExpMestMedicineSql = string.Format("UPDATE HIS_EXP_MEST_MEDICINE SET MEDICINE_ID = {0} WHERE ID = {1}", data.NewMedicineId, expMestMedicine.ID);

                        List<string> sqls = new List<string>();
                        sqls.Add(releaseOldBeanSql);
                        sqls.AddRange(addNewBeanToExpMestMedicineSql);
                        sqls.Add(updateExpMestMedicineSql);

                        if (DAOWorker.SqlDAO.Execute(sqls))
                        {
                            result = true;
                            expMestMedicine.MEDICINE_ID = data.NewMedicineId;
                            resultData = expMestMedicine;

                            new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisVaccination_SuaThongTinLo, newMedicine.PACKAGE_NUMBER).ExpMestCode(expMest.EXP_MEST_CODE).VaccinationCode(vaccination.VACCINATION_CODE).Run();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                result = false;
            }
            return result;
        }
    }
}