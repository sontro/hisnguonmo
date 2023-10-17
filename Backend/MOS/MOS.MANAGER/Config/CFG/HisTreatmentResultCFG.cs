using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTreatmentResult;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisTreatmentResultCFG
    {
        private static List<HIS_TREATMENT_RESULT> data;
        public static List<HIS_TREATMENT_RESULT> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisTreatmentResultGet().Get(new HisTreatmentResultFilterQuery());
                }
                return data;
            }
        }

        /// <summary>
        /// ma ket qua dieu tri mac dinh doi voi loai la kham
        /// </summary>
        private const string TREATMENT_RESULT_CODE__DEFAULT_OF_EXAM = "MOS.HIS_TREATMENT_RESULT.TREATMENT_RESULT_CODE.DEFAULT_OF_EXAM";

        private static long treatmentResultIdDefaultOfExam;
        public static long TREATMENT_RESULT_ID__DEFAULT_OF_EXAM
        {
            get
            {
                if (treatmentResultIdDefaultOfExam == 0)
                {
                    treatmentResultIdDefaultOfExam = GetId(TREATMENT_RESULT_CODE__DEFAULT_OF_EXAM);
                }
                return treatmentResultIdDefaultOfExam;
            }
        }

        private static long GetId(string code)
        {
            long result = 0;
            try
            {
                string value = ConfigUtil.GetStrConfig(code);
                var data = new HisTreatmentResult.HisTreatmentResultGet().GetByCode(value);
                if (data == null) throw new ArgumentNullException(code);
                result = data.ID;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = 0;
            }
            return result;
        }

        public static void Reload()
        {
            var idDefaultOfExam = GetId(TREATMENT_RESULT_CODE__DEFAULT_OF_EXAM);

            treatmentResultIdDefaultOfExam = idDefaultOfExam;

            data = null;
        }
    }
}
