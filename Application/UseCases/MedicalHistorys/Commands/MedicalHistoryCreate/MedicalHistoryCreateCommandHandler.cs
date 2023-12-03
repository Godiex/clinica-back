using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Ports;
using Domain.Services;

namespace Application.UseCases.MedicalHistorys.Commands.MedicalHistoryCreate;

public class MedicalHistoryCreateCommandHandler : IRequestHandler<MedicalHistoryCreateCommand>
{
    private readonly MedicalHistoryService _medicalHistoryServices;
    private readonly IGenericRepository<Patient> _patientRepository;

    public MedicalHistoryCreateCommandHandler(MedicalHistoryService medicalHistoryServices,
        IGenericRepository<Patient> patientRepository, IGenericRepository<Doctor> doctorRepository)
    {
        _medicalHistoryServices =
            medicalHistoryServices ?? throw new ArgumentNullException(nameof(medicalHistoryServices));
        _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
    }

    public async Task<Unit> Handle(MedicalHistoryCreateCommand request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetAsync(filter: e => e.DocumentNumber == request.DocumentNumber);

        if (patient == null)
        {
            throw new NotFoundException(Messages.EntityNotFound);
        }

        var medicalHistory = new MedicalHistory
        (
            request.Date,
            request.Description,
            request.Diagnosis,
            request.Treatment,
            patient.FirstOrDefault().Id
        );

        await _medicalHistoryServices.Create(medicalHistory);

        return Unit.Value;
    }
}