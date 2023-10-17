using Inventec.Core;
using MOS.Dashboard.Base;
using MOS.Dashboard.DDO;
using MOS.Dashboard.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Dashboard.HisTreatment
{
    class HisTreatmentGet : GetBase
    {
        internal HisTreatmentGet()
            : base()
        {

        }

        internal HisTreatmentGet(CommonParam param)
            : base(param)
        {

        }

        internal List<TreatmentIcdDDO> GetTopIcd(TreatmentIcdFilter filter)
        {
            List<TreatmentIcdDDO> result = null;
            try
            {
                StringBuilder sb = new StringBuilder()
                .Append("SELECT TREA.ID, TREA.TREATMENT_CODE, TREA.IS_PAUSE, TREA.ICD_CODE, TREA.ICD_NAME, TREA.ICD_SUB_CODE, TREA.ICD_TEXT, TREA.OUT_TIME, TREA.END_ROOM_ID, TREA.END_DEPARTMENT_ID, TREA.OUT_DATE")
                .Append(" FROM HIS_TREATMENT TREA")
                .Append(" WHERE TREA.IS_PAUSE = 1")
                .Append(" AND TREA.TDL_TREATMENT_TYPE_ID = 1")
                .Append(" AND TREA.END_DEPARTMENT_ID =: param1")
                .Append(" AND TREA.OUT_DATE =: param2");

                string sql = sb.ToString();
                List<TreatmentData> datas = DAOWorker.SqlDAO.GetSql<TreatmentData>(sql, filter.DepartmentId, filter.OutDate);
                if (IsNotNullOrEmpty(datas))
                {
                    result = new List<TreatmentIcdDDO>();
                    Dictionary<string, TreatmentIcdDDO> dicIcd = new Dictionary<string, TreatmentIcdDDO>();

                    foreach (var treat in datas)
                    {
                        List<string> icdCodes = this.ProcessIcdTreatment(treat);
                        if (IsNotNullOrEmpty(icdCodes))
                        {
                            foreach (var code in icdCodes)
                            {
                                TreatmentIcdDDO ddo = new TreatmentIcdDDO();
                                if (dicIcd.ContainsKey(code))
                                {
                                    ddo = dicIcd[code];
                                }
                                else
                                {
                                    ddo.IcdCode = code;
                                    dicIcd[code] = ddo;
                                }
                                ddo.Amount += 1;
                            }
                        }
                    }

                    if (this.param.Limit.HasValue)
                    {
                        int start = this.param.Start ?? 0;
                        int limit = this.param.Limit.Value;
                        result = dicIcd.Values.OrderByDescending(o => o.Amount).Skip(start).Take(limit).ToList();
                    }
                    else
                    {
                        result = dicIcd.Values.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        private List<string> ProcessIcdTreatment(TreatmentData treat)
        {
            List<string> rs = new List<string>();
            if (!String.IsNullOrWhiteSpace(treat.ICD_CODE))
            {
                string code = treat.ICD_CODE;
                code = code.Trim(' ', ',');
                rs.Add(code);
            }
            if (!String.IsNullOrWhiteSpace(treat.ICD_SUB_CODE))
            {
                List<string> subs = treat.ICD_SUB_CODE.Split(',').ToList();
                subs.ForEach(o => o = o.Trim(' ', ','));
                subs = subs.Where(o => !String.IsNullOrWhiteSpace(o)).ToList();
                if (IsNotNullOrEmpty(subs))
                {
                    rs.AddRange(subs);
                }
            }
            if (IsNotNullOrEmpty(rs))
            {
                rs = rs.Distinct().ToList();
            }
            return rs;
        }

        internal List<TreatmentTimeDDO> GetWithTime(TreatmentTimeFilter filter)
        {
            List<TreatmentTimeDDO> result = null;
            try
            {

                StringBuilder sb = new StringBuilder()
                .Append("SELECT TREA.ID, TREA.TREATMENT_CODE, TREA.IN_TIME, TREA.OUT_TIME, TREA.TDL_PATIENT_CODE, TREA.TDL_PATIENT_NAME, TREA.TDL_PATIENT_DOB, TREA.TDL_PATIENT_IS_HAS_NOT_DAY_DOB, TREA.TDL_PATIENT_ADDRESS, TREA.TDL_PATIENT_GENDER_ID, TREA.TDL_PATIENT_GENDER_NAME")
                .Append(", (SELECT 1 FROM DUAL WHERE EXISTS (SELECT 1 FROM HIS_SERVICE_REQ XN")
                .Append(" WHERE XN.TREATMENT_ID = TREA.ID AND XN.IS_DELETE = 0")
                .Append(" AND (XN.IS_NO_EXECUTE IS NULL OR XN.IS_NO_EXECUTE <> 1)")
                .Append(" AND XN.SERVICE_REQ_TYPE_ID = 2")
                .Append(")) AS IS_HAS_TEST")
                .Append(", (SELECT 1 FROM DUAL WHERE EXISTS (SELECT 1 FROM HIS_SERVICE_REQ CDHA")
                .Append(" WHERE CDHA.TREATMENT_ID = TREA.ID AND CDHA.IS_DELETE = 0")
                .Append(" AND (CDHA.IS_NO_EXECUTE IS NULL OR CDHA.IS_NO_EXECUTE <> 1)")
                .Append(" AND CDHA.SERVICE_REQ_TYPE_ID IN (3,9,10)")
                .Append(")) AS IS_HAS_CDHA")
                .Append(", (SELECT 1 FROM DUAL WHERE EXISTS (SELECT 1 FROM HIS_SERVICE_REQ TDCH")
                .Append(" WHERE TDCH.TREATMENT_ID = TREA.ID AND TDCH.IS_DELETE = 0")
                .Append(" AND (TDCH.IS_NO_EXECUTE IS NULL OR TDCH.IS_NO_EXECUTE <> 1)")
                .Append(" AND TDCH.SERVICE_REQ_TYPE_ID = 5")
                .Append(")) AS IS_HAS_TDCN")
                .Append(" FROM HIS_TREATMENT TREA")
                .Append(" WHERE TREA.IS_PAUSE = 1 AND TREA.TDL_TREATMENT_TYPE_ID = 1")
                .Append(" AND TREA.OUT_DATE =: param1")
                .Append(" AND TREA.END_DEPARTMENT_ID =: param2")
                .Append(" ORDER BY (TREA.OUT_TIME - TREA.IN_TIME)");
                if (param.Limit.HasValue)
                {
                    int start = param.Start.HasValue ? param.Start.Value : 0;
                    int limit = param.Limit.Value;
                    sb.Append(String.Format(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY ", start, limit));
                }
                string sql = sb.ToString();
                List<TreatmentExtWaitTime> datas = DAOWorker.SqlDAO.GetSql<TreatmentExtWaitTime>(sql, filter.OutDate, filter.DepartmentId);
                if (IsNotNullOrEmpty(datas))
                {
                    result = new List<TreatmentTimeDDO>();
                    foreach (var item in datas)
                    {
                        TreatmentTimeDDO ddo = new TreatmentTimeDDO();
                        ddo.Dob = item.TDL_PATIENT_DOB;
                        ddo.GenderName = item.TDL_PATIENT_GENDER_NAME;
                        ddo.id = item.ID;
                        ddo.InTime = item.IN_TIME;
                        ddo.OutTime = item.OUT_TIME;
                        ddo.PatientCode = item.TDL_PATIENT_CODE;
                        ddo.PatientName = item.TDL_PATIENT_NAME;
                        ddo.TreatmentCode = item.TREATMENT_CODE;
                        DateTime dtIn = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(item.IN_TIME).Value;
                        DateTime dtOut = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(item.OUT_TIME.Value).Value;
                        TimeSpan ts = dtOut - dtIn;
                        ddo.WaitTime = (decimal)Math.Round(ts.TotalMinutes, 2, MidpointRounding.AwayFromZero);
                        if (!item.IS_HAS_CDHA.HasValue && !item.IS_HAS_TDCN.HasValue && !item.IS_HAS_TEST.HasValue)
                        {
                            ddo.TypeCode = "A";
                            ddo.TypeName = "a.KHAM";
                        }
                        else if (item.IS_HAS_CDHA.HasValue && !item.IS_HAS_TDCN.HasValue && !item.IS_HAS_TEST.HasValue)
                        {
                            ddo.TypeCode = "B1";
                            ddo.TypeName = "b.KHAM+CDHA";
                        }
                        else if (!item.IS_HAS_CDHA.HasValue && item.IS_HAS_TDCN.HasValue && !item.IS_HAS_TEST.HasValue)
                        {
                            ddo.TypeCode = "B2";
                            ddo.TypeName = "b.KHAM+TDCN";
                        }
                        else if (!item.IS_HAS_CDHA.HasValue && !item.IS_HAS_TDCN.HasValue && item.IS_HAS_TEST.HasValue)
                        {
                            ddo.TypeCode = "B3";
                            ddo.TypeName = "b.KHAM+XN";
                        }
                        else if (item.IS_HAS_CDHA.HasValue && item.IS_HAS_TDCN.HasValue && !item.IS_HAS_TEST.HasValue)
                        {
                            ddo.TypeCode = "C1";
                            ddo.TypeName = "c.KHAM+CDHA+TDCN";
                        }
                        else if (item.IS_HAS_CDHA.HasValue && !item.IS_HAS_TDCN.HasValue && item.IS_HAS_TEST.HasValue)
                        {
                            ddo.TypeCode = "C2";
                            ddo.TypeName = "c.KHAM+CDHA+XN";
                        }
                        else if (!item.IS_HAS_CDHA.HasValue && item.IS_HAS_TDCN.HasValue && item.IS_HAS_TEST.HasValue)
                        {
                            ddo.TypeCode = "C3";
                            ddo.TypeName = "c.KHAM+TDCN+XN";
                        }
                        else if (item.IS_HAS_CDHA.HasValue && item.IS_HAS_TDCN.HasValue && item.IS_HAS_TEST.HasValue)
                        {
                            ddo.TypeCode = "D";
                            ddo.TypeName = "d.KHAM+CDHA+TDCN+XN";
                        }
                        result.Add(ddo);
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        internal List<TreatmentTimeAvgDDO> GetTimeAvg(TreatmentTimeAvgFilter filter)
        {
            List<TreatmentTimeAvgDDO> result = null;
            try
            {
                StringBuilder sb = new StringBuilder()
                .Append("SELECT TREA.ID, TREA.IN_TIME, TREA.OUT_TIME")
                .Append(", (SELECT 1 FROM DUAL WHERE EXISTS (SELECT 1 FROM HIS_SERVICE_REQ XN")
                .Append(" WHERE XN.TREATMENT_ID = TREA.ID AND XN.IS_DELETE = 0")
                .Append(" AND (XN.IS_NO_EXECUTE IS NULL OR XN.IS_NO_EXECUTE <> 1)")
                .Append(" AND XN.SERVICE_REQ_TYPE_ID = 2")
                .Append(")) AS IS_HAS_TEST")
                .Append(", (SELECT 1 FROM DUAL WHERE EXISTS (SELECT 1 FROM HIS_SERVICE_REQ CDHA")
                .Append(" WHERE CDHA.TREATMENT_ID = TREA.ID AND CDHA.IS_DELETE = 0")
                .Append(" AND (CDHA.IS_NO_EXECUTE IS NULL OR CDHA.IS_NO_EXECUTE <> 1)")
                .Append(" AND CDHA.SERVICE_REQ_TYPE_ID IN (3,9,10)")
                .Append(")) AS IS_HAS_CDHA")
                .Append(", (SELECT 1 FROM DUAL WHERE EXISTS (SELECT 1 FROM HIS_SERVICE_REQ TDCH")
                .Append(" WHERE TDCH.TREATMENT_ID = TREA.ID AND TDCH.IS_DELETE = 0")
                .Append(" AND (TDCH.IS_NO_EXECUTE IS NULL OR TDCH.IS_NO_EXECUTE <> 1)")
                .Append(" AND TDCH.SERVICE_REQ_TYPE_ID = 5")
                .Append(")) AS IS_HAS_TDCN")
                .Append(" FROM HIS_TREATMENT TREA")
                .Append(" WHERE TREA.IS_PAUSE = 1")
                .Append(" AND TREA.TDL_TREATMENT_TYPE_ID = 1")
                .Append(" AND TREA.OUT_DATE =: param1")
                .Append(" AND TREA.END_DEPARTMENT_ID =: param2");

                string sql = sb.ToString();

                List<TreatmentExtTimeAvg> datas = DAOWorker.SqlDAO.GetSql<TreatmentExtTimeAvg>(sql, filter.OutDate, filter.DepartmentId);
                if (IsNotNullOrEmpty(datas))
                {
                    result = new List<TreatmentTimeAvgDDO>();
                    TreatmentTimeAvgDDO aKham = this.ProcessAKham(datas);
                    result.Add(aKham);
                    TreatmentTimeAvgDDO bKham_cdha = this.ProcessAKhamCdha(datas);
                    result.Add(bKham_cdha);
                    TreatmentTimeAvgDDO bKham_tdcn = this.ProcessAKhamTdcn(datas);
                    result.Add(bKham_tdcn);
                    TreatmentTimeAvgDDO bKham_Xn = this.ProcessAKhamXn(datas);
                    result.Add(bKham_Xn);
                    TreatmentTimeAvgDDO bKham_cdha_tdcn = this.ProcessAKhamCdhaTdcn(datas);
                    result.Add(bKham_cdha_tdcn);
                    TreatmentTimeAvgDDO bKham_cdha_xn = this.ProcessAKhamCdhaXn(datas);
                    result.Add(bKham_cdha_xn);
                    TreatmentTimeAvgDDO bKham_tdcn_xn = this.ProcessAKhamTdcnXn(datas);
                    result.Add(bKham_tdcn_xn);
                    TreatmentTimeAvgDDO bKham_cdha_tdcn_xn = this.ProcessAKhamCdhaTdcnXn(datas);
                    result.Add(bKham_cdha_tdcn_xn);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

        private TreatmentTimeAvgDDO ProcessAKham(List<TreatmentExtTimeAvg> datas)
        {
            TreatmentTimeAvgDDO ddo = new TreatmentTimeAvgDDO();
            ddo.TypeCode = "A";
            ddo.TypeCode = "a.KHAM";

            List<TreatmentExtTimeAvg> khams = datas.Where(o => !o.IS_HAS_CDHA.HasValue && !o.IS_HAS_TDCN.HasValue && !o.IS_HAS_TEST.HasValue).ToList();
            if (IsNotNullOrEmpty(khams))
            {
                List<decimal> times = new List<decimal>();
                foreach (var item in khams)
                {
                    decimal t = this.GetDiffTime(item);
                    times.Add(t);
                }
                times = times.OrderBy(o => o).ToList();
                ddo.MinTime = times.First();
                ddo.MaxTime = times.Last();
                decimal total = times.Sum(s => s);
                decimal count = (decimal)times.Count;
                ddo.AvgTime = Math.Round((total / count), 0, MidpointRounding.AwayFromZero);
            }

            return ddo;
        }

        private TreatmentTimeAvgDDO ProcessAKhamCdha(List<TreatmentExtTimeAvg> datas)
        {
            TreatmentTimeAvgDDO ddo = new TreatmentTimeAvgDDO();
            ddo.TypeCode = "B1";
            ddo.TypeCode = "b.KHAM+CDHA";

            List<TreatmentExtTimeAvg> kham_cdhas = datas.Where(o => o.IS_HAS_CDHA.HasValue && !o.IS_HAS_TDCN.HasValue && !o.IS_HAS_TEST.HasValue).ToList();
            if (IsNotNullOrEmpty(kham_cdhas))
            {
                List<decimal> times = new List<decimal>();
                foreach (var item in kham_cdhas)
                {
                    decimal t = this.GetDiffTime(item);
                    times.Add(t);
                }
                times = times.OrderBy(o => o).ToList();
                ddo.MinTime = times.First();
                ddo.MaxTime = times.Last();
                decimal total = times.Sum(s => s);
                decimal count = (decimal)times.Count;
                ddo.AvgTime = Math.Round((total / count), 0, MidpointRounding.AwayFromZero);
            }
            return ddo;
        }

        private TreatmentTimeAvgDDO ProcessAKhamTdcn(List<TreatmentExtTimeAvg> datas)
        {
            TreatmentTimeAvgDDO ddo = new TreatmentTimeAvgDDO();
            ddo.TypeCode = "B2";
            ddo.TypeCode = "b.KHAM+TDCN";

            List<TreatmentExtTimeAvg> kham_tdcns = datas.Where(o => !o.IS_HAS_CDHA.HasValue && o.IS_HAS_TDCN.HasValue && !o.IS_HAS_TEST.HasValue).ToList();
            if (IsNotNullOrEmpty(kham_tdcns))
            {
                List<decimal> times = new List<decimal>();
                foreach (var item in kham_tdcns)
                {
                    decimal t = this.GetDiffTime(item);
                    times.Add(t);
                }
                times = times.OrderBy(o => o).ToList();
                ddo.MinTime = times.First();
                ddo.MaxTime = times.Last();
                decimal total = times.Sum(s => s);
                decimal count = (decimal)times.Count;
                ddo.AvgTime = Math.Round((total / count), 0, MidpointRounding.AwayFromZero);
            }
            return ddo;
        }

        private TreatmentTimeAvgDDO ProcessAKhamXn(List<TreatmentExtTimeAvg> datas)
        {
            TreatmentTimeAvgDDO ddo = new TreatmentTimeAvgDDO();
            ddo.TypeCode = "B3";
            ddo.TypeCode = "b.KHAM+XN";

            List<TreatmentExtTimeAvg> kham_xns = datas.Where(o => !o.IS_HAS_CDHA.HasValue && !o.IS_HAS_TDCN.HasValue && o.IS_HAS_TEST.HasValue).ToList();
            if (IsNotNullOrEmpty(kham_xns))
            {
                List<decimal> times = new List<decimal>();
                foreach (var item in kham_xns)
                {
                    decimal t = this.GetDiffTime(item);
                    times.Add(t);
                }
                times = times.OrderBy(o => o).ToList();
                ddo.MinTime = times.First();
                ddo.MaxTime = times.Last();
                decimal total = times.Sum(s => s);
                decimal count = (decimal)times.Count;
                ddo.AvgTime = Math.Round((total / count), 0, MidpointRounding.AwayFromZero);
            }
            return ddo;
        }

        private TreatmentTimeAvgDDO ProcessAKhamCdhaTdcn(List<TreatmentExtTimeAvg> datas)
        {
            TreatmentTimeAvgDDO ddo = new TreatmentTimeAvgDDO();
            ddo.TypeCode = "C1";
            ddo.TypeCode = "C.KHAM+CDHA+TDCN";

            List<TreatmentExtTimeAvg> kham_cdha_tdcns = datas.Where(o => o.IS_HAS_CDHA.HasValue && o.IS_HAS_TDCN.HasValue && !o.IS_HAS_TEST.HasValue).ToList();
            if (IsNotNullOrEmpty(kham_cdha_tdcns))
            {
                List<decimal> times = new List<decimal>();
                foreach (var item in kham_cdha_tdcns)
                {
                    decimal t = this.GetDiffTime(item);
                    times.Add(t);
                }
                times = times.OrderBy(o => o).ToList();
                ddo.MinTime = times.First();
                ddo.MaxTime = times.Last();
                decimal total = times.Sum(s => s);
                decimal count = (decimal)times.Count;
                ddo.AvgTime = Math.Round((total / count), 0, MidpointRounding.AwayFromZero);
            }
            return ddo;
        }

        private TreatmentTimeAvgDDO ProcessAKhamCdhaXn(List<TreatmentExtTimeAvg> datas)
        {
            TreatmentTimeAvgDDO ddo = new TreatmentTimeAvgDDO();
            ddo.TypeCode = "C2";
            ddo.TypeCode = "c.KHAM+CDHA+XN";

            List<TreatmentExtTimeAvg> kham_cdha_xns = datas.Where(o => o.IS_HAS_CDHA.HasValue && !o.IS_HAS_TDCN.HasValue && o.IS_HAS_TEST.HasValue).ToList();
            if (IsNotNullOrEmpty(kham_cdha_xns))
            {
                List<decimal> times = new List<decimal>();
                foreach (var item in kham_cdha_xns)
                {
                    decimal t = this.GetDiffTime(item);
                    times.Add(t);
                }
                times = times.OrderBy(o => o).ToList();
                ddo.MinTime = times.First();
                ddo.MaxTime = times.Last();
                decimal total = times.Sum(s => s);
                decimal count = (decimal)times.Count;
                ddo.AvgTime = Math.Round((total / count), 0, MidpointRounding.AwayFromZero);
            }
            return ddo;
        }

        private TreatmentTimeAvgDDO ProcessAKhamTdcnXn(List<TreatmentExtTimeAvg> datas)
        {
            TreatmentTimeAvgDDO ddo = new TreatmentTimeAvgDDO();
            ddo.TypeCode = "C3";
            ddo.TypeCode = "c.KHAM+TDCN+XN";

            List<TreatmentExtTimeAvg> kham_tdcn_xns = datas.Where(o => !o.IS_HAS_CDHA.HasValue && o.IS_HAS_TDCN.HasValue && o.IS_HAS_TEST.HasValue).ToList();
            if (IsNotNullOrEmpty(kham_tdcn_xns))
            {
                List<decimal> times = new List<decimal>();
                foreach (var item in kham_tdcn_xns)
                {
                    decimal t = this.GetDiffTime(item);
                    times.Add(t);
                }
                times = times.OrderBy(o => o).ToList();
                ddo.MinTime = times.First();
                ddo.MaxTime = times.Last();
                decimal total = times.Sum(s => s);
                decimal count = (decimal)times.Count;
                ddo.AvgTime = Math.Round((total / count), 0, MidpointRounding.AwayFromZero);
            }
            return ddo;
        }

        private TreatmentTimeAvgDDO ProcessAKhamCdhaTdcnXn(List<TreatmentExtTimeAvg> datas)
        {
            TreatmentTimeAvgDDO ddo = new TreatmentTimeAvgDDO();
            ddo.TypeCode = "D";
            ddo.TypeCode = "d.KHAM+CDHA+TDCN+XN";

            List<TreatmentExtTimeAvg> kham_cdha_tdcn_xns = datas.Where(o => o.IS_HAS_CDHA.HasValue && o.IS_HAS_TDCN.HasValue && o.IS_HAS_TEST.HasValue).ToList();
            if (IsNotNullOrEmpty(kham_cdha_tdcn_xns))
            {
                List<decimal> times = new List<decimal>();
                foreach (var item in kham_cdha_tdcn_xns)
                {
                    decimal t = this.GetDiffTime(item);
                    times.Add(t);
                }
                times = times.OrderBy(o => o).ToList();
                ddo.MinTime = times.First();
                ddo.MaxTime = times.Last();
                decimal total = times.Sum(s => s);
                decimal count = (decimal)times.Count;
                ddo.AvgTime = Math.Round((total / count), 0, MidpointRounding.AwayFromZero);
            }
            return ddo;
        }

        private decimal GetDiffTime(TreatmentExtTimeAvg item)
        {
            decimal t = 0;
            DateTime dtIntruc = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(item.IN_TIME).Value;
            DateTime dtFinish = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(item.OUT_TIME.Value).Value;
            TimeSpan ts = dtFinish - dtIntruc;
            t = (decimal)Math.Round(ts.TotalMinutes, 0, MidpointRounding.AwayFromZero);
            return t;
        }

        internal List<TreatmentRegisterByTimeDDO> GetRegisterByTime(TreatmentRegisterByTimeFilter filter)
        {
            List<TreatmentRegisterByTimeDDO> result = null;
            try
            {
                StringBuilder sb = new StringBuilder()
                .Append("SELECT COUNT(*) AS Amount, (TREA.IN_TIME - MOD(TREA.IN_TIME, 10000)) AS InTime")
                .Append(" FROM HIS_TREATMENT TREA")
                .Append(" WHERE TREA.TDL_TREATMENT_TYPE_ID = 1")
                .Append(" AND TREA.IN_DATE =: param1")
                .Append(" AND (SELECT DEPARTMENT_ID FROM HIS_DEPARTMENT_TRAN WHERE TREATMENT_ID = TREA.ID  ORDER BY DEPARTMENT_IN_TIME, ID FETCH FIRST ROWS ONLY) =: param2")
                .Append(" GROUP BY (TREA.IN_TIME - MOD(TREA.IN_TIME, 10000))");

                string sql = sb.ToString();

                List<TreatmentRegisterByTimeDDO> datas = DAOWorker.SqlDAO.GetSql<TreatmentRegisterByTimeDDO>(sql, filter.InDate, filter.DepartmentId);
                if (IsNotNullOrEmpty(datas))
                {
                    result = datas.OrderBy(o => o.InTime).ToList();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

    }

    class TreatmentData
    {
        public long ID { get; set; }
        public string TREATMENT_CODE { get; set; }
        public Nullable<short> IS_PAUSE { get; set; }
        public string ICD_CODE { get; set; }
        public string ICD_NAME { get; set; }
        public string ICD_SUB_CODE { get; set; }
        public string ICD_TEXT { get; set; }
        public Nullable<long> OUT_TIME { get; set; }
        public Nullable<long> END_ROOM_ID { get; set; }
        public Nullable<long> END_DEPARTMENT_ID { get; set; }
        public Nullable<long> OUT_DATE { get; set; }
    }

    class TreatmentExtWaitTime
    {
        public long ID { get; set; }
        public string TREATMENT_CODE { get; set; }
        public long IN_TIME { get; set; }
        public Nullable<long> OUT_TIME { get; set; }
        public string TDL_PATIENT_CODE { get; set; }
        public string TDL_PATIENT_NAME { get; set; }
        public long TDL_PATIENT_DOB { get; set; }
        public Nullable<short> TDL_PATIENT_IS_HAS_NOT_DAY_DOB { get; set; }
        public string TDL_PATIENT_ADDRESS { get; set; }
        public long TDL_PATIENT_GENDER_ID { get; set; }
        public string TDL_PATIENT_GENDER_NAME { get; set; }
        public short? IS_HAS_TEST { get; set; }
        public short? IS_HAS_CDHA { get; set; }
        public short? IS_HAS_TDCN { get; set; }
    }

    class TreatmentExtTimeAvg
    {
        public long ID { get; set; }
        public long IN_TIME { get; set; }
        public Nullable<long> OUT_TIME { get; set; }
        public short? IS_HAS_TEST { get; set; }
        public short? IS_HAS_CDHA { get; set; }
        public short? IS_HAS_TDCN { get; set; }
    }
}
