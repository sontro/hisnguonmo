using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Patient
{
    class GlobalConfig
    {
        private static List<MOS.EFMODEL.DataModels.HIS_BLOOD_ABO> hisBloodAbo;
        public static List<MOS.EFMODEL.DataModels.HIS_BLOOD_ABO> hisBloodAbos
        {
            get
            {
                if (hisBloodAbo == null || hisBloodAbo.Count == 0)
                {
                    hisBloodAbo = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get
                        <MOS.EFMODEL.DataModels.HIS_BLOOD_ABO>().OrderByDescending(o => o.CREATE_TIME).ToList();
                }
                return hisBloodAbo;
            }
            set
            {
                hisBloodAbo = value;
            }
        }

        private static List<MOS.EFMODEL.DataModels.HIS_BLOOD_RH> hisBloodRh;
        public static List<MOS.EFMODEL.DataModels.HIS_BLOOD_RH> hisBloodRhs
        {
            get
            {
                if (hisBloodRh == null || hisBloodRh.Count == 0)
                {
                    hisBloodRh = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get
                        <MOS.EFMODEL.DataModels.HIS_BLOOD_RH>().OrderByDescending(o => o.CREATE_TIME).ToList();
                }
                return hisBloodRh;
            }
            set
            {
                hisBloodRh = value;
            }
        }

        private static List<MOS.EFMODEL.DataModels.HIS_BORN_TYPE> bornType;
        public static List<MOS.EFMODEL.DataModels.HIS_BORN_TYPE> bornTypes
        {
            get
            {
                if (bornType == null || bornType.Count == 0)
                {
                    bornType = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get
                        <MOS.EFMODEL.DataModels.HIS_BORN_TYPE>().OrderByDescending(o => o.CREATE_TIME).ToList();
                }
                return bornType;
            }
            set
            {
                bornType = value;
            }
        }

        private static List<MOS.EFMODEL.DataModels.HIS_CAREER> career;
        public static List<MOS.EFMODEL.DataModels.HIS_CAREER> careers
        {
            get
            {
                if (career == null || career.Count == 0)
                {
                    career = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get
                        <MOS.EFMODEL.DataModels.HIS_CAREER>().OrderByDescending(o => o.CREATE_TIME).ToList();
                }
                return career;
            }
            set
            {
                career = value;
            }
        }

        private static List<MOS.EFMODEL.DataModels.HIS_GENDER> gender;
        public static List<MOS.EFMODEL.DataModels.HIS_GENDER> genders
        {
            get
            {
                if (gender == null || gender.Count == 0)
                {
                    gender = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get
                        <MOS.EFMODEL.DataModels.HIS_GENDER>().OrderByDescending(o => o.CREATE_TIME).ToList();
                }
                return gender;
            }
            set
            {
                gender = value;
            }
        }
        
        private static List<MOS.EFMODEL.DataModels.HIS_MILITARY_RANK> militaty;
        public static List<MOS.EFMODEL.DataModels.HIS_MILITARY_RANK> militatys
        {
            get
            {
                
                if (militaty == null || gender.Count == 0)
                {
                    
                    militaty = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get
                        <MOS.EFMODEL.DataModels.HIS_MILITARY_RANK>().OrderByDescending(o => o.CREATE_TIME).ToList();
                }
                return militaty;
            }
            set
            {
                militaty = value;
            }
        }

        private static List<MOS.EFMODEL.DataModels.HIS_WORK_PLACE> workPlace;
        public static List<MOS.EFMODEL.DataModels.HIS_WORK_PLACE> workPlaces
        {
            get
            {
                if (workPlace == null || gender.Count == 0)
                {
                    workPlace = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get
                        <MOS.EFMODEL.DataModels.HIS_WORK_PLACE>().OrderByDescending(o => o.CREATE_TIME).ToList();
                }
                return workPlace;
            }
            set
            {
                workPlace = value;
            }
        }

    }
}
