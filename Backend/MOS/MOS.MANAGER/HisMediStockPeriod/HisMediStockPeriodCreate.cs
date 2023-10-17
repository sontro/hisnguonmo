using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisImpMest;
using MOS.MANAGER.HisMediStock;
using MOS.SDO;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MOS.MANAGER.HisMediStockPeriod
{
    partial class HisMediStockPeriodCreate : BusinessBase
    {
        private HIS_MEDI_STOCK_PERIOD recentHisMediStockPeriod;
        private HIS_MEDI_STOCK_PERIOD previousHisMediStockPeriod;
        private List<long> listImpId = new List<long>();

        private const int MAX_IN_CLAUSE_SIZE = 700;

        internal HisMediStockPeriodCreate()
            : base()
        {
        }

        internal HisMediStockPeriodCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
        }

        internal bool Create(HIS_MEDI_STOCK_PERIOD data, ref HIS_MEDI_STOCK_PERIOD resultData, bool isAuto = false)
        {
            bool result = false;
            HIS_MEDI_STOCK mediStock = null;
            try
            {
                bool valid = true;
                HisMediStockPeriodCheck checker = new HisMediStockPeriodCheck(param);
                HisMediStockCheck mediStockChecker = new HisMediStockCheck(param);
                valid = valid && mediStockChecker.VerifyId(data.MEDI_STOCK_ID, ref mediStock);
                valid = valid && mediStockChecker.IsUnLock(mediStock);
                valid = valid && checker.CheckToTime(data, ref previousHisMediStockPeriod);
                if (valid)
                {
                    //if (!data.TO_TIME.HasValue)
                    //{
                    //    hasTimeTo = false;
                    //    string storedSql = "PKG_CREATE_MEDI_STOCK_PERIOD.PRO_CREATE_MEDI_STOCK_PERIOD";

                    //    string creator = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();

                    //    OracleParameter mediStockIdPar = new OracleParameter("P_MEDI_STOCK_ID", OracleDbType.Int32, data.MEDI_STOCK_ID, ParameterDirection.Input);
                    //    OracleParameter creatorPar = new OracleParameter("P_CREATOR", OracleDbType.Varchar2, creator, ParameterDirection.Input);
                    //    OracleParameter appCreatorPar = new OracleParameter("P_APP_CREATOR", OracleDbType.Varchar2, MOS.UTILITY.Constant.APPLICATION_CODE, ParameterDirection.Input);
                    //    OracleParameter namePar = new OracleParameter("P_MEDI_STOCK_PERIOD_NAME", OracleDbType.Varchar2, data.MEDI_STOCK_PERIOD_NAME, ParameterDirection.Input);
                    //    OracleParameter resultPar = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    //    object resultHolder = null;

                    //    if (DAOWorker.SqlDAO.ExecuteStored(OutputHandler, ref resultHolder, storedSql, mediStockIdPar, namePar, creatorPar, appCreatorPar, resultPar))
                    //    {
                    //        if (resultHolder != null)
                    //        {
                    //            resultData = (HIS_MEDI_STOCK_PERIOD)resultHolder;
                    //            result = resultData != null;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    hasTimeTo = true;
                    //    string storedSql = "PKG_MEDI_STOCK_PERIOD_NOT_BEAN.PRO_MEDI_STOCK_PERIOD_NOT_BEAN";

                    //    string creator = Inventec.Token.ResourceSystem.ResourceTokenManager.GetLoginName();

                    //    OracleParameter mediStockIdPar = new OracleParameter("P_MEDI_STOCK_ID", OracleDbType.Int32, data.MEDI_STOCK_ID, ParameterDirection.Input);
                    //    OracleParameter creatorPar = new OracleParameter("P_CREATOR", OracleDbType.Varchar2, creator, ParameterDirection.Input);
                    //    OracleParameter appCreatorPar = new OracleParameter("P_APP_CREATOR", OracleDbType.Varchar2, MOS.UTILITY.Constant.APPLICATION_CODE, ParameterDirection.Input);
                    //    OracleParameter namePar = new OracleParameter("P_MEDI_STOCK_PERIOD_NAME", OracleDbType.Varchar2, data.MEDI_STOCK_PERIOD_NAME, ParameterDirection.Input);
                    //    OracleParameter toTimePar = new OracleParameter("P_TO_TIME", OracleDbType.Varchar2, data.TO_TIME.Value, ParameterDirection.Input);
                    //    OracleParameter resultPar = new OracleParameter("P_RESULT", OracleDbType.Int32, ParameterDirection.Output);

                    //    object resultHolder = null;

                    //    if (DAOWorker.SqlDAO.ExecuteStored(OutputHandler, ref resultHolder, storedSql, mediStockIdPar, namePar, creatorPar, appCreatorPar, toTimePar, resultPar))
                    //    {
                    //        if (resultHolder != null)
                    //        {
                    //            resultData = (HIS_MEDI_STOCK_PERIOD)resultHolder;
                    //            result = resultData != null;
                    //        }
                    //    }
                    //}

                    this.LockMediStock(data.MEDI_STOCK_ID);
                    this.PrepareDataMaterial(data);
                    this.PrepareDataMedicine(data);
                    this.PrepareDataBlood(data);
                    this.ProcessHisMediStockPeriod(data, isAuto);
                    this.ProcessMestPeriodMaty();
                    this.ProcessMestPeriodMate();
                    this.ProcessMestPeriodMety();
                    this.ProcessMestPeriodMedi();
                    this.ProcessMestPeriodBlty();
                    this.ProcessMestPeriodBlood();
                    this.UnLockMediStock(mediStock.ID);//unlock medi_stock truoc de co the cap nhat exp_mest va imp_mest
                    this.ProcessImpExpMest();
                    result = true;
                    resultData = recentHisMediStockPeriod;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                UnLockMediStock(mediStock.ID);
                RollBack();
                result = false;
            }
            return result;
        }

        ////Xu ly ket qua tra ve khi goi procedure
        //private void OutputHandler(ref object resultHolder, params OracleParameter[] parameters)
        //{
        //    try
        //    {
        //        //Tham so thu 5 chua output (khong truyen time_to)
        //        if (!hasTimeTo && parameters[4] != null && parameters[4].Value != null)
        //        {
        //            long id = long.Parse(parameters[4].Value.ToString());
        //            resultHolder = new HisMediStockPeriodGet().GetById(id);
        //        }
        //        //Tham so thu 6 chua output (truyen time_to)
        //        if (hasTimeTo && parameters[5] != null && parameters[5].Value != null)
        //        {
        //            long id = long.Parse(parameters[5].Value.ToString());
        //            resultHolder = new HisMediStockPeriodGet().GetById(id);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogSystem.Error(ex);
        //    }
        //}

        /// <summary>
        /// Tao du lieu HisMediStockPeriod
        /// </summary>
        /// <param name="data"></param>
        private void ProcessHisMediStockPeriod(HIS_MEDI_STOCK_PERIOD data, bool isAuto)
        {
            data.PREVIOUS_ID = this.previousHisMediStockPeriod != null ? new Nullable<long>(this.previousHisMediStockPeriod.ID) : null;

            List<long> listExpId = new List<long>();
            if (IsNotNullOrEmpty(DicAppliedExpMestMaterials))
            {
                listExpId.AddRange(DicAppliedExpMestMaterials.SelectMany(s => s.Value).Select(s => s.EXP_MEST_ID ?? 0).ToList());
            }

            if (IsNotNullOrEmpty(DicAppliedExpMestMedicines))
            {
                listExpId.AddRange(DicAppliedExpMestMedicines.SelectMany(s => s.Value).Select(s => s.EXP_MEST_ID ?? 0).ToList());
            }

            if (IsNotNullOrEmpty(appliedExpMestBloods))
            {
                listExpId.AddRange(appliedExpMestBloods.Select(s => s.EXP_MEST_ID).ToList());
            }

            if (IsNotNullOrEmpty(DicAppliedImpMestMaterials))
            {
                listImpId.AddRange(DicAppliedImpMestMaterials.SelectMany(s => s.Value).Select(s => s.IMP_MEST_ID).ToList());
            }

            if (IsNotNullOrEmpty(DicAppliedImpMestMedicines))
            {
                listImpId.AddRange(DicAppliedImpMestMedicines.SelectMany(s => s.Value).Select(s => s.IMP_MEST_ID).ToList());
            }

            if (IsNotNullOrEmpty(appliedImpMestBloods))
            {
                listImpId.AddRange(appliedImpMestBloods.Select(s => s.IMP_MEST_ID).ToList());
            }

            List<long> listMedicineType = new List<long>();
            var groupMety = DicAppliedImpMestMedicines.SelectMany(s => s.Value).GroupBy(o => o.MEDICINE_TYPE_ID).ToList();
            foreach (var item in groupMety)
            {
                decimal amountExp = DicAppliedExpMestMedicines.SelectMany(s => s.Value).Where(o => o.TDL_MEDICINE_TYPE_ID == item.Key).Sum(s => s.AMOUNT);
                if (item.Sum(s => s.AMOUNT) - amountExp != 0)
                {
                    listMedicineType.Add(item.Key);
                }
            }

            List<long> listMaterialType = new List<long>();
            var groupMaty = DicAppliedImpMestMaterials.SelectMany(s => s.Value).GroupBy(o => o.MATERIAL_TYPE_ID).ToList();
            foreach (var item in groupMaty)
            {
                decimal amountExp = DicAppliedExpMestMaterials.SelectMany(s => s.Value).Where(o => o.TDL_MATERIAL_TYPE_ID == item.Key).Sum(s => s.AMOUNT);
                if (item.Sum(s => s.AMOUNT) - amountExp != 0)
                {
                    listMaterialType.Add(item.Key);
                }
            }

            listExpId = listExpId.Distinct().ToList();
            listImpId = listImpId.Distinct().ToList();

            data.COUNT_EXP_MEST = listExpId.Count;
            data.COUNT_IMP_MEST = listImpId.Count;
            data.COUNT_MEDICINE_TYPE = listMedicineType.Count;
            data.COUNT_MATERIAL_TYPE = listMaterialType.Count;
            data.IS_AUTO_PERIOD = isAuto ? data.IS_AUTO_PERIOD : null;

            if (!DAOWorker.HisMediStockPeriodDAO.Create(data))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediStockPeriod_ThemMoiThatBai);
                throw new Exception("Tao du lieu HIS_MEDI_STOCK_PERIOD that bai. " + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => data), data));
            }

            this.recentHisMediStockPeriod = data;
        }

        /// <summary>
        /// Xu ly truoc du lieu
        /// </summary>
        /// <param name="mediStockId"></param>
        private void LockMediStock(long mediStockId)
        {
            //Khoa kho truoc khi thuc hien tong hop
            if (!new HisMediStockLock().Lock(mediStockId))
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisMediStock_KhoaThatBai);
                throw new Exception("Khong the tien hanh bat dau chot ky vi khong khoa duoc kho, mediStockId :" + mediStockId);
            }
        }

        /// <summary>
        /// Mo khoa kho khi xu ly xong
        /// </summary>
        private void UnLockMediStock(long mediStockId)
        {
            //Mo khoa khi ket thuc kiem ke
            if (!new HisMediStockLock().UnLock(mediStockId))
            {
                throw new Exception("Mo khoa kho that bai. mediStockId:" + mediStockId);
            }
        }

        /// <summary>
        /// Thuc hien rollback du lieu
        /// </summary>
        private void RollBack()
        {
            #region Rollback du lieu HisMediStockPeriod
            if (this.recentHisMediStockPeriod != null)
            {
                List<string> sqls = new List<string>();
                sqls.Add(string.Format("DELETE HIS_MEST_PERIOD_BLOOD WHERE MEDI_STOCK_PERIOD_ID = {0}", recentHisMediStockPeriod.ID));
                sqls.Add(string.Format("DELETE HIS_MEST_PERIOD_BLTY WHERE MEDI_STOCK_PERIOD_ID = {0}", recentHisMediStockPeriod.ID));
                sqls.Add(string.Format("DELETE HIS_MEST_PERIOD_MATE WHERE MEDI_STOCK_PERIOD_ID = {0}", recentHisMediStockPeriod.ID));
                sqls.Add(string.Format("DELETE HIS_MEST_PERIOD_MATY WHERE MEDI_STOCK_PERIOD_ID = {0}", recentHisMediStockPeriod.ID));
                sqls.Add(string.Format("DELETE HIS_MEST_PERIOD_MEDI WHERE MEDI_STOCK_PERIOD_ID = {0}", recentHisMediStockPeriod.ID));
                sqls.Add(string.Format("DELETE HIS_MEST_PERIOD_METY WHERE MEDI_STOCK_PERIOD_ID = {0}", recentHisMediStockPeriod.ID));
                sqls.Add(string.Format("DELETE HIS_MEDI_STOCK_PERIOD WHERE ID = {0}", recentHisMediStockPeriod.ID));

                if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
                {
                    LogSystem.Warn("Rollback du lieu HisMediStockPeriod that bai." + LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => this.recentHisMediStockPeriod), this.recentHisMediStockPeriod));
                    throw new Exception("Sql: " + sqls.ToString());
                }
            }
            #endregion
        }

        /// <summary>
        /// cap nhat cac phieu nhap xuat duoc chot ky
        /// </summary>
        private void ProcessImpExpMest()
        {
            List<string> sqls = new List<string>();
            //do so luong id rat lon nen can tach nhieu cau sql thay vi tao 1 cau co nhieu id
            if (IsNotNullOrEmpty(listImpId))
            {
                int skip = 0;
                while (listImpId.Count - skip > 0)
                {
                    List<long> listIds = listImpId.Skip(skip).Take(MAX_IN_CLAUSE_SIZE).ToList();
                    skip += MAX_IN_CLAUSE_SIZE;
                    sqls.Add(DAOWorker.SqlDAO.AddInClause(listIds, string.Format("UPDATE HIS_IMP_MEST SET MEDI_STOCK_PERIOD_ID = {0} WHERE %IN_CLAUSE%", recentHisMediStockPeriod.ID), "ID"));
                }
            }

            if (IsNotNullOrEmpty(DicAppliedExpMestMaterials))
            {
                List<long> expMestMaterialIds = DicAppliedExpMestMaterials.SelectMany(s => s.Value).Select(s => s.ID).ToList();
                int skip = 0;
                while (expMestMaterialIds.Count - skip > 0)
                {
                    List<long> listIds = expMestMaterialIds.Skip(skip).Take(MAX_IN_CLAUSE_SIZE).ToList();
                    skip += MAX_IN_CLAUSE_SIZE;
                    sqls.Add(DAOWorker.SqlDAO.AddInClause(listIds, string.Format("UPDATE HIS_EXP_MEST_MATERIAL SET MEDI_STOCK_PERIOD_ID = {0} WHERE %IN_CLAUSE%", recentHisMediStockPeriod.ID), "ID"));
                }
            }

            if (IsNotNullOrEmpty(DicAppliedExpMestMedicines))
            {
                List<long> expMestMedicineIds = DicAppliedExpMestMedicines.SelectMany(s => s.Value).Select(s => s.ID).ToList();
                int skip = 0;
                while (expMestMedicineIds.Count - skip > 0)
                {
                    List<long> listIds = expMestMedicineIds.Skip(skip).Take(MAX_IN_CLAUSE_SIZE).ToList();
                    skip += MAX_IN_CLAUSE_SIZE;
                    sqls.Add(DAOWorker.SqlDAO.AddInClause(listIds, string.Format("UPDATE HIS_EXP_MEST_MEDICINE SET MEDI_STOCK_PERIOD_ID = {0} WHERE %IN_CLAUSE%", recentHisMediStockPeriod.ID), "ID"));
                }
            }

            if (IsNotNullOrEmpty(appliedExpMestBloods))
            {
                List<long> expMestBloodIds = appliedExpMestBloods.Select(s => s.ID).ToList();
                int skip = 0;
                while (expMestBloodIds.Count - skip > 0)
                {
                    List<long> listIds = expMestBloodIds.Skip(skip).Take(MAX_IN_CLAUSE_SIZE).ToList();
                    skip += MAX_IN_CLAUSE_SIZE;
                    sqls.Add(DAOWorker.SqlDAO.AddInClause(listIds, string.Format("UPDATE HIS_EXP_MEST_BLOOD SET MEDI_STOCK_PERIOD_ID = {0} WHERE %IN_CLAUSE%", recentHisMediStockPeriod.ID), "ID"));
                }
            }

            if (IsNotNullOrEmpty(sqls) && !DAOWorker.SqlDAO.Execute(sqls))
            {
                throw new Exception("Cap nhat HIS_IMP_MEST, HIS_EXP_MEST_BLOOD, HIS_EXP_MEST_MATERIAL, HIS_EXP_MEST_MEDICINE that bai");
            }
        }
    }
}
