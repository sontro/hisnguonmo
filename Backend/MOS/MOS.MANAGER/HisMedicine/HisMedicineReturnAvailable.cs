using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisMediStock;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MOS.MANAGER.HisMedicine
{
    class HisMedicineReturnAvailable : BusinessBase
    {
        private List<HIS_MEDICINE_BEAN> beforeUpdateHisMedicineBeans = new List<HIS_MEDICINE_BEAN>();

        internal HisMedicineReturnAvailable()
            : base()
        {

        }

        internal HisMedicineReturnAvailable(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool ReturnAvailable(SDO.HisMedicineReturnAvailableSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_MEDICINE> raws = new List<HIS_MEDICINE>();
                HIS_MEDI_STOCK mediStock = null;
                List<HIS_MEDICINE_BEAN> ListMedicineBeanRaw = null;
                HisMedicineCheck checker = new HisMedicineCheck(param);
                HisMediStockCheck stockChecker = new HisMediStockCheck(param);
                valid = valid && stockChecker.VerifyId(data.MediStockId, ref mediStock);
                if (data.MedicineId.HasValue)
                {
                    HIS_MEDICINE raw = null;
                    valid = valid && checker.VerifyId(data.MedicineId.Value, ref raw);
                    valid = valid && checker.IsUnLock(raw);
                    if (valid)
                    {
                        raws.Add(raw);
                    }
                }
                else if (data.MedicineTypeId.HasValue)
                {
                    valid = valid && checkerMedicineByType(param, mediStock.ID, data.MedicineTypeId.Value, ref raws);
                    valid = valid && checker.IsUnLock(raws);
                }
                else
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    Logging("MedicineId and MedicineTypeId is null.", LogType.Error);
                    valid = false;
                }

                valid = valid && CheckBean(param, mediStock.ID, raws.Select(s => s.ID).ToList(), ref ListMedicineBeanRaw);
                if (valid)
                {
                    AutoMapper.Mapper.CreateMap<HIS_MEDICINE_BEAN, HIS_MEDICINE_BEAN>();
                    List<HIS_MEDICINE_BEAN> MedicineBean = AutoMapper.Mapper.Map<List<HIS_MEDICINE_BEAN>>(ListMedicineBeanRaw);

                    List<HIS_MEDICINE_BEAN> updateActiveBean = new List<HIS_MEDICINE_BEAN>();
                    List<HIS_MEDICINE_BEAN> updateStockBean = new List<HIS_MEDICINE_BEAN>();
                    foreach (var item in raws)
                    {
                        decimal inStockAmount = GetInStockAmount(mediStock.ID, item.ID);
                        decimal totalImpAmount = GetImpAmount(mediStock.ID, item.ID);
                        decimal totalExpAmount = GetExpAmount(mediStock.ID, item.ID);

                        List<HIS_MEDICINE_BEAN> updateBean = MedicineBean.Where(s => s.MEDICINE_ID == item.ID).ToList();
                        decimal diffAmount = inStockAmount - (totalImpAmount - totalExpAmount);

                        if (diffAmount <= 0)
                        {
                            updateActiveBean.AddRange(updateBean);
                        }
                        else
                        {
                            for (int i = 0; i < MedicineBean.Count; i++)
                            {
                                if (diffAmount <= 0)
                                {
                                    updateActiveBean.Add(MedicineBean[i]);
                                }
                                else
                                {
                                    //nếu số lượng trong bean lớn hơn số lượnng chênh lệch thì giảmm đi số lượng chênh lệch
                                    //nếu số lượng trong bean nhỏ hơn hoặc bằng số lượng chênh lệch thì cho ra khỏi kho
                                    var amount = MedicineBean[i].AMOUNT;
                                    if (MedicineBean[i].AMOUNT > diffAmount)
                                    {
                                        MedicineBean[i].AMOUNT -= diffAmount;
                                        updateActiveBean.Add(MedicineBean[i]);
                                    }
                                    else
                                    {
                                        updateStockBean.Add(MedicineBean[i]);
                                    }

                                    diffAmount -= amount;
                                }
                            }
                        }
                    }

                    if (!UpdateMedicineBean(updateActiveBean, updateStockBean))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMedicine_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMedicine that bai." + LogUtil.TraceData("data", data));
                    }

                    beforeUpdateHisMedicineBeans.AddRange(ListMedicineBeanRaw);
                    result = true;
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

        private bool UpdateMedicineBean(List<HIS_MEDICINE_BEAN> updateActiveBean, List<HIS_MEDICINE_BEAN> updateStockBean)
        {
            bool result = true;
            try
            {
                List<string> listSql = new List<string>();
                if (IsNotNullOrEmpty(updateActiveBean))
                {
                    string sql = "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE=1, AMOUNT={0} WHERE ID={1}";
                    foreach (var item in updateActiveBean)
                    {
                        listSql.Add(string.Format(sql, item.AMOUNT.ToString("G27", CultureInfo.InvariantCulture), item.ID));
                    }
                }

                if (IsNotNullOrEmpty(updateStockBean))
                {
                    string sql = "UPDATE HIS_MEDICINE_BEAN SET MEDI_STOCK_ID=null WHERE ID={0}";
                    Inventec.Common.Logging.LogSystem.Info("Update bean out stock: "+sql);
                    foreach (var item in updateStockBean)
                    {
                        listSql.Add(string.Format(sql, item.ID));
                    }
                }

                if (!DAOWorker.SqlDAO.Execute(listSql))
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private decimal GetInStockAmount(long medistockId, long medicineId)
        {
            decimal result = 0;
            try
            {
                string sql = "SELECT SUM(AMOUNT) FROM HIS_MEDICINE_BEAN WHERE MEDICINE_ID = {0} AND MEDI_STOCK_ID = {1}";
                sql = string.Format(sql, medicineId, medistockId);
                var amount = new MOS.DAO.Sql.SqlDAO().GetSqlSingle<Nullable<decimal>>(sql);
                if (amount.HasValue)
                {
                    result = amount.Value;
                }
            }
            catch (Exception ex)
            {
                result = 0;
                Inventec.Common.Logging.LogSystem.Info(string.Format("medistockId {0} medicineId {1}", medistockId, medicineId));
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private decimal GetExpAmount(long medistockId, long medicineId)
        {
            decimal result = 0;
            try
            {
                string sql = "SELECT SUM(AMOUNT) FROM HIS_EXP_MEST_MEDICINE WHERE MEDICINE_ID = {0} AND TDL_MEDI_STOCK_ID = {1} AND IS_EXPORT = 1";
                sql = string.Format(sql, medicineId, medistockId);
                var amount = new MOS.DAO.Sql.SqlDAO().GetSqlSingle<Nullable<decimal>>(sql);
                if (amount.HasValue)
                {
                    result = amount.Value;
                }
            }
            catch (Exception ex)
            {
                result = 0;
                Inventec.Common.Logging.LogSystem.Info(string.Format("medistockId {0} medicineId {1}", medistockId, medicineId));
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private decimal GetImpAmount(long medistockId, long medicineId)
        {
            decimal result = 0;
            try
            {
                string sql = "SELECT SUM(AMOUNT) FROM HIS_IMP_MEST_MEDICINE MEMA WHERE MEDICINE_ID = {0} AND EXISTS (SELECT 1 FROM HIS_IMP_MEST WHERE MEMA.IMP_MEST_ID = ID AND MEDI_STOCK_ID = {1} AND IMP_TIME IS NOT NULL AND IMP_MEST_STT_ID = 5)";
                sql = string.Format(sql, medicineId, medistockId);
                var amount = new MOS.DAO.Sql.SqlDAO().GetSqlSingle<Nullable<decimal>>(sql);
                if (amount.HasValue)
                {
                    result = amount.Value;
                }
            }
            catch (Exception ex)
            {
                result = 0;
                Inventec.Common.Logging.LogSystem.Info(string.Format("medistockId {0} medicineId {1}", medistockId, medicineId));
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private bool checkerMedicineByType(CommonParam param, long mediStockId, long medicineTypeId, ref List<HIS_MEDICINE> raws)
        {
            bool valid = true;
            try
            {
                string sql = "SELECT * FROM HIS_MEDICINE M WHERE EXISTS (SELECT 1 FROM HIS_MEDICINE_BEAN WHERE M.ID=MEDICINE_ID AND MEDI_STOCK_ID={0} AND TDL_MEDICINE_TYPE_ID={1})";
                sql = string.Format(sql, mediStockId, medicineTypeId);
                raws = new MOS.DAO.Sql.SqlDAO().GetSql<HIS_MEDICINE>(sql);
                if (!IsNotNullOrEmpty(raws))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    Logging("ERROR mediStockId: " + mediStockId + "| medicineTypeId: " + medicineTypeId, LogType.Error);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        private bool CheckBean(CommonParam param, long mediStockId, List<long> medicineIds, ref List<HIS_MEDICINE_BEAN> medicineBean)
        {
            bool valid = true;
            try
            {
                string sql = "SELECT * FROM HIS_MEDICINE_BEAN WHERE MEDI_STOCK_ID = {0} AND MEDICINE_ID IN ({1}) AND (IS_ACTIVE IS NULL OR IS_ACTIVE <> 1) AND SESSION_KEY IS NULL AND EXP_MEST_MEDICINE_ID IS NULL";
                sql = string.Format(sql, mediStockId, string.Join(",", medicineIds));
                medicineBean = new MOS.DAO.Sql.SqlDAO().GetSql<HIS_MEDICINE_BEAN>(sql);
                if (!IsNotNullOrEmpty(medicineBean))
                {
                    valid = false;
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMedicine_ThuocDangThuocPhieuXuatChuaThucXuat);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal void RollbackData()
        {
            if (IsNotNullOrEmpty(this.beforeUpdateHisMedicineBeans))
            {
                if (!new MOS.MANAGER.HisMedicineBean.Update.HisMedicineBeanUpdate(param).UpdateList(this.beforeUpdateHisMedicineBeans))
                {
                    LogSystem.Warn("Rollback du lieu HisMedicineBeans that bai, can kiem tra lai." + LogUtil.TraceData("HisMedicineBeans", this.beforeUpdateHisMedicineBeans));
                }
            }
        }
    }
}
