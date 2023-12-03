namespace Application.UseCases.MedicalHistorys.Queries.GetMedicalHistoryByDocumentNumber;

public record MedicalHistoryByDocumentNumberDto(Guid Id, DateTime Date, string Description, string Diagnosis, string Treatment,
    Guid PatientId, string DocumentoNumber,string FullName);