using MOS.LibraryHein.Bhyt;
using MOS.LibraryHein.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LibraryHein.Factory
{
    public class HeinProcessor
    {
        private IHeinProcessor processor;

        public HeinProcessor(string libCode)
        {
            this.processor = HeinProcessorFactory.GetHeinProcessor(libCode);
        }

        public bool IsValidHeinCardNumber(string heinCardNumber)
        {
            return this.processor != null && this.processor.IsValidHeinCardNumber(heinCardNumber);
        }

        public bool UpdateHeinInfo(string treatmentTypeCode, string heinCardNumber, List<RequestServiceData> requestServiceData)
        {
            return this.processor != null && this.processor.UpdateHeinInfo(treatmentTypeCode, heinCardNumber, requestServiceData);
        }

        public decimal? GetHeinRatio(string treatmentTypeCode, string heinCardNumber, string jsonPatientTypeData, string jsonServiceTypeData)
        {
            return this.processor != null ? this.processor.GetHeinRatio(treatmentTypeCode, heinCardNumber, jsonPatientTypeData, jsonServiceTypeData) : 0;
        }
    }
}
