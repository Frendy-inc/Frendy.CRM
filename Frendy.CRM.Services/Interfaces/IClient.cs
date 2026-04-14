using Frendy.CRM.Shared.Models;
using Frendy.Shared.Dto.RequestDto.AuditRequestDto;
using Frendy.Shared.Dto.RequestDto.StaffRequestDto;
using Frendy.Shared.Dto.RequestDto.UserRequestDto;

namespace Frendy.CRM.Services.Interfaces;

public interface IClient
{
    Task<CurrentUserDetails> GetCurrentUserDetailsAsync();
    Task<AuditShortDetails> GetAuditsAsync(GetAuditsRequestDto requestDto);
    Task<AuditDetails?> GetAuditDetailsAsync(Guid auditId);
    Task<UserShortDetails> GetUsersAsync(GetUsersRequestDto requestDto);
    Task<UserDetails?> GetUserDetailsAsync(Guid userId);
    Task<StaffShortDetails> GetStaffsAsync(GetStaffsRequestDto requestDto);
    Task<StaffDetails?> GetStaffDetailsAsync(Guid staffId);
    Task DemoteStaffAsync(Guid staffId);
}