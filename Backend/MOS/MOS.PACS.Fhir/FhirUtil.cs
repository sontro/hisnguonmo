using Hl7.Fhir.Model;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MOS.PACS.Fhir
{
    class FhirUtil
    {
        static string bundleCFG = ConfigurationManager.AppSettings["MOS.PACS.Fhir.Bundle"] ?? "/Bundle";

        internal static PacsReceivedData ProcessData(Hl7.Fhir.Model.Bundle dataResult, string bundleId = null)
        {
            PacsReceivedData result = null;
            try
            {
                if (dataResult != null && dataResult.Entry != null && dataResult.Entry.Count > 0)
                {
                    //lay Bundle Collection
                    if (dataResult.Type == Bundle.BundleType.Searchset)
                    {
                        for (int i = dataResult.Entry.Count - 1; i >= 0; i--)
                        {
                            if (dataResult.Entry[i].Resource.ResourceType == ResourceType.Bundle)
                            {
                                var bundle = (Hl7.Fhir.Model.Bundle)dataResult.Entry[i].Resource;
                                if (bundle.Type == Bundle.BundleType.Collection)
                                {
                                    return ProcessData(bundle, dataResult.Id);
                                }
                            }
                        }
                    }

                    long sereServId = Convert.ToInt64(dataResult.Identifier.Value);
                    if (sereServId <= 0)
                    {
                        Inventec.Common.Logging.LogSystem.Debug(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => dataResult), dataResult));
                        Inventec.Common.Logging.LogAction.Info(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => dataResult), dataResult));
                        throw new Exception("khong co thong tin Accession number/ sereServId");
                    }

                    string studyID = dataResult.Id ?? bundleId;
                    string captureUsername = "";
                    string resultUsername = "";
                    string resultLoginname = "";
                    DateTime beginTime = DateTime.Now;
                    DateTime endTime = DateTime.Now;
                    DateTime finishTime = DateTime.Now;
                    long? numberOfFilm = null;
                    string conclude = "";
                    string description = "";

                    bool hasResult = false;

                    foreach (var component in dataResult.Entry)
                    {
                        if (component.Resource.ResourceType == Hl7.Fhir.Model.ResourceType.ImagingStudy)
                        {
                            try
                            {
                                Hl7.Fhir.Model.ImagingStudy imagingStudy = (Hl7.Fhir.Model.ImagingStudy)component.Resource;
                                if (imagingStudy != null && imagingStudy.Series != null && imagingStudy.Series.Count > 0)
                                {
                                    numberOfFilm = imagingStudy.Series.First().NumberOfInstances;
                                    if (imagingStudy.Series.First().Performer != null && imagingStudy.Series.First().Performer.Count > 0 && imagingStudy.Series.First().Performer.First().Actor != null)
                                    {
                                        captureUsername = imagingStudy.Series.First().Performer.First().Actor.Display;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Inventec.Common.Logging.LogSystem.Error(ex);
                                Inventec.Common.Logging.LogAction.Error(ex);
                            }
                        }
                        else if (component.Resource.ResourceType == Hl7.Fhir.Model.ResourceType.DiagnosticReport)
                        {
                            try
                            {
                                Hl7.Fhir.Model.DiagnosticReport diagnosticReport = (Hl7.Fhir.Model.DiagnosticReport)component.Resource;
                                if (diagnosticReport != null)
                                {
                                    conclude = diagnosticReport.Conclusion;
                                    if (diagnosticReport.Performer != null && diagnosticReport.Performer.Count > 0)
                                    {
                                        resultUsername = diagnosticReport.Performer.First().Display;
                                    }

                                    if (diagnosticReport.Result != null && diagnosticReport.Result.Count > 0)
                                    {
                                        description = diagnosticReport.Result.First().Display;
                                    }

                                    if (diagnosticReport.Effective != null)
                                    {
                                        if (diagnosticReport.Effective is Hl7.Fhir.Model.Period)
                                        {
                                            var ftime = (Hl7.Fhir.Model.Period)diagnosticReport.Effective;
                                            if (!String.IsNullOrWhiteSpace(ftime.Start))
                                            {
                                                beginTime = DateTime.Parse(ftime.Start);
                                            }

                                            if (!String.IsNullOrWhiteSpace(ftime.End))
                                            {
                                                endTime = DateTime.Parse(ftime.End);
                                            }
                                        }
                                        else if (diagnosticReport.Effective is Hl7.Fhir.Model.FhirDateTime)
                                        {
                                            var ftime = (Hl7.Fhir.Model.FhirDateTime)diagnosticReport.Effective;
                                            finishTime = DateTime.Parse(ftime.Value);
                                        }
                                    }
                                    else if (diagnosticReport.Issued.HasValue)
                                    {
                                        finishTime = diagnosticReport.Issued.Value.DateTime;
                                    }

                                    if (!String.IsNullOrWhiteSpace(conclude) || !String.IsNullOrWhiteSpace(description) ||
                                        endTime != DateTime.Now || finishTime != DateTime.Now)
                                    {
                                        hasResult = true;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Inventec.Common.Logging.LogSystem.Error(ex);
                                Inventec.Common.Logging.LogAction.Error(ex);
                            }
                        }
                        else if (component.Resource.ResourceType == Hl7.Fhir.Model.ResourceType.Practitioner)
                        {
                            try
                            {
                                Hl7.Fhir.Model.Practitioner practitioner = (Hl7.Fhir.Model.Practitioner)component.Resource;
                                if (practitioner != null)
                                {
                                    resultLoginname = practitioner.Id;
                                    HumanName name = null;
                                    if (practitioner.Name != null && practitioner.Name.Count > 0)
                                    {
                                        name = practitioner.Name.First();
                                    }

                                    if (name != null)
                                    {
                                        resultUsername = name.Text;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Inventec.Common.Logging.LogSystem.Error(ex);
                                Inventec.Common.Logging.LogAction.Error(ex);
                            }
                        }
                    }

                    if (hasResult)
                    {
                        result = new PacsReceivedData();
                        result.SereServId = sereServId;
                        result.BeginTime = beginTime < finishTime ? Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(beginTime) : Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(finishTime);
                        result.Conclude = conclude;
                        result.Description = description;
                        result.EndTime = endTime < finishTime ? Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(endTime) : Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(finishTime);
                        result.FinishTime = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(finishTime);
                        result.NumberOfFilm = numberOfFilm;
                        if (!String.IsNullOrWhiteSpace(resultUsername))
                        {
                            result.SubclinicalResultUsername = resultUsername.Split('|').Last();
                        }
                        else if (!String.IsNullOrWhiteSpace(captureUsername))
                        {
                            result.SubclinicalResultUsername = captureUsername.Split('|').Last();
                        }

                        result.SubclinicalResultLoginname = resultLoginname;

                        result.studyID = studyID;
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Error(ex);
                Inventec.Common.Logging.LogAction.Error(ex);
            }
            return result;
        }

        internal static Bundle MakeFhirPACS(HIS_TREATMENT treatment, HIS_SERVICE_REQ serviceReq, V_HIS_ROOM exeRoom, HIS_SERE_SERV data, HIS_SERE_SERV_EXT ext, List<HIS_EMPLOYEE> employees, List<V_HIS_SERVICE> services)
        {
            Bundle result = null;
            try
            {
                result = new Bundle();
                result.Type = Bundle.BundleType.Collection;
                result.Entry = new List<Bundle.EntryComponent>();

                if (ext != null && !String.IsNullOrWhiteSpace(ext.JSON_PRINT_ID))
                {
                    result.Id = ext.JSON_PRINT_ID.Replace("studyID:", "");
                }

                result.Identifier = new Identifier("Accession-number", data.ID + "");

                #region patient
                Patient patient = new Patient();
                patient.Id = treatment.TDL_PATIENT_CODE;
                patient.Name = new List<HumanName>();
                HumanName name = HumanName.ForFamily(treatment.TDL_PATIENT_LAST_NAME).WithGiven(treatment.TDL_PATIENT_FIRST_NAME);
                name.Use = HumanName.NameUse.Official;
                patient.Name.Add(name);
                if (treatment.TDL_PATIENT_GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__FEMALE)
                {
                    patient.Gender = AdministrativeGender.Female;
                }
                else if (treatment.TDL_PATIENT_GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__MALE)
                {
                    patient.Gender = AdministrativeGender.Male;
                }
                else if (treatment.TDL_PATIENT_GENDER_ID == IMSys.DbConfig.HIS_RS.HIS_GENDER.ID__UNKNOWN)
                {
                    patient.Gender = AdministrativeGender.Unknown;
                }
                else
                {
                    patient.Gender = AdministrativeGender.Other;
                }

                DateTime dob = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(treatment.TDL_PATIENT_DOB) ?? DateTime.MinValue;
                if (dob != DateTime.MinValue)
                {
                    patient.BirthDate = dob.ToString("yyyy-MM-dd");
                }

                patient.Identifier = new List<Identifier>();
                patient.Identifier.Add(new Identifier("MA_BN", treatment.TDL_PATIENT_CODE));

                if (String.IsNullOrWhiteSpace(treatment.TDL_HEIN_CARD_NUMBER))
                {
                    patient.Identifier.Add(new Identifier("BHYT", treatment.TDL_HEIN_CARD_NUMBER));
                }

                Patient.ContactComponent contactComponent = new Patient.ContactComponent();
                if (!String.IsNullOrWhiteSpace(treatment.TDL_PATIENT_FATHER_NAME))
                {
                    contactComponent.Name = HumanName.ForFamily(treatment.TDL_PATIENT_FATHER_NAME);
                    contactComponent.Relationship = new List<CodeableConcept> { new CodeableConcept(".","N","Cha") };
                }
                else if (!String.IsNullOrWhiteSpace(treatment.TDL_PATIENT_MOTHER_NAME))
                {
                    contactComponent.Name = HumanName.ForFamily(treatment.TDL_PATIENT_MOTHER_NAME);
                    contactComponent.Relationship = new List<CodeableConcept> { new CodeableConcept(".", "N", "Mẹ") };
                }
                else if (!String.IsNullOrWhiteSpace(treatment.TDL_PATIENT_RELATIVE_NAME))
                {
                    contactComponent.Name = HumanName.ForFamily(treatment.TDL_PATIENT_RELATIVE_NAME);
                    contactComponent.Relationship = new List<CodeableConcept> { new CodeableConcept(".", "N", treatment.TDL_PATIENT_RELATIVE_TYPE) };
                }

                patient.Contact = new List<Patient.ContactComponent> { contactComponent };

                patient.Deceased = new FhirBoolean(false);
                patient.Address = new List<Address> { new Address { Text = treatment.TDL_PATIENT_ADDRESS, Use = Address.AddressUse.Home } };
                patient.Telecom = new List<ContactPoint> { new ContactPoint { Value = treatment.TDL_PATIENT_PHONE, System = ContactPoint.ContactPointSystem.Phone, Use = ContactPoint.ContactPointUse.Mobile } };
                result.Entry.Add(new Bundle.EntryComponent { Resource = patient, FullUrl = "Patient/" + patient.Id });
                #endregion

                #region encounter
                Encounter encounter = new Encounter();
                encounter.Id = treatment.TREATMENT_CODE;
                encounter.Identifier = new List<Identifier> { new Identifier { Value = treatment.TREATMENT_CODE } };
                //EMER : cấp cứu
                //AMB : nội trú
                //IMP : ngoại trú
                if (serviceReq.IS_EMERGENCY == MOS.UTILITY.Constant.IS_TRUE)
                {
                    encounter.Class = new Coding("http://terminology.hl7.org/CodeSystem/v3-ActCode", "EMER", "Cấp cứu");
                }
                else if (treatment.TDL_TREATMENT_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TREATMENT_TYPE.ID__DTNOITRU)
                {
                    encounter.Class = new Coding("http://terminology.hl7.org/CodeSystem/v3-ActCode", "AMB", "Nội trú");
                }
                else
                {
                    encounter.Class = new Coding("http://terminology.hl7.org/CodeSystem/v3-ActCode", "IMP", "Khám/ngoại trú");
                }

                encounter.Location = new List<Encounter.LocationComponent> { new Encounter.LocationComponent { Location = new ResourceReference() { Reference = exeRoom.ROOM_NAME } } };

                DateTime beginTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(treatment.IN_TIME) ?? DateTime.Now;
                encounter.Period = new Period { Start = beginTime.ToString(FhirDateTime.FMT_FULL) };
                encounter.Status = Encounter.EncounterStatus.InProgress;

                result.Entry.Add(new Bundle.EntryComponent { Resource = encounter, FullUrl = "Encounter/" + encounter.Id });
                #endregion

                #region serviceRequest
                ServiceRequest serviceRequest = new ServiceRequest();
                serviceRequest.Id = data.ID + "";
                serviceRequest.Status = RequestStatus.Active;
                serviceRequest.Intent = RequestIntent.Order;
                serviceRequest.Priority = RequestPriority.Routine;
                if (serviceReq.IS_EMERGENCY == MOS.UTILITY.Constant.IS_TRUE)
                {
                    serviceRequest.Priority = RequestPriority.Urgent;
                }

                serviceRequest.Code = new CodeableConcept { Coding = new List<Coding> { new Coding("local", data.TDL_SERVICE_CODE) }, Text = data.TDL_SERVICE_NAME };
                serviceRequest.Subject = new ResourceReference { Reference = "Patient/" + patient.Id };
                DateTime intructionTime = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(serviceReq.INTRUCTION_TIME) ?? DateTime.Now;
                serviceRequest.AuthoredOn = intructionTime.ToString(FhirDateTime.FMT_FULL);
                HIS_EMPLOYEE employee = employees != null && employees.Count > 0 ? employees.FirstOrDefault(o => o.LOGINNAME == serviceReq.REQUEST_LOGINNAME) : null;
                serviceRequest.Requester = new ResourceReference { Reference = "Practitioner/" + serviceReq.REQUEST_LOGINNAME, Display = serviceReq.REQUEST_USERNAME, Identifier = new Identifier { System = "CCHN", Value = employee != null ? employee.DIPLOMA : "" } };
                serviceRequest.ReasonCode = new List<CodeableConcept> { new CodeableConcept { Coding = new List<Coding> { new Coding("http://hl7.org/fhir/sid/icd-10", serviceReq.ICD_CODE) }, Text = serviceReq.ICD_NAME } };
                serviceRequest.Requisition = new Identifier { Value = serviceReq.SERVICE_REQ_CODE };

                result.Entry.Add(new Bundle.EntryComponent { Resource = serviceRequest, FullUrl = "ServiceRequest/" + serviceRequest.Id });
                #endregion

                if (ext != null && CheckDataExt(ext))
                {
                    #region imagingStudy
                    ImagingStudy imagingStudy = new ImagingStudy();
                    imagingStudy.Id = data.ID + "";
                    imagingStudy.Subject = new ResourceReference { Reference = "Patient/" + patient.Id };
                    imagingStudy.NumberOfSeries = 1;
                    imagingStudy.Series = new List<ImagingStudy.SeriesComponent>();

                    ImagingStudy.SeriesComponent series = new ImagingStudy.SeriesComponent();

                    V_HIS_SERVICE service = services != null && services.Count > 0 ? services.FirstOrDefault(o => o.ID == data.SERVICE_ID) : null;

                    if (!String.IsNullOrWhiteSpace(service.PACS_TYPE_CODE))
                    {
                        imagingStudy.Modality = new List<Coding> { new Coding("http://dicom.nema.org/resources/ontology/DCM", service.PACS_TYPE_CODE) };
                    }
                    else if (service.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__SA)
                    {
                        series.Modality = new Coding("http://dicom.nema.org/resources/ontology/DCM", "US");
                    }
                    else if (service.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__CDHA)
                    {
                        if (service.DIIM_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_DIIM_TYPE.ID__CT)
                        {
                            series.Modality = new Coding("http://dicom.nema.org/resources/ontology/DCM", "CT");
                        }
                        else if (service.DIIM_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_DIIM_TYPE.ID__MRI)
                        {
                            series.Modality = new Coding("http://dicom.nema.org/resources/ontology/DCM", "MRI");
                        }
                        else if (service.DIIM_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_DIIM_TYPE.ID__XQ)
                        {
                            series.Modality = new Coding("http://dicom.nema.org/resources/ontology/DCM", "DX");
                        }
                        else
                        {
                            series.Modality = new Coding("http://dicom.nema.org/resources/ontology/DCM", "CR");
                        }
                    }
                    else if (service.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__NS)
                    {
                        series.Modality = new Coding("http://dicom.nema.org/resources/ontology/DCM", "ES");
                    }
                    else
                    {
                        series.Modality = new Coding("http://dicom.nema.org/resources/ontology/DCM", "MG");
                    }

                    series.NumberOfInstances = (int)(ext.NUMBER_OF_FILM ?? 1);
                    series.Performer = new List<ImagingStudy.PerformerComponent>();

                    if (!String.IsNullOrWhiteSpace(ext.SUBCLINICAL_RESULT_LOGINNAME))
                    {
                        series.Performer.Add(new ImagingStudy.PerformerComponent { Function = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v3-ParticipationType", "SPRF"), Actor = new ResourceReference { Display = ext.SUBCLINICAL_RESULT_LOGINNAME + "|" + ext.SUBCLINICAL_RESULT_USERNAME } });
                    }
                    else if (!String.IsNullOrWhiteSpace(serviceReq.EXECUTE_LOGINNAME))
                    {
                        series.Performer.Add(new ImagingStudy.PerformerComponent { Function = new CodeableConcept("http://terminology.hl7.org/CodeSystem/v3-ParticipationType", "SPRF"), Actor = new ResourceReference { Display = serviceReq.EXECUTE_LOGINNAME + "|" + serviceReq.EXECUTE_USERNAME } });
                    }

                    imagingStudy.Series.Add(series);

                    result.Entry.Add(new Bundle.EntryComponent { Resource = imagingStudy, Request = new Bundle.RequestComponent { Method = Bundle.HTTPVerb.POST, Url = "ImagingStudy/" + imagingStudy.Id } });
                    #endregion

                    #region Media
                    Media media = new Media();
                    media.Id = data.ID + "";
                    media.Subject = new ResourceReference { Reference = "Patient/" + patient.Id };
                    media.DeviceName = "https:\\\\pacs.vn/AutoSearch/AutoSearch?accessionno=";
                    media.Content = new Attachment { ContentType = "application/xml" };
                    result.Entry.Add(new Bundle.EntryComponent { Resource = media, Request = new Bundle.RequestComponent { Method = Bundle.HTTPVerb.POST, Url = "Media/" + media.Id } });
                    #endregion

                    #region diagnosticReport
                    DiagnosticReport diagnosticReport = new DiagnosticReport();
                    diagnosticReport.Text = new Narrative { Status = Narrative.NarrativeStatus.Empty };
                    diagnosticReport.Identifier = new List<Identifier> { new Identifier { Value = data.ID + "" } };
                    diagnosticReport.Status = DiagnosticReport.DiagnosticReportStatus.Final;
                    diagnosticReport.Subject = new ResourceReference { Reference = "Patient/" + patient.Id };

                    long finishTime = ext.END_TIME.HasValue ? ext.END_TIME.Value : (serviceReq.FINISH_TIME ?? 0);
                    DateTime ftime = finishTime > 0 ? Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(finishTime) ?? DateTime.Now : DateTime.Now;

                    long beginExtTime = ext.BEGIN_TIME.HasValue ? ext.BEGIN_TIME.Value : (serviceReq.START_TIME ?? 0);
                    DateTime btime = beginExtTime > 0 ? Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(beginExtTime) ?? DateTime.Now : DateTime.Now;

                    Hl7.Fhir.Model.Period executeTimeExt = new Period();
                    executeTimeExt.Start = btime.ToString(FhirDateTime.FMT_FULL);
                    executeTimeExt.End = ftime.ToString(FhirDateTime.FMT_FULL);

                    diagnosticReport.Effective = executeTimeExt;

                    diagnosticReport.Performer = new List<ResourceReference>();

                    ResourceReference diagnosticReportPerformer = new ResourceReference();
                    diagnosticReportPerformer.Reference = "Practitioner/";

                    if (!String.IsNullOrWhiteSpace(ext.SUBCLINICAL_RESULT_LOGINNAME))
                    {
                        diagnosticReportPerformer.Display = ext.SUBCLINICAL_RESULT_LOGINNAME + "|" + ext.SUBCLINICAL_RESULT_USERNAME;
                        HIS_EMPLOYEE extEmployee = employees != null && employees.Count > 0 ? employees.FirstOrDefault(o => o.LOGINNAME == ext.SUBCLINICAL_RESULT_LOGINNAME) : null;
                        diagnosticReportPerformer.Identifier = new Identifier("CCHN", extEmployee != null ? extEmployee.DIPLOMA : "");
                    }
                    else if (!String.IsNullOrWhiteSpace(serviceReq.EXECUTE_LOGINNAME))
                    {
                        diagnosticReportPerformer.Display = serviceReq.EXECUTE_LOGINNAME + "|" + serviceReq.EXECUTE_USERNAME;
                        HIS_EMPLOYEE extEmployee = employees != null && employees.Count > 0 ? employees.FirstOrDefault(o => o.LOGINNAME == serviceReq.EXECUTE_LOGINNAME) : null;
                        diagnosticReportPerformer.Identifier = new Identifier("CCHN", extEmployee != null ? extEmployee.DIPLOMA : "");
                    }

                    diagnosticReport.Performer.Add(diagnosticReportPerformer);
                    diagnosticReport.Conclusion = ext.CONCLUDE;
                    diagnosticReport.Result = new List<ResourceReference> { new ResourceReference { Display = ext.DESCRIPTION } };
                    diagnosticReport.ImagingStudy = new List<ResourceReference> { new ResourceReference { Reference = "ImagingStudy/" + imagingStudy.Id } };
                    diagnosticReport.Media = new List<DiagnosticReport.MediaComponent> { new DiagnosticReport.MediaComponent { Comment = "", Link = new ResourceReference { Reference = "Media/" + media.Id } } };

                    result.Entry.Add(new Bundle.EntryComponent { Resource = diagnosticReport, Request = new Bundle.RequestComponent { Method = Bundle.HTTPVerb.POST, Url = "DiagnosticReport" + data.ID } });
                    #endregion
                }
                else
                {
                    #region imagingStudy
                    ImagingStudy imagingStudy = new ImagingStudy();
                    imagingStudy.Id = data.ID + "";
                    imagingStudy.Status = ImagingStudy.ImagingStudyStatus.Unknown;
                    imagingStudy.Subject = new ResourceReference { Reference = "Patient/" + patient.Id };
                    imagingStudy.BasedOn = new List<ResourceReference> { new ResourceReference { Reference = "ServiceRequest/" + serviceRequest.Id } };
                    imagingStudy.Identifier = new List<Identifier> { new Identifier("Accession-number", data.ID + "") };

                    V_HIS_SERVICE service = services != null && services.Count > 0 ? services.FirstOrDefault(o => o.ID == data.SERVICE_ID) : null;
                    if (service != null)
                    {
                        if (!String.IsNullOrWhiteSpace(service.PACS_TYPE_CODE))
                        {
                            imagingStudy.Modality = new List<Coding> { new Coding("http://dicom.nema.org/resources/ontology/DCM", service.PACS_TYPE_CODE) };
                        }
                        else if (service.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__SA)
                        {
                            imagingStudy.Modality = new List<Coding> { new Coding("http://dicom.nema.org/resources/ontology/DCM", "US") };
                        }
                        else if (service.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__CDHA)
                        {
                            if (service.DIIM_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_DIIM_TYPE.ID__CT)
                            {
                                imagingStudy.Modality = new List<Coding> { new Coding("http://dicom.nema.org/resources/ontology/DCM", "CT") };
                            }
                            else if (service.DIIM_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_DIIM_TYPE.ID__MRI)
                            {
                                imagingStudy.Modality = new List<Coding> { new Coding("http://dicom.nema.org/resources/ontology/DCM", "MRI") };
                            }
                            else if (service.DIIM_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_DIIM_TYPE.ID__XQ)
                            {
                                imagingStudy.Modality = new List<Coding> { new Coding("http://dicom.nema.org/resources/ontology/DCM", "DX") };
                            }
                            else
                            {
                                imagingStudy.Modality = new List<Coding> { new Coding("http://dicom.nema.org/resources/ontology/DCM", "CR") };
                            }
                        }
                        else if (service.SERVICE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__NS)
                        {
                            imagingStudy.Modality = new List<Coding> { new Coding("http://dicom.nema.org/resources/ontology/DCM", "ES") };
                        }
                        else
                        {
                            imagingStudy.Modality = new List<Coding> { new Coding("http://dicom.nema.org/resources/ontology/DCM", "MG") };
                        }
                    }
                    else
                    {
                        imagingStudy.Modality = new List<Coding> { new Coding("http://dicom.nema.org/resources/ontology/DCM", "MG") };
                    }

                    result.Entry.Add(new Bundle.EntryComponent { Resource = imagingStudy, FullUrl = "ImagingStudy/" + imagingStudy.Id });
                    #endregion
                }

                #region Practitioner
                Practitioner practitioner = new Practitioner();
                practitioner.Id = serviceReq.REQUEST_LOGINNAME;
                practitioner.Identifier = new List<Identifier> { new Identifier { System = "CCHN", Value = employee != null ? employee.DIPLOMA : "" } };
                practitioner.Name = new List<HumanName> { new HumanName { Use = HumanName.NameUse.Official, Text = serviceReq.REQUEST_USERNAME } };
                result.Entry.Add(new Bundle.EntryComponent { Resource = practitioner, FullUrl = "Practitioner/" + practitioner.Id });
                #endregion
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                Inventec.Common.Logging.LogAction.Error(ex);
            }
            return result;
        }

        internal static bool SendDeleteFhir(string uri, string loginname, string password, string bundleId)
        {
            bool result = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(uri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + CreateAuthenString(loginname, password));

                    string extension = uri.Substring(uri.IndexOf('/', uri.IndexOf("//") + 2));
                    string url = extension + bundleCFG + "/" + bundleId;
                    string rs = "";

                    Inventec.Common.Logging.LogSystem.Info("____DeleteAsync_____url : " + url);
                    HttpResponseMessage response = null;
                    response = client.DeleteAsync(url).Result;

                    try
                    {
                        if (response == null)
                        {
                            throw new Exception(string.Format("Loi khi goi API: {0}. Du lieu tra ve null", uri));
                        }
                        else if (!response.IsSuccessStatusCode)
                        {
                            int statusCode = response.StatusCode.GetHashCode();
                            throw new Exception(string.Format("Loi khi goi API: {0}. StatusCode: {1}", uri, statusCode));
                        }
                        else
                        {
                            rs = response.Content.ReadAsStringAsync().Result;
                        }
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData("rs", rs), ex);
                        Inventec.Common.Logging.LogAction.Error(Inventec.Common.Logging.LogUtil.TraceData("rs", rs), ex);
                    }

                    if (!String.IsNullOrWhiteSpace(rs))
                    {
                        Inventec.Common.Logging.LogSystem.Debug("_______rs: " + rs);
                        Inventec.Common.Logging.LogAction.Info("_______rs: " + rs);
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
                Inventec.Common.Logging.LogAction.Error(ex);
            }
            return result;
        }

        internal static bool SendFhir(string uri, string loginname, string password, Bundle dataSend, ref string id)
        {
            bool result = false;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(uri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + CreateAuthenString(loginname, password));

                    string extension = uri.Substring(uri.IndexOf('/', uri.IndexOf("//") + 2));
                    string url = extension + bundleCFG + "";
                    string rs = "";

                    var serial = new Hl7.Fhir.Serialization.FhirJsonSerializer();
                    string sendJsonData = serial.SerializeToString(dataSend);
                    Inventec.Common.Logging.LogSystem.Debug("_____sendJsonData : " + sendJsonData);
                    Inventec.Common.Logging.LogAction.Info("_____sendJsonData : " + sendJsonData);
                    HttpResponseMessage response = null;
                    if (!string.IsNullOrWhiteSpace(dataSend.Id))
                    {
                        Inventec.Common.Logging.LogSystem.Debug("_________PutAsync");
                        Inventec.Common.Logging.LogAction.Info("_________PutAsync");
                        response = client.PutAsync(url + "/" + dataSend.Id, new StringContent(sendJsonData, Encoding.UTF8, "application/json")).Result;
                    }
                    else
                    {
                        Inventec.Common.Logging.LogSystem.Debug("_______PostAsync");
                        Inventec.Common.Logging.LogAction.Info("_______PostAsync");
                        response = client.PostAsync(url, new StringContent(sendJsonData, Encoding.UTF8, "application/json")).Result;
                    }

                    try
                    {
                        if (response == null)
                        {
                            throw new Exception(string.Format("Loi khi goi API: {0}. Du lieu tra ve null", uri));
                        }
                        else if (!response.IsSuccessStatusCode)
                        {
                            int statusCode = response.StatusCode.GetHashCode();
                            if (statusCode == 400 && string.IsNullOrWhiteSpace(dataSend.Id))
                            {
                                //put loi thi post lai data de tao
                                HttpResponseMessage postResponse = client.PostAsync(url, new StringContent(sendJsonData, Encoding.UTF8, "application/json")).Result;
                                if (postResponse == null || !postResponse.IsSuccessStatusCode)
                                {
                                    int postStatusCode = postResponse.StatusCode.GetHashCode();
                                    throw new Exception(string.Format("Loi khi goi API: {0}. StatusCode: {1}", uri, postStatusCode));
                                }
                                else
                                {
                                    rs = postResponse.Content.ReadAsStringAsync().Result;
                                }
                            }
                        }
                        else
                        {
                            rs = response.Content.ReadAsStringAsync().Result;
                        }
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData("rs", rs), ex);
                        Inventec.Common.Logging.LogAction.Error(Inventec.Common.Logging.LogUtil.TraceData("rs", rs), ex);
                    }

                    if (!String.IsNullOrWhiteSpace(rs))
                    {
                        var parser = new Hl7.Fhir.Serialization.FhirJsonParser();
                        Bundle dataResult = parser.Parse<Hl7.Fhir.Model.Bundle>(rs);
                        id = dataResult.Id;
                        result = true;
                    }

                    Inventec.Common.Logging.LogSystem.Debug("_______rs: " + rs);
                    Inventec.Common.Logging.LogAction.Info("_______rs: " + rs);
                }
            }
            catch (Exception ex)
            {
                result = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
                Inventec.Common.Logging.LogAction.Error(ex);
            }
            return result;
        }

        internal static Hl7.Fhir.Model.Bundle GetResult(string uri, string loginname, string password, long sereServId)
        {
            Hl7.Fhir.Model.Bundle result = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(uri);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + CreateAuthenString(loginname, password));

                    string extension = uri.Substring(uri.IndexOf('/', uri.IndexOf("//") + 2));

                    string url = extension + bundleCFG + "?identifier=" + sereServId;
                    string rs = "";
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    try
                    {
                        if (response == null || !response.IsSuccessStatusCode)
                        {
                            int statusCode = response.StatusCode.GetHashCode();
                            throw new Exception(string.Format("Loi khi goi API: {0}. StatusCode: {1}", uri, statusCode));
                        }
                        else
                        {
                            rs = response.Content.ReadAsStringAsync().Result;
                            var parser = new Hl7.Fhir.Serialization.FhirJsonParser();
                            Inventec.Common.Logging.LogSystem.Debug("___________Bundle result: " + rs);
                            Inventec.Common.Logging.LogAction.Info("___________Bundle result: " + rs);
                            result = parser.Parse<Hl7.Fhir.Model.Bundle>(rs);
                        }
                    }
                    catch (Exception ex)
                    {
                        Inventec.Common.Logging.LogSystem.Error(Inventec.Common.Logging.LogUtil.TraceData("rs", rs), ex);
                        Inventec.Common.Logging.LogAction.Error(Inventec.Common.Logging.LogUtil.TraceData("rs", rs), ex);
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Error(ex);
                Inventec.Common.Logging.LogAction.Error(ex);
            }
            return result;
        }

        private static string CreateAuthenString(string loginname, string password)
        {
            if (string.IsNullOrWhiteSpace(loginname) || string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("loginname, password khong duoc de trong");
            }

            string authenString = string.Format("{0}:{1}", loginname, password);

            return Convert.ToBase64String(Encoding.Default.GetBytes(authenString));
        }

        private static bool CheckDataExt(HIS_SERE_SERV_EXT ext)
        {
            try
            {
                if (ext != null)
                {
                    if (!String.IsNullOrWhiteSpace(ext.CONCLUDE) ||
                        !String.IsNullOrWhiteSpace(ext.DESCRIPTION) ||
                        !String.IsNullOrWhiteSpace(ext.SUBCLINICAL_RESULT_LOGINNAME))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                Inventec.Common.Logging.LogAction.Error(ex);
            }
            return false;
        }
    }
}
