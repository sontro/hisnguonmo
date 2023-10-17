using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.MANAGER.Base;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisImpMest;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Common.Delete
{
    class HisExpMestTruncate : BusinessBase
    {
        private MedicineProcessor medicineProcessor;
        private MaterialProcessor materialProcessor;
        private BloodProcessor bloodProcessor;
        private VitaminAProcessor vitaminAProcessor;
        private ExpMestMetyReqProcessor expMestMetyReqProcessor;
        private ExpMestMatyReqProcessor expMestMatyReqProcessor;
        private ExpMestBltyReqProcessor expMestBltyReqProcessor;
        private SereServTeinProcessor sereServTeinProcessor;

        internal HisExpMestTruncate()
            : base()
        {
            this.Init();
        }

        internal HisExpMestTruncate(CommonParam param)
            : base(param)
        {
            this.Init();
        }

        private void Init()
        {
            this.bloodProcessor = new BloodProcessor(param);
            this.expMestBltyReqProcessor = new ExpMestBltyReqProcessor(param);
            this.expMestMatyReqProcessor = new ExpMestMatyReqProcessor(param);
            this.expMestMetyReqProcessor = new ExpMestMetyReqProcessor(param);
            this.materialProcessor = new MaterialProcessor(param);
            this.medicineProcessor = new MedicineProcessor(param);
            this.vitaminAProcessor = new VitaminAProcessor(param);
            this.sereServTeinProcessor = new SereServTeinProcessor(param);
        }

        internal bool Truncate(HisExpMestSDO data, bool allowDeletePres)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_EXP_MEST raw = null;
                HisExpMestCheck checker = new HisExpMestCheck(param);
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyId(data.ExpMestId, ref raw);
                valid = valid && checker.HasNotBill(raw);
                valid = valid && checker.HasNoDebt(raw);
                valid = valid && checker.IsUnlock(raw);
                valid = valid && checker.IsUnNotTaken(raw);
                valid = valid && (raw.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__DPK || checker.HasNotInExpMestAggr(raw) );//thuoc phieu tong hop, ko cho xoa don pk van cho phep xoa
                valid = valid && checker.VerifyStatusForDelete(raw);
                valid = valid && checker.HasNoNationalCode(raw);
                valid = valid && checker.CheckPermission(raw, allowDeletePres);
                valid = valid && checker.IsCompensationPres(raw);
                if (valid)
                {
                    if (raw.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__PL
                        || raw.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BL
                        || raw.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__THPK)
                    {
                        BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Phieu xuat loai la phieu linh. bu le. tong hop phong kham khong duoc thuc hien chuc nang nay" + LogUtil.TraceData("expMest", raw));
                    }
                    List<string> listExecuteSql = new List<string>();

                    if (!this.medicineProcessor.Run(raw, ref listExecuteSql))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                    if (!this.materialProcessor.Run(raw, ref listExecuteSql))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                    if (!this.bloodProcessor.Run(raw, ref listExecuteSql))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                    if (!this.expMestMetyReqProcessor.Run(raw, ref listExecuteSql))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                    if (!this.expMestMatyReqProcessor.Run(raw, ref listExecuteSql))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                    if (!this.expMestBltyReqProcessor.Run(raw, ref listExecuteSql))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }
                    if (!this.vitaminAProcessor.Run(raw, ref listExecuteSql))
                    {
                        throw new Exception("vitaminAProcessor. Rollback du lieu. Ket thuc nghiep vu");
                    }
                    if (!this.sereServTeinProcessor.Run(raw, ref listExecuteSql))
                    {
                        throw new Exception("sereServTeinProcessor. Rollback du lieu. Ket thuc nghiep vu");
                    }

                    //xu ly phieu xuat hoan cs va bu cs
                    this.ProcessChmsExpMest(raw, listExecuteSql);

                    listExecuteSql.Add(string.Format("DELETE HIS_EXP_MEST WHERE ID = {0}", raw.ID));

                    if (raw.AGGR_EXP_MEST_ID.HasValue)
                    {
                        //neu khong co phieu con nao khac thuoc phieu tong hop thi sau khi xoa don se tu dong xoa phieu tong hop tuong ung
                        listExecuteSql.Add(string.Format("DELETE HIS_EXP_MEST WHERE ID = {0} AND (SELECT COUNT(*) FROM HIS_EXP_MEST WHERE AGGR_EXP_MEST_ID = {0}) = 0", raw.AGGR_EXP_MEST_ID.Value));
                    }

                    StringBuilder sb = new StringBuilder();
                    sb.Append(string.Join(";", listExecuteSql)).Append(";");

                    string sql = string.Format("BEGIN {0} END;", sb.ToString());
                    if (!DAOWorker.SqlDAO.Execute(sql))
                    {
                        LogSystem.Warn("Xoa HIS_EXP_MEST that bai. Sql:" + sql.ToString());
                        return false;
                    }
                    new EventLogGenerator(EventLog.Enum.HisExpMest_HuyPhieuXuat).ExpMestCode(raw.EXP_MEST_CODE).Run();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
                param.HasException = true;
            }
            return result;
        }

        private void ProcessChmsExpMest(HIS_EXP_MEST data, List<string> listSql)
        {
            if (data.EXP_MEST_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_TYPE.ID__BCS)
            {
                List<HIS_EXP_MEST> hisExpBcs = new HisExpMestGet().GetByXbttExpMestId(data.ID);
                if (IsNotNullOrEmpty(hisExpBcs))
                {
                    if (!new HisExpMestCheck(param).IsUnlock(hisExpBcs))
                    {
                        throw new Exception("hisExpMestBcs dang bi khoa" + LogUtil.TraceData("hisExpBcs", hisExpBcs));
                    }
                    string updateExpMest = new StringBuilder().Append("UPDATE HIS_EXP_MEST SET XBTT_EXP_MEST_ID = NULL, TDL_XBTT_EXP_MEST_CODE = NULL WHERE XBTT_EXP_MEST_ID = ").Append(data.ID).ToString();
                    listSql.Add(updateExpMest);
                }
            }
        }
    }
}
