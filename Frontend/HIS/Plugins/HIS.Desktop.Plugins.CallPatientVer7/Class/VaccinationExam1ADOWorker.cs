using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.LocalStorage.BackendData.ADO;

namespace HIS.Desktop.LocalStorage.BackendData
{
    class VaccinationExam1ADOWorker
    {
        private static VaccinationExamADO vaccinationExam1ADO;

        public static VaccinationExamADO VaccinationExam1ADO
        {
            get
            {
                if (vaccinationExam1ADO == null)
                {
                    vaccinationExam1ADO = new VaccinationExamADO();
                }
                lock (vaccinationExam1ADO) ;
                return vaccinationExam1ADO;
            }
            set
            {
                lock (vaccinationExam1ADO) ;
                vaccinationExam1ADO = value;
            }
        }
    }
}
