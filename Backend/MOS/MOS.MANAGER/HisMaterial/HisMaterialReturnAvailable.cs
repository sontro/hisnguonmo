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

namespace MOS.MANAGER.HisMaterial
{
    class HisMaterialReturnAvailable : BusinessBase
    {
        private List<HIS_MATERIAL_BEAN> beforeUpdateHisMaterialBeans = new List<HIS_MATERIAL_BEAN>();

        internal HisMaterialReturnAvailable()
            : base()
        {

        }

        internal HisMaterialReturnAvailable(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool ReturnAvailable(SDO.HisMaterialReturnAvailableSDO data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                List<HIS_MATERIAL> raws = new List<HIS_MATERIAL>();
                HIS_MEDI_STOCK mediStock = null;
                List<HIS_MATERIAL_BEAN> ListMaterialBeanRaw = null;
                HisMaterialCheck checker = new HisMaterialCheck(param);
                HisMediStockCheck stockChecker = new HisMediStockCheck(param);
                valid = valid && stockChecker.VerifyId(data.MediStockId, ref mediStock);
                if (data.MaterialId.HasValue)
                {
                    HIS_MATERIAL raw = null;
                    valid = valid && checker.VerifyId(data.MaterialId.Value, ref raw);
                    valid = valid && checker.IsUnLock(raw);
                    if (valid)
                    {
                        raws.Add(raw);
                    }
                }
                else if (data.MaterialTypeId.HasValue)
                {
                    valid = valid && checkerMaterialByType(param, mediStock.ID, data.MaterialTypeId.Value, ref raws);
                    valid = valid && checker.IsUnLock(raws);
                }
                else
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    Logging("MaterialId and MaterialTypeId is null.", LogType.Error);
                    valid = false;
                }

                valid = valid && CheckBean(param, mediStock.ID, raws.Select(s => s.ID).ToList(), ref ListMaterialBeanRaw);
                if (valid)
                {
                    AutoMapper.Mapper.CreateMap<HIS_MATERIAL_BEAN, HIS_MATERIAL_BEAN>();
                    List<HIS_MATERIAL_BEAN> materialBean = AutoMapper.Mapper.Map<List<HIS_MATERIAL_BEAN>>(ListMaterialBeanRaw);

                    List<HIS_MATERIAL_BEAN> updateActiveBean = new List<HIS_MATERIAL_BEAN>();
                    List<HIS_MATERIAL_BEAN> updateStockBean = new List<HIS_MATERIAL_BEAN>();
                    foreach (var item in raws)
                    {
                        decimal inStockAmount = GetInStockAmount(mediStock.ID, item.ID);
                        decimal totalImpAmount = GetImpAmount(mediStock.ID, item.ID);
                        decimal totalExpAmount = GetExpAmount(mediStock.ID, item.ID);

                        List<HIS_MATERIAL_BEAN> updateBean = materialBean.Where(s => s.MATERIAL_ID == item.ID).ToList();
                        decimal diffAmount = inStockAmount - (totalImpAmount - totalExpAmount);

                        if (diffAmount <= 0)
                        {
                            updateActiveBean.AddRange(updateBean);
                        }
                        else
                        {
                            for (int i = 0; i < materialBean.Count; i++)
                            {
                                if (diffAmount <= 0)
                                {
                                    updateActiveBean.Add(materialBean[i]);
                                }
                                else
                                {
                                    //nếu số lượng trong bean lớn hơn số lượnng chênh lệch thì giảmm đi số lượng chênh lệch
                                    //nếu số lượng trong bean nhỏ hơn hoặc bằng số lượng chênh lệch thì cho ra khỏi kho
                                    var amount = materialBean[i].AMOUNT;
                                    if (materialBean[i].AMOUNT > diffAmount)
                                    {
                                        materialBean[i].AMOUNT -= diffAmount;
                                        updateActiveBean.Add(materialBean[i]);
                                    }
                                    else
                                    {
                                        updateStockBean.Add(materialBean[i]);
                                    }

                                    diffAmount -= amount;
                                }
                            }
                        }
                    }

                    if (!UpdateMaterialBean(updateActiveBean, updateStockBean))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMaterial_CapNhatThatBai);
                        throw new Exception("Cap nhat thong tin HisMaterial that bai." + LogUtil.TraceData("data", data));
                    }

                    beforeUpdateHisMaterialBeans.AddRange(ListMaterialBeanRaw);
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

