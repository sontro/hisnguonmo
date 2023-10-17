using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMestMaterial;
using MOS.MANAGER.HisExpMestMatyReq;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisExpMestMetyReq;
using MOS.SDO;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.MANAGER.HisExpMestBltyReq;
using MOS.MANAGER.HisExpMest.Common.Create;
using MOS.MANAGER.HisExpMest.Common.Update;
using MOS.MANAGER.EventLogUtil;
using MOS.LibraryEventLog;
using MOS.UTILITY;
using MOS.MANAGER.HisExpMest.Common;

namespace MOS.MANAGER.HisExpMest.Aggr.Create
{
    /// <summary>
    /// Tạo phiếu lĩnh:
    /// 1. Các phiếu con phải chưa thuộc phiếu lĩnh nào
    /// 2. Các phiếu con phải là đơn nội trú
    /// 3. Các đơn phải cùng khoa y/c với khoa mà người dùng đang làm việc
    /// </summary>
    partial class HisExpMestAggrCreate : BusinessBase
    {
        private HisExpMestCreate hisExpMestCreate;

        internal HisExpMestAggrCreate()
            : base()
        {
            this.Init();
        }

        internal HisExpMestAggrCreate(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.Init();
        }

        private void Init()
        {
            this.hisExpMestCreate = new HisExpMestCreate(param);
        }

