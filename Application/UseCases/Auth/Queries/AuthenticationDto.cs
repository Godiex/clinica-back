using Domain.Enums;

namespace Application.UseCases.Auth.Queries;

public record AuthenticationDto(Guid userId, string DocumentNumber, Role role);