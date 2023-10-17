using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ.Delete
{
    class HisSereServDeleteSql : BusinessBase
    {
        private List<string> rollbackScripts = new List<string>();

        internal HisSereServDeleteSql()
            : base()
        {
        }

        internal HisSereServDeleteSql(CommonParam paramDelete)
            : base(paramDelete)
        {
        }

        internal bool Run(List<HIS_SERE_SERV> listRaw)
        {
            bool result = true;
            try
            {
                HisSereServCheck checker = new HisSereServCheck(param);
                List<long> sereServIds = listRaw.Select(o => o.ID).ToList();

                bool valid = IsNotNullOrEmpty(listRaw);
                valid = valid && checker.HasNoChild(sereServIds);
                valid = valid && checker.HasNoBill(listRaw);//Chi cho phep xoa doi voi cac sere_serv chua co bill
                valid = valid && checker.HasNoDeposit(sereServIds, false);
                valid = valid && checker.HasNoInvoice(listRaw);//Chi cho phep xoa doi voi cac sere_serv chua co invoice
                valid = valid && checker.HasNoHeinApproval(listRaw); //da duyet ho so Bao hiem thi ko cho phep sua

                if (valid)
                {
                    List<string> sqls = new List<string>();

                    //Luu y:
                    //+ can cap nhat medicine_id, material_id, blood_id ve null de tranh truong hop fk khi nguoi dung xoa thuoc/vat tu/mau sau khi xoa sere_serv
                    //+ Can cap nhat truong IS_SENT_EXT ve null de phuc vu viec lay du lieu gui sang he thong LIS/PACS
                    sqls.Add(DAOWorker.SqlDAO.AddInClause(sereServIds, "UPDATE HIS_SERE_SERV SET IS_DELETE = 1, MEDICINE_ID = NULL, MATERIAL_ID = NULL, BLOOD_ID = NULL, EXP_MEST_MEDICINE_ID = NULL, EXP_MEST_MATERIAL_ID = NULL, IS_SENT_EXT = NULL WHERE %IN_CLAUSE% ", "ID"));

                    if (!DAOWorker.SqlDAO.Execute(sqls))
                    {
                        throw new Exception("Xoa sere_serv that bai. Rollback du lieu. Ket thuc nghiep vu");
                    }

                    this.rollbackScripts = this.GenerateRollbackSql(listRaw);
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal void Rollback()
        {
            try
            {
                if (IsNotNullOrEmpty(this.rollbackScripts))
                {
                    if (!DAOWorker.SqlDAO.Execute(this.rollbackScripts))
                    {
                        LogSystem.Warn("Rollback xoa sere_serv that bai");
                    }
                    this.rollbackScripts = null; //tranh truong hop goi rollback 2 lan
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private List<string> GenerateRollbackSql(List<HIS_SERE_SERV> listRaw)
        {
            try
            {
                if (IsNotNullOrEmpty(listRaw))
                {
                    List<string> result = new List<string>();

                    foreach (HIS_SERE_SERV ss in listRaw)
                    {
                        string medicineId = ss.MEDICINE_ID.HasValue ? ss.MEDICINE_ID.ToString() : "NULL";
                        string materialId = ss.MATERIAL_ID.HasValue ? ss.MATERIAL_ID.ToString() : "NULL";
                        string bloodId = ss.BLOOD_ID.HasValue ? ss.BLOOD_ID.ToString() : "NULL";
                        string expMedicineId = ss.EXP_MEST_MEDICINE_ID.HasValue ? ss.EXP_MEST_MEDICINE_ID.ToString() : "NULL";
                        string expMaterialId = ss.EXP_MEST_MATERIAL_ID.HasValue ? ss.EXP_MEST_MATERIAL_ID.ToString() : "NULL";

                        string sql = string.Format("UPDATE HIS_SERE_SERV SET IS_DELETE = 0, MEDICINE_ID = {0}, MATERIAL_ID = {1}, BLOOD_ID = {2}, EXP_MEST_MEDICINE_ID = {3}, EXP_MEST_MATERIAL_ID = {4} WHERE ID = {5}", medicineId, materialId, bloodId, expMedicineId, expMaterialId, ss.ID);
                        result.Add(sql);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return null;
        }
    }
}
