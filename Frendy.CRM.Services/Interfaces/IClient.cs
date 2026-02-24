using Frendy.CRM.Shared.Models;
using Frendy.Shared.Dto.RequestDto.AuditRequestDto;
using Frendy.Shared.Dto.ResponseDto.AuditResponseDto;

namespace Frendy.CRM.Services.Interfaces;

public interface IClient
{
    Task<CurrentUserDetails> GetCurrentUserDetailsAsync();
    Task<AuditShortDetails> GetAuditsAsync(GetAuditsRequestDto requestDto);
    Task<AuditDetails?> GetAuditDetailsAsync(Guid auditId);
}