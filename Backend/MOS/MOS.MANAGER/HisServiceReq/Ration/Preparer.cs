using Inventec.Core;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq.Ration
{
    class Preparer
    {
        /// <summary>
        /// Xu ly de dam bao cac suat an co thoi gian, phong xu ly, thoi gian an khac nhau 
        /// thi thuoc cac chi dinh khac nhau
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool PrepareData(HisRationServiceReqSDO data, ref List<RationRequest> rationRequests)
        {
            List<RationRequest> result = new List<RationRequest>();
            const long TwelveOclock = 120000;

            if (data != null && data.InstructionTimes.Count > 0 && data.RationServices != null && data.RationServices.Count > 0)
            {
                //sap xep tu nho den lon
                data.InstructionTimes = data.InstructionTimes.OrderBy(t => t).ToList();

                foreach (long instructionTime in data.InstructionTimes)
                {
                    foreach (RationServiceSDO ss in data.RationServices)
                    {
                        List<RationRequest> rr = ss.RationTimeIds.Select(o => new RationRequest
                        {
                            Amount = ((data.IsForAutoCreateRation && data.HalfInFirstDay) || (instructionTime == data.InstructionTimes[0] && data.InstructionTimes.Count > 1 && data.HalfInFirstDay)) ? ss.Amount / 2 : ss.Amount,
                            IntructionTime = instructionTime,
                            PatientTypeId = ss.PatientTypeId,
                            RationTimeId = o,
                            RoomId = ss.RoomId,
                            ServiceId = ss.ServiceId,
                            InstructionNote = ss.InstructionNote
                        }).ToList();
                        result.AddRange(rr);

                        // Tu dong chi dong xuat an khi la buoi chieu
                        if (data.IsForAutoCreateRation && data.HalfInFirstDay)
                        {
                            // Khoi tao danh sach cac instruction tham can them
                            List<DateTime> instructionAdder = new List<DateTime>();

                            long nowHour = Convert.ToInt64(Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateTime.Now).Value.ToString().Substring(8));
                            Inventec.Common.Logging.LogSystem.Info(Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateTime.Now).Value.ToString());
                            Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(DateTime.Now).Value.ToString());
                            Inventec.Common.Logging.LogSystem.Info(nowHour.ToString());
                            Inventec.Common.Logging.LogSystem.Debug(nowHour.ToString());
                            DateTime instructionDateTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(instructionTime).Value;
                            bool isFriday = instructionDateTime.DayOfWeek == DayOfWeek.Friday;  // Neu gio cua ngay hom nay sau 12h va la thu sau
                            if (nowHour > TwelveOclock && !isFriday)
                            {
                                instructionAdder.Add(instructionDateTime.AddDays(1));
                            }
                            else if (nowHour > TwelveOclock && isFriday)
                            {
                                instructionAdder.AddRange(new List<DateTime>() { instructionDateTime.AddDays(1), instructionDateTime.AddDays(2), instructionDateTime.AddDays(3) });
                            }
                            if (instructionAdder != null && instructionAdder.Count > 0)
                            {
                                foreach (var newInstruction in instructionAdder)
                                {
                                    List<RationRequest> rrAdder = ss.RationTimeIds.Select(o => new RationRequest
                                    {
                                        Amount = ss.Amount,
                                        IntructionTime = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(newInstruction).Value,
                                        PatientTypeId = ss.PatientTypeId,
                                        RationTimeId = o,
                                        RoomId = ss.RoomId,
                                        ServiceId = ss.ServiceId,
                                        InstructionNote = ss.InstructionNote
                                    }).ToList();
                                    result.AddRange(rrAdder);
                                }
                            }
                        }
                    }
                }
            }

            if (result.Count > 0)
            {
                rationRequests = result;
                return true;
            }

            return false;
        }

        public bool PrepareData(List<RationServiceSDO> data, long intructionTime , ref List<RationRequest> rationRequests)
        {
            if (data != null && data.Count > 0)
            {
                List<RationRequest> result = new List<RationRequest>();

                foreach (RationServiceSDO ss in data)
                {
                    List<RationRequest> rr = ss.RationTimeIds.Select(o => new RationRequest
                    {
                        Amount = ss.Amount,
                        IntructionTime = intructionTime,
                        PatientTypeId = ss.PatientTypeId,
                        RationTimeId = o,
                        RoomId = ss.RoomId,
                        ServiceId = ss.ServiceId,
                        InstructionNote = ss.InstructionNote
                    }).ToList();
                    result.AddRange(rr);
                }

                if (result.Count > 0)
                {
                    rationRequests = result;
                    return true;
                }

                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