        internal bool Create(HisExpMestAggrSDO data, ref List<HIS_EXP_MEST> resultData)
        {
            bool result = false;
            try
            {
                WorkPlaceSDO workPlaceSdo = null;
                List<HIS_EXP_MEST> children = null;
                bool valid = true;
                HisExpMestAggrCreateCheck checker = new HisExpMestAggrCreateCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && this.HasWorkPlaceInfo(data.ReqRoomId, ref workPlaceSdo);
                valid = valid && checker.IsAllowed(data, workPlaceSdo, ref children);
                if (valid)
                {
                    //Luu anh xa giua phieu linh va cac phieu con
                    Dictionary<HIS_EXP_MEST, List<HIS_EXP_MEST>> aggrDic = new Dictionary<HIS_EXP_MEST, List<HIS_EXP_MEST>>();

                    List<HIS_EXP_MEST> aggrs = null;

                    this.CreateAggr(data, workPlaceSdo, children, aggrDic, ref aggrs);

                    this.UpdateChildren(aggrDic);

                    resultData = aggrs;

                    HisAggrExpMestLog.Run(aggrDic, LibraryEventLog.EventLog.Enum.HisExpMest_TongHopPhieuLinh);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                resultData = null;
                this.RollBack();
                result = false;
            }
            return result;
        }

        private void CreateAggr(HisExpMestAggrSDO data, WorkPlaceSDO workPlaceSdo, List<HIS_EXP_MEST> children, Dictionary<HIS_EXP_MEST, List<HIS_EXP_MEST>> aggrDic, ref List<HIS_EXP_MEST> aggrs)
        {
            long createYear = DateTime.Now.Year;
            bool isAllEqual = children.TrueForAll(x => x.TDL_PATIENT_TYPE_ID.Equals(children.First().TDL_PATIENT_TYPE_ID));
            long? patientTypeOfChild = isAllEqual ? children.FirstOrDefault().TDL_PATIENT_TYPE_ID : null;
            //Tao du lieu phieu linh
            //Tach theo kho xuat cua cac phieu con
            //Neu co cau hinh tach theo don thuoc dau * thi thuc hien tach theo don thuoc dau *
            if (HisExpMestCFG.IS_SPLIT_STAR_MARK && HisExpMestCFG.SPECIAL_MEDICINE_NUM_ORDER_OPTION != HisExpMestCFG.SpecialMedicineNumOrderOption.BY_YEAR__TYPE__REQ_DEPARTMENT__MEDI_STOCK)
            {
                var groups = children.GroupBy(o => new { o.MEDI_STOCK_ID, IsStarMark = (o.IS_STAR_MARK == Constant.IS_TRUE), o.EXP_MEST_REASON_ID });
                foreach (var g in groups)
                {
                    HIS_EXP_MEST expMest = new HIS_EXP_MEST();
                    expMest.MEDI_STOCK_ID = g.Key.MEDI_STOCK_ID;
                    expMest.IS_STAR_MARK = g.Key.IsStarMark ? (short?)(Constant.IS_TRUE) : null;
                    expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                    expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL;
                    expMest.REQ_ROOM_ID = data.ReqRoomId;
                    expMest.REQ_DEPARTMENT_ID = workPlaceSdo.DepartmentId;
                    expMest.DESCRIPTION = data.Description;
                    expMest.TDL_AGGR_TREATMENT_CODE = string.Join(";", g.Select(s => s.TDL_TREATMENT_CODE).Distinct());
                    expMest.TDL_AGGR_PATIENT_CODE = string.Join(";", g.Select(s => s.TDL_PATIENT_CODE).Distinct());
                    expMest.EXP_MEST_REASON_ID = g.Key.EXP_MEST_REASON_ID;
                    expMest.TDL_PATIENT_TYPE_ID = patientTypeOfChild;
                    aggrDic.Add(expMest, g.ToList());
                }
            }
            //Neu co cau hinh tach theo don thuoc dau * thi thuc hien tach theo don thuoc dau * va danh so thu tu theo loai don thi phai tach phieu linh theo ca loai don
            else if (HisExpMestCFG.IS_SPLIT_STAR_MARK && HisExpMestCFG.SPECIAL_MEDICINE_NUM_ORDER_OPTION == HisExpMestCFG.SpecialMedicineNumOrderOption.BY_YEAR__TYPE__REQ_DEPARTMENT__MEDI_STOCK)
            {
                var groups = children.GroupBy(o => new { o.MEDI_STOCK_ID, IsStarMark = (o.IS_STAR_MARK == Constant.IS_TRUE), o.SPECIAL_MEDICINE_TYPE, o.EXP_MEST_REASON_ID });
                foreach (var g in groups)
                {
                    HIS_EXP_MEST expMest = new HIS_EXP_MEST();
                    expMest.MEDI_STOCK_ID = g.Key.MEDI_STOCK_ID;
                    expMest.IS_STAR_MARK = g.Key.IsStarMark ? (short?)(Constant.IS_TRUE) : null;
                    expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                    expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL;
                    expMest.SPECIAL_MEDICINE_TYPE = g.Key.SPECIAL_MEDICINE_TYPE;
                    expMest.REQ_ROOM_ID = data.ReqRoomId;
                    expMest.REQ_DEPARTMENT_ID = workPlaceSdo.DepartmentId;
                    expMest.DESCRIPTION = data.Description;
                    expMest.TDL_AGGR_TREATMENT_CODE = string.Join(";", g.Select(s => s.TDL_TREATMENT_CODE).Distinct());
                    expMest.TDL_AGGR_PATIENT_CODE = string.Join(";", g.Select(s => s.TDL_PATIENT_CODE).Distinct());
                    //Danh STT xuat thuoc dac biet
                    expMest.SPECIAL_MEDICINE_NUM_ORDER = HisExpMestUtil.GetNextSpeciaMedicineTypeNumOrder(expMest.SPECIAL_MEDICINE_TYPE, expMest.REQ_DEPARTMENT_ID, createYear, expMest.MEDI_STOCK_ID);
                    expMest.EXP_MEST_REASON_ID = g.Key.EXP_MEST_REASON_ID;
                    expMest.TDL_PATIENT_TYPE_ID = patientTypeOfChild;
                    aggrDic.Add(expMest, g.ToList());
                }
            }
            //Neu co cau hinh danh so thu tu theo loai don thi phai tach phieu linh theo loai don
            else if (!HisExpMestCFG.IS_SPLIT_STAR_MARK && HisExpMestCFG.SPECIAL_MEDICINE_NUM_ORDER_OPTION == HisExpMestCFG.SpecialMedicineNumOrderOption.BY_YEAR__TYPE__REQ_DEPARTMENT__MEDI_STOCK)
            {
                var groups = children.GroupBy(o => new { o.MEDI_STOCK_ID, o.SPECIAL_MEDICINE_TYPE, o.EXP_MEST_REASON_ID });
                foreach (var g in groups)
                {
                    HIS_EXP_MEST expMest = new HIS_EXP_MEST();
                    expMest.MEDI_STOCK_ID = g.Key.MEDI_STOCK_ID;
                    expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                    expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL;
                    expMest.SPECIAL_MEDICINE_TYPE = g.Key.SPECIAL_MEDICINE_TYPE;
                    expMest.REQ_ROOM_ID = data.ReqRoomId;
                    expMest.REQ_DEPARTMENT_ID = workPlaceSdo.DepartmentId;
                    expMest.TDL_AGGR_TREATMENT_CODE = string.Join(";", g.Select(s => s.TDL_TREATMENT_CODE).Distinct());
                    expMest.TDL_AGGR_PATIENT_CODE = string.Join(";", g.Select(s => s.TDL_PATIENT_CODE).Distinct());
                    //Danh STT xuat thuoc dac biet
                    expMest.SPECIAL_MEDICINE_NUM_ORDER = HisExpMestUtil.GetNextSpeciaMedicineTypeNumOrder(expMest.SPECIAL_MEDICINE_TYPE, expMest.REQ_DEPARTMENT_ID, createYear, expMest.MEDI_STOCK_ID);
                    expMest.DESCRIPTION = data.Description;
                    expMest.EXP_MEST_REASON_ID = g.Key.EXP_MEST_REASON_ID;
                    expMest.TDL_PATIENT_TYPE_ID = patientTypeOfChild;
                    aggrDic.Add(expMest, g.ToList());
                }
            }
            else
            {
                var groups = children.GroupBy(o => new { o.MEDI_STOCK_ID, o.EXP_MEST_REASON_ID });
                foreach (var g in groups)
                {
                    HIS_EXP_MEST expMest = new HIS_EXP_MEST();
                    expMest.MEDI_STOCK_ID = g.Key.MEDI_STOCK_ID;
                    expMest.EXP_MEST_STT_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__REQUEST;
                    expMest.EXP_MEST_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL;
                    expMest.REQ_ROOM_ID = data.ReqRoomId;
                    expMest.REQ_DEPARTMENT_ID = workPlaceSdo.DepartmentId;
                    expMest.DESCRIPTION = data.Description;
                    expMest.TDL_AGGR_TREATMENT_CODE = string.Join(";", g.Select(s => s.TDL_TREATMENT_CODE).Distinct());
                    expMest.TDL_AGGR_PATIENT_CODE = string.Join(";", g.Select(s => s.TDL_PATIENT_CODE).Distinct());
                    expMest.EXP_MEST_REASON_ID = g.Key.EXP_MEST_REASON_ID;
                    expMest.TDL_PATIENT_TYPE_ID = patientTypeOfChild;
                    aggrDic.Add(expMest, g.ToList());
                }
            }

            //Tao phieu linh
            aggrs = aggrDic.Keys.ToList();
            if (!this.hisExpMestCreate.CreateList(aggrs))
            {
                throw new Exception("Tao phieu linh that bai");
            }
        }

        private void UpdateChildren(Dictionary<HIS_EXP_MEST, List<HIS_EXP_MEST>> aggrDic)
        {
            List<string> sqls = new List<string>();

            List<HIS_EXP_MEST> aggrs = aggrDic.Keys.ToList();

            foreach (HIS_EXP_MEST aggr in aggrs)
            {
                List<HIS_EXP_MEST> tmp = aggrDic[aggr];

                //update exp_mest con
                string sqlExpMest = "UPDATE HIS_EXP_MEST SET AGGR_EXP_MEST_ID = {0}, TDL_AGGR_EXP_MEST_CODE = '{1}' WHERE %IN_CLAUSE%";
                sqlExpMest = DAOWorker.SqlDAO.AddInClause(tmp.Select(o => o.ID).ToList(), sqlExpMest, "ID");
                sqlExpMest = string.Format(sqlExpMest, aggr.ID, aggr.EXP_MEST_CODE);
                sqls.Add(sqlExpMest);

                //update exp_mest_medicine
                string sqlMedicine = "UPDATE HIS_EXP_MEST_MEDICINE SET TDL_AGGR_EXP_MEST_ID = {0} WHERE %IN_CLAUSE%";
                sqlMedicine = DAOWorker.SqlDAO.AddInClause(tmp.Select(o => o.ID).ToList(), sqlMedicine, "EXP_MEST_ID");
                sqlMedicine = string.Format(sqlMedicine, aggr.ID);
                sqls.Add(sqlMedicine);

                //update exp_mest_material
                string sqlMaterial = "UPDATE HIS_EXP_MEST_MATERIAL SET TDL_AGGR_EXP_MEST_ID = {0} WHERE %IN_CLAUSE%";
                sqlMaterial = DAOWorker.SqlDAO.AddInClause(tmp.Select(o => o.ID).ToList(), sqlMaterial, "EXP_MEST_ID");
                sqlMaterial = string.Format(sqlMaterial, aggr.ID);
                sqls.Add(sqlMaterial);
            }

            if (IsNotNullOrEmpty(sqls))
            {
                if (!DAOWorker.SqlDAO.Execute(sqls))
                {
                    throw new Exception("Rollback du lieu");
                }
            }
        }

        private void RollBack()
        {
            this.hisExpMestCreate.RollbackData();
        }
    }
}