        private bool UpdateMaterialBean(List<HIS_MATERIAL_BEAN> updateActiveBean, List<HIS_MATERIAL_BEAN> updateStockBean)
        {
            bool result = true;
            try
            {
                List<string> listSql = new List<string>();
                if (IsNotNullOrEmpty(updateActiveBean))
                {
                    string sql = "UPDATE HIS_MATERIAL_BEAN SET IS_ACTIVE=1, AMOUNT={0} WHERE ID={1}";
                    foreach (var item in updateActiveBean)
                    {
                        listSql.Add(string.Format(sql, item.AMOUNT.ToString("G27", CultureInfo.InvariantCulture), item.ID));
                    }
                }

                if (IsNotNullOrEmpty(updateStockBean))
                {
                    string sql = "UPDATE HIS_MATERIAL_BEAN SET MEDI_STOCK_ID=null WHERE ID={0}";
                    Inventec.Common.Logging.LogSystem.Info("Update bean out stock: " + sql);
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

        private decimal GetInStockAmount(long medistockId, long materialId)
        {
            decimal result = 0;
            try
            {
                string sql = "SELECT SUM(AMOUNT) FROM HIS_MATERIAL_BEAN WHERE MATERIAL_ID = {0} AND MEDI_STOCK_ID = {1}";
                sql = string.Format(sql, materialId, medistockId);
                var amount = new MOS.DAO.Sql.SqlDAO().GetSqlSingle<Nullable<decimal>>(sql);
                if (amount.HasValue)
                {
                    result = amount.Value;
                }
            }
            catch (Exception ex)
            {
                result = 0;
                Inventec.Common.Logging.LogSystem.Info(string.Format("medistockId {0} materialId {1}", medistockId, materialId));
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private decimal GetExpAmount(long medistockId, long materialId)
        {
            decimal result = 0;
            try
            {
                string sql = "SELECT SUM(AMOUNT) FROM HIS_EXP_MEST_MATERIAL WHERE MATERIAL_ID = {0} AND TDL_MEDI_STOCK_ID = {1} AND IS_EXPORT = 1";
                sql = string.Format(sql, materialId, medistockId);
                var amount = new MOS.DAO.Sql.SqlDAO().GetSqlSingle<Nullable<decimal>>(sql);
                if (amount.HasValue)
                {
                    result = amount.Value;
                }
            }
            catch (Exception ex)
            {
                result = 0;
                Inventec.Common.Logging.LogSystem.Info(string.Format("medistockId {0} materialId {1}", medistockId, materialId));
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private decimal GetImpAmount(long medistockId, long materialId)
        {
            decimal result = 0;
            try
            {
                string sql = "SELECT SUM(AMOUNT) FROM HIS_IMP_MEST_MATERIAL MEMA WHERE MATERIAL_ID = {0} AND EXISTS (SELECT 1 FROM HIS_IMP_MEST WHERE MEMA.IMP_MEST_ID = ID AND MEDI_STOCK_ID = {1} AND IMP_TIME IS NOT NULL AND IMP_MEST_STT_ID = 5)";
                sql = string.Format(sql, materialId, medistockId);
                var amount = new MOS.DAO.Sql.SqlDAO().GetSqlSingle<Nullable<decimal>>(sql);
                if (amount.HasValue)
                {
                    result = amount.Value;
                }
            }
            catch (Exception ex)
            {
                result = 0;
                Inventec.Common.Logging.LogSystem.Info(string.Format("medistockId {0} materialId {1}", medistockId, materialId));
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        private bool checkerMaterialByType(CommonParam param, long mediStockId, long materialTypeId, ref List<HIS_MATERIAL> raws)
        {
            bool valid = true;
            try
            {
                string sql = "SELECT * FROM HIS_MATERIAL M WHERE EXISTS (SELECT 1 FROM HIS_MATERIAL_BEAN WHERE M.ID=MATERIAL_ID AND MEDI_STOCK_ID={0} AND TDL_MATERIAL_TYPE_ID={1})";
                sql = string.Format(sql, mediStockId, materialTypeId);
                raws = new MOS.DAO.Sql.SqlDAO().GetSql<HIS_MATERIAL>(sql);
                if (!IsNotNullOrEmpty(raws))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    Logging("ERROR mediStockId: " + mediStockId + "| materialTypeId: " + materialTypeId, LogType.Error);
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

        private bool CheckBean(CommonParam param, long mediStockId, List<long> materialIds, ref List<HIS_MATERIAL_BEAN> materialBean)
        {
            bool valid = true;
            try
            {
                string sql = "SELECT * FROM HIS_MATERIAL_BEAN WHERE MEDI_STOCK_ID = {0} AND MATERIAL_ID IN ({1}) AND (IS_ACTIVE IS NULL OR IS_ACTIVE <> 1) AND SESSION_KEY IS NULL AND EXP_MEST_MATERIAL_ID IS NULL";
                sql = string.Format(sql, mediStockId, string.Join(",", materialIds));
                materialBean = new MOS.DAO.Sql.SqlDAO().GetSql<HIS_MATERIAL_BEAN>(sql);
                if (!IsNotNullOrEmpty(materialBean))
                {
                    valid = false;
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisMaterial_VatTuDangThuocPhieuXuatChuaThucXuat);
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
            if (IsNotNullOrEmpty(this.beforeUpdateHisMaterialBeans))
            {
                if (!new MOS.MANAGER.HisMaterialBean.Update.HisMaterialBeanUpdate(param).UpdateList(this.beforeUpdateHisMaterialBeans))
                {
                    LogSystem.Warn("Rollback du lieu HisMaterialBean that bai, can kiem tra lai." + LogUtil.TraceData("HisMaterialBeans", this.beforeUpdateHisMaterialBeans));
                }
            }
        }
    }
}
