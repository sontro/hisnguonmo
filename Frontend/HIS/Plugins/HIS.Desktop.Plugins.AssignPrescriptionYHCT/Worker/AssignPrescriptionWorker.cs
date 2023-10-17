using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPrescriptionYHCT
{
    class AssignPrescriptionWorker : IDisposable
    {
        public MediMatyCreateWorker MediMatyCreateWorker { get; set; }

        public void Dispose()
        {
            MediMatyCreateWorker = null;
        }

        public static AssignPrescriptionWorker Instance { get; private set; }
        public static void CreateInstance()
        {
            if (Instance == null)
                Instance = new AssignPrescriptionWorker();
        }

        static AssignPrescriptionWorker()
        {
            if (Instance == null)
                Instance = new AssignPrescriptionWorker();
        }

        public static void DisposeInstance()
        {
            if (Instance != null)
            {
                Instance.Dispose();
                Instance = null;
            }
        }
    }
}
