using Drmc.Procurement.Application.Features.APP.DTO;
using Drmc.Procurement.Application.Features.APP.Interfaces;
using Drmc.Procurement.Application.Features.Auth.DTO;
using Drmc.Procurement.Application.Features.Auth.Interfaces;
using Drmc.Procurement.Application.Features.DepartmentSections.DTO;
using Drmc.Procurement.Application.Features.DepartmentSections.Interfaces;
using Drmc.Procurement.Application.Features.Departments.DTO;
using Drmc.Procurement.Application.Features.Departments.Interfaces;
using Drmc.Procurement.Application.Features.Items.DTO;
using Drmc.Procurement.Application.Features.Items.Interfaces;
using Drmc.Procurement.Application.Features.PlanningYearCategories.DTO;
using Drmc.Procurement.Application.Features.PlanningYearCategories.Interfaces;
using Drmc.Procurement.Application.Features.PlanningYearItems.DTO;
using Drmc.Procurement.Application.Features.PlanningYearItems.Interfaces;
using Drmc.Procurement.Application.Features.PlanningYears.DTO;
using Drmc.Procurement.Application.Features.PlanningYears.Interfaces;
using Drmc.Procurement.Application.Features.ProcurementCategories.DTO;
using Drmc.Procurement.Application.Features.ProcurementCategories.Interfaces;
using Drmc.Procurement.Application.Features.Roles.DTO;
using Drmc.Procurement.Application.Features.Roles.Interfaces;
using Drmc.Procurement.Application.Features.UserCategoryPreferences.DTO;
using Drmc.Procurement.Application.Features.UserCategoryPreferences.Interfaces;
using Drmc.Procurement.Application.Features.UserRoles.DTO;
using Drmc.Procurement.Application.Features.UserRoles.Interfaces;
using Drmc.Procurement.Application.Features.Users.DTO;
using Drmc.Procurement.Application.Features.Users.Interfaces;
using MediatR;

namespace Drmc.Procurement.Application.Common.Cqrs;

public sealed record AuthenticateCommand(LoginRequest Request)
    : IRequest<UserDto?>;

public sealed class AuthenticateCommandHandler
    : IRequestHandler<AuthenticateCommand, UserDto?>
{
    private readonly IAuthService _service;
    public AuthenticateCommandHandler(IAuthService service) => _service = service;
    public Task<UserDto?> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        => _service.AuthenticateAsync(request.Request);
}

public sealed record GetItemsQuery : IRequest<List<ItemDto>>;
public sealed class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, List<ItemDto>>
{
    private readonly IItemService _service;
    public GetItemsQueryHandler(IItemService service) => _service = service;
    public Task<List<ItemDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        => _service.GetAllAsync();
}

public sealed record GetItemsByCategoryQuery(int CategoryId) : IRequest<List<ItemDto>>;
public sealed class GetItemsByCategoryQueryHandler : IRequestHandler<GetItemsByCategoryQuery, List<ItemDto>>
{
    private readonly IItemService _service;
    public GetItemsByCategoryQueryHandler(IItemService service) => _service = service;
    public Task<List<ItemDto>> Handle(GetItemsByCategoryQuery request, CancellationToken cancellationToken)
        => _service.GetByCategoryIdAsync(request.CategoryId);
}

public sealed record GetItemQuery(int Id) : IRequest<ItemDto?>;
public sealed class GetItemQueryHandler : IRequestHandler<GetItemQuery, ItemDto?>
{
    private readonly IItemService _service;
    public GetItemQueryHandler(IItemService service) => _service = service;
    public Task<ItemDto?> Handle(GetItemQuery request, CancellationToken cancellationToken)
        => _service.GetByIdAsync(request.Id);
}

public sealed record GetPlanningYearCatalogueQuery(int PlanningYear, int CategoryId)
    : IRequest<List<PlanningYearCatalogueItemDto>>;
public sealed class GetPlanningYearCatalogueQueryHandler
    : IRequestHandler<GetPlanningYearCatalogueQuery, List<PlanningYearCatalogueItemDto>>
{
    private readonly IItemService _service;
    public GetPlanningYearCatalogueQueryHandler(IItemService service) => _service = service;
    public Task<List<PlanningYearCatalogueItemDto>> Handle(GetPlanningYearCatalogueQuery request, CancellationToken cancellationToken)
        => _service.GetByPlanningYearAndCategoryAsync(request.PlanningYear, request.CategoryId);
}

public sealed record CreateItemCommand(CreateItemRequest Request) : IRequest<ItemDto>;
public sealed class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, ItemDto>
{
    private readonly IItemService _service;
    public CreateItemCommandHandler(IItemService service) => _service = service;
    public Task<ItemDto> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        => _service.CreateAsync(request.Request);
}

public sealed record UpdateItemCommand(int Id, UpdateItemRequest Request) : IRequest<ItemDto?>;
public sealed class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, ItemDto?>
{
    private readonly IItemService _service;
    public UpdateItemCommandHandler(IItemService service) => _service = service;
    public Task<ItemDto?> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        => _service.UpdateAsync(request.Id, request.Request);
}

public sealed record SetItemActiveStatusCommand(int Id, bool IsActive) : IRequest<bool>;
public sealed class SetItemActiveStatusCommandHandler : IRequestHandler<SetItemActiveStatusCommand, bool>
{
    private readonly IItemService _service;
    public SetItemActiveStatusCommandHandler(IItemService service) => _service = service;
    public Task<bool> Handle(SetItemActiveStatusCommand request, CancellationToken cancellationToken)
        => _service.SetActiveStatusAsync(request.Id, request.IsActive);
}

public sealed record GetPlanningYearCategoriesQuery : IRequest<List<PlanningYearCategoryDto>>;
public sealed class GetPlanningYearCategoriesQueryHandler : IRequestHandler<GetPlanningYearCategoriesQuery, List<PlanningYearCategoryDto>>
{
    private readonly IPlanningYearCategoryService _service;
    public GetPlanningYearCategoriesQueryHandler(IPlanningYearCategoryService service) => _service = service;
    public Task<List<PlanningYearCategoryDto>> Handle(GetPlanningYearCategoriesQuery request, CancellationToken cancellationToken)
        => _service.GetAllAsync();
}

public sealed record GetPlanningYearCategoriesByYearQuery(int PlanningYearId)
    : IRequest<List<PlanningYearCategoryDto>>;
public sealed class GetPlanningYearCategoriesByYearQueryHandler
    : IRequestHandler<GetPlanningYearCategoriesByYearQuery, List<PlanningYearCategoryDto>>
{
    private readonly IPlanningYearCategoryService _service;
    public GetPlanningYearCategoriesByYearQueryHandler(IPlanningYearCategoryService service) => _service = service;
    public Task<List<PlanningYearCategoryDto>> Handle(GetPlanningYearCategoriesByYearQuery request, CancellationToken cancellationToken)
        => _service.GetByPlanningYearIdAsync(request.PlanningYearId);
}

public sealed record GetPlanningYearCategoryQuery(int Id) : IRequest<PlanningYearCategoryDto?>;
public sealed class GetPlanningYearCategoryQueryHandler : IRequestHandler<GetPlanningYearCategoryQuery, PlanningYearCategoryDto?>
{
    private readonly IPlanningYearCategoryService _service;
    public GetPlanningYearCategoryQueryHandler(IPlanningYearCategoryService service) => _service = service;
    public Task<PlanningYearCategoryDto?> Handle(GetPlanningYearCategoryQuery request, CancellationToken cancellationToken)
        => _service.GetByIdAsync(request.Id);
}

public sealed record AssignPlanningYearCategoryCommand(AssignPlanningYearCategoryRequest Request)
    : IRequest<PlanningYearCategoryDto>;
public sealed class AssignPlanningYearCategoryCommandHandler
    : IRequestHandler<AssignPlanningYearCategoryCommand, PlanningYearCategoryDto>
{
    private readonly IPlanningYearCategoryService _service;
    public AssignPlanningYearCategoryCommandHandler(IPlanningYearCategoryService service) => _service = service;
    public Task<PlanningYearCategoryDto> Handle(AssignPlanningYearCategoryCommand request, CancellationToken cancellationToken)
        => _service.AssignAsync(request.Request);
}

public sealed record SetPlanningYearCategoryActiveStatusCommand(int Id, bool IsActive) : IRequest<bool>;
public sealed class SetPlanningYearCategoryActiveStatusCommandHandler
    : IRequestHandler<SetPlanningYearCategoryActiveStatusCommand, bool>
{
    private readonly IPlanningYearCategoryService _service;
    public SetPlanningYearCategoryActiveStatusCommandHandler(IPlanningYearCategoryService service) => _service = service;
    public Task<bool> Handle(SetPlanningYearCategoryActiveStatusCommand request, CancellationToken cancellationToken)
        => _service.SetActiveStatusAsync(request.Id, request.IsActive);
}

public sealed record GetPlanningYearItemsQuery : IRequest<List<PlanningYearItemDto>>;
public sealed class GetPlanningYearItemsQueryHandler : IRequestHandler<GetPlanningYearItemsQuery, List<PlanningYearItemDto>>
{
    private readonly IPlanningYearItemService _service;
    public GetPlanningYearItemsQueryHandler(IPlanningYearItemService service) => _service = service;
    public Task<List<PlanningYearItemDto>> Handle(GetPlanningYearItemsQuery request, CancellationToken cancellationToken)
        => _service.GetAllAsync();
}

public sealed record GetPlanningYearItemsByCategoryQuery(int PlanningYearCategoryId)
    : IRequest<List<PlanningYearItemDto>>;
public sealed class GetPlanningYearItemsByCategoryQueryHandler
    : IRequestHandler<GetPlanningYearItemsByCategoryQuery, List<PlanningYearItemDto>>
{
    private readonly IPlanningYearItemService _service;
    public GetPlanningYearItemsByCategoryQueryHandler(IPlanningYearItemService service) => _service = service;
    public Task<List<PlanningYearItemDto>> Handle(GetPlanningYearItemsByCategoryQuery request, CancellationToken cancellationToken)
        => _service.GetByPlanningYearCategoryIdAsync(request.PlanningYearCategoryId);
}

public sealed record GetPlanningYearItemQuery(int Id) : IRequest<PlanningYearItemDto?>;
public sealed class GetPlanningYearItemQueryHandler : IRequestHandler<GetPlanningYearItemQuery, PlanningYearItemDto?>
{
    private readonly IPlanningYearItemService _service;
    public GetPlanningYearItemQueryHandler(IPlanningYearItemService service) => _service = service;
    public Task<PlanningYearItemDto?> Handle(GetPlanningYearItemQuery request, CancellationToken cancellationToken)
        => _service.GetByIdAsync(request.Id);
}

public sealed record AssignPlanningYearItemCommand(AssignPlanningYearItemRequest Request)
    : IRequest<PlanningYearItemDto>;
public sealed class AssignPlanningYearItemCommandHandler
    : IRequestHandler<AssignPlanningYearItemCommand, PlanningYearItemDto>
{
    private readonly IPlanningYearItemService _service;
    public AssignPlanningYearItemCommandHandler(IPlanningYearItemService service) => _service = service;
    public Task<PlanningYearItemDto> Handle(AssignPlanningYearItemCommand request, CancellationToken cancellationToken)
        => _service.AssignAsync(request.Request);
}

public sealed record UpdatePlanningYearItemCommand(int Id, UpdatePlanningYearItemRequest Request)
    : IRequest<PlanningYearItemDto?>;
public sealed class UpdatePlanningYearItemCommandHandler
    : IRequestHandler<UpdatePlanningYearItemCommand, PlanningYearItemDto?>
{
    private readonly IPlanningYearItemService _service;
    public UpdatePlanningYearItemCommandHandler(IPlanningYearItemService service) => _service = service;
    public Task<PlanningYearItemDto?> Handle(UpdatePlanningYearItemCommand request, CancellationToken cancellationToken)
        => _service.UpdateAsync(request.Id, request.Request);
}

public sealed record SetPlanningYearItemActiveStatusCommand(int Id, bool IsActive) : IRequest<bool>;
public sealed class SetPlanningYearItemActiveStatusCommandHandler
    : IRequestHandler<SetPlanningYearItemActiveStatusCommand, bool>
{
    private readonly IPlanningYearItemService _service;
    public SetPlanningYearItemActiveStatusCommandHandler(IPlanningYearItemService service) => _service = service;
    public Task<bool> Handle(SetPlanningYearItemActiveStatusCommand request, CancellationToken cancellationToken)
        => _service.SetActiveStatusAsync(request.Id, request.IsActive);
}

public sealed record GetPlanningYearsQuery : IRequest<List<PlanningYearDto>>;
public sealed class GetPlanningYearsQueryHandler : IRequestHandler<GetPlanningYearsQuery, List<PlanningYearDto>>
{
    private readonly IPlanningYearService _service;
    public GetPlanningYearsQueryHandler(IPlanningYearService service) => _service = service;
    public Task<List<PlanningYearDto>> Handle(GetPlanningYearsQuery request, CancellationToken cancellationToken)
        => _service.GetAllAsync();
}

public sealed record GetPlanningYearQuery(int Id) : IRequest<PlanningYearDto?>;
public sealed class GetPlanningYearQueryHandler : IRequestHandler<GetPlanningYearQuery, PlanningYearDto?>
{
    private readonly IPlanningYearService _service;
    public GetPlanningYearQueryHandler(IPlanningYearService service) => _service = service;
    public Task<PlanningYearDto?> Handle(GetPlanningYearQuery request, CancellationToken cancellationToken)
        => _service.GetByIdAsync(request.Id);
}

public sealed record CreatePlanningYearCommand(CreatePlanningYearRequest Request) : IRequest<PlanningYearDto>;
public sealed class CreatePlanningYearCommandHandler : IRequestHandler<CreatePlanningYearCommand, PlanningYearDto>
{
    private readonly IPlanningYearService _service;
    public CreatePlanningYearCommandHandler(IPlanningYearService service) => _service = service;
    public Task<PlanningYearDto> Handle(CreatePlanningYearCommand request, CancellationToken cancellationToken)
        => _service.CreateAsync(request.Request);
}

public sealed record UpdatePlanningYearCommand(int Id, CreatePlanningYearRequest Request) : IRequest<PlanningYearDto?>;
public sealed class UpdatePlanningYearCommandHandler : IRequestHandler<UpdatePlanningYearCommand, PlanningYearDto?>
{
    private readonly IPlanningYearService _service;
    public UpdatePlanningYearCommandHandler(IPlanningYearService service) => _service = service;
    public Task<PlanningYearDto?> Handle(UpdatePlanningYearCommand request, CancellationToken cancellationToken)
        => _service.UpdateAsync(request.Id, request.Request);
}

public sealed record UpdatePlanningYearStatusCommand(int Id, string Status) : IRequest<bool>;
public sealed class UpdatePlanningYearStatusCommandHandler : IRequestHandler<UpdatePlanningYearStatusCommand, bool>
{
    private readonly IPlanningYearService _service;
    public UpdatePlanningYearStatusCommandHandler(IPlanningYearService service) => _service = service;
    public Task<bool> Handle(UpdatePlanningYearStatusCommand request, CancellationToken cancellationToken)
        => _service.UpdateStatusAsync(request.Id, request.Status);
}

public sealed record GetDepartmentsQuery : IRequest<List<DepartmentDto>>;
public sealed class GetDepartmentsQueryHandler : IRequestHandler<GetDepartmentsQuery, List<DepartmentDto>>
{
    private readonly IDepartmentService _service;
    public GetDepartmentsQueryHandler(IDepartmentService service) => _service = service;
    public Task<List<DepartmentDto>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        => _service.GetAllAsync();
}

public sealed record GetDepartmentQuery(int Id) : IRequest<DepartmentDto?>;
public sealed class GetDepartmentQueryHandler : IRequestHandler<GetDepartmentQuery, DepartmentDto?>
{
    private readonly IDepartmentService _service;
    public GetDepartmentQueryHandler(IDepartmentService service) => _service = service;
    public Task<DepartmentDto?> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
        => _service.GetByIdAsync(request.Id);
}

public sealed record CreateDepartmentCommand(CreateDepartmentRequest Request) : IRequest<DepartmentDto>;
public sealed class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, DepartmentDto>
{
    private readonly IDepartmentService _service;
    public CreateDepartmentCommandHandler(IDepartmentService service) => _service = service;
    public Task<DepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        => _service.CreateAsync(request.Request);
}

public sealed record UpdateDepartmentCommand(int Id, CreateDepartmentRequest Request) : IRequest<DepartmentDto?>;
public sealed class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, DepartmentDto?>
{
    private readonly IDepartmentService _service;
    public UpdateDepartmentCommandHandler(IDepartmentService service) => _service = service;
    public Task<DepartmentDto?> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        => _service.UpdateAsync(request.Id, request.Request);
}

public sealed record SetDepartmentActiveStatusCommand(int Id, bool IsActive) : IRequest<bool>;
public sealed class SetDepartmentActiveStatusCommandHandler : IRequestHandler<SetDepartmentActiveStatusCommand, bool>
{
    private readonly IDepartmentService _service;
    public SetDepartmentActiveStatusCommandHandler(IDepartmentService service) => _service = service;
    public Task<bool> Handle(SetDepartmentActiveStatusCommand request, CancellationToken cancellationToken)
        => _service.SetActiveStatusAsync(request.Id, request.IsActive);
}

public sealed record GetDepartmentSectionsQuery : IRequest<List<DepartmentSectionDto>>;
public sealed class GetDepartmentSectionsQueryHandler : IRequestHandler<GetDepartmentSectionsQuery, List<DepartmentSectionDto>>
{
    private readonly IDepartmentSectionService _service;
    public GetDepartmentSectionsQueryHandler(IDepartmentSectionService service) => _service = service;
    public Task<List<DepartmentSectionDto>> Handle(GetDepartmentSectionsQuery request, CancellationToken cancellationToken)
        => _service.GetAllAsync();
}

public sealed record GetDepartmentSectionsByDepartmentQuery(int DepartmentId) : IRequest<List<DepartmentSectionDto>>;
public sealed class GetDepartmentSectionsByDepartmentQueryHandler : IRequestHandler<GetDepartmentSectionsByDepartmentQuery, List<DepartmentSectionDto>>
{
    private readonly IDepartmentSectionService _service;
    public GetDepartmentSectionsByDepartmentQueryHandler(IDepartmentSectionService service) => _service = service;
    public Task<List<DepartmentSectionDto>> Handle(GetDepartmentSectionsByDepartmentQuery request, CancellationToken cancellationToken)
        => _service.GetByDepartmentIdAsync(request.DepartmentId);
}

public sealed record GetDepartmentSectionQuery(int Id) : IRequest<DepartmentSectionDto?>;
public sealed class GetDepartmentSectionQueryHandler : IRequestHandler<GetDepartmentSectionQuery, DepartmentSectionDto?>
{
    private readonly IDepartmentSectionService _service;
    public GetDepartmentSectionQueryHandler(IDepartmentSectionService service) => _service = service;
    public Task<DepartmentSectionDto?> Handle(GetDepartmentSectionQuery request, CancellationToken cancellationToken)
        => _service.GetByIdAsync(request.Id);
}

public sealed record CreateDepartmentSectionCommand(CreateDepartmentSectionRequest Request) : IRequest<DepartmentSectionDto>;
public sealed class CreateDepartmentSectionCommandHandler : IRequestHandler<CreateDepartmentSectionCommand, DepartmentSectionDto>
{
    private readonly IDepartmentSectionService _service;
    public CreateDepartmentSectionCommandHandler(IDepartmentSectionService service) => _service = service;
    public Task<DepartmentSectionDto> Handle(CreateDepartmentSectionCommand request, CancellationToken cancellationToken)
        => _service.CreateAsync(request.Request);
}

public sealed record UpdateDepartmentSectionCommand(int Id, CreateDepartmentSectionRequest Request) : IRequest<DepartmentSectionDto?>;
public sealed class UpdateDepartmentSectionCommandHandler : IRequestHandler<UpdateDepartmentSectionCommand, DepartmentSectionDto?>
{
    private readonly IDepartmentSectionService _service;
    public UpdateDepartmentSectionCommandHandler(IDepartmentSectionService service) => _service = service;
    public Task<DepartmentSectionDto?> Handle(UpdateDepartmentSectionCommand request, CancellationToken cancellationToken)
        => _service.UpdateAsync(request.Id, request.Request);
}

public sealed record SetDepartmentSectionActiveStatusCommand(int Id, bool IsActive) : IRequest<bool>;
public sealed class SetDepartmentSectionActiveStatusCommandHandler : IRequestHandler<SetDepartmentSectionActiveStatusCommand, bool>
{
    private readonly IDepartmentSectionService _service;
    public SetDepartmentSectionActiveStatusCommandHandler(IDepartmentSectionService service) => _service = service;
    public Task<bool> Handle(SetDepartmentSectionActiveStatusCommand request, CancellationToken cancellationToken)
        => _service.SetActiveStatusAsync(request.Id, request.IsActive);
}

public sealed record GetProcurementCategoriesQuery : IRequest<List<ProcurementCategoryDto>>;
public sealed class GetProcurementCategoriesQueryHandler : IRequestHandler<GetProcurementCategoriesQuery, List<ProcurementCategoryDto>>
{
    private readonly IProcurementCategoryService _service;
    public GetProcurementCategoriesQueryHandler(IProcurementCategoryService service) => _service = service;
    public Task<List<ProcurementCategoryDto>> Handle(GetProcurementCategoriesQuery request, CancellationToken cancellationToken)
        => _service.GetAllAsync();
}

public sealed record GetProcurementCategoryQuery(int Id) : IRequest<ProcurementCategoryDto?>;
public sealed class GetProcurementCategoryQueryHandler : IRequestHandler<GetProcurementCategoryQuery, ProcurementCategoryDto?>
{
    private readonly IProcurementCategoryService _service;
    public GetProcurementCategoryQueryHandler(IProcurementCategoryService service) => _service = service;
    public Task<ProcurementCategoryDto?> Handle(GetProcurementCategoryQuery request, CancellationToken cancellationToken)
        => _service.GetByIdAsync(request.Id);
}

public sealed record GetProcurementCategoryHistoryQuery(int Id)
    : IRequest<List<ProcurementCategoryAuditLogDto>>;
public sealed class GetProcurementCategoryHistoryQueryHandler
    : IRequestHandler<GetProcurementCategoryHistoryQuery, List<ProcurementCategoryAuditLogDto>>
{
    private readonly IProcurementCategoryService _service;
    public GetProcurementCategoryHistoryQueryHandler(IProcurementCategoryService service) => _service = service;
    public Task<List<ProcurementCategoryAuditLogDto>> Handle(GetProcurementCategoryHistoryQuery request, CancellationToken cancellationToken)
        => _service.GetHistoryAsync(request.Id);
}

public sealed record CreateProcurementCategoryCommand(CreateProcurementCategoryRequest Request, int ChangedByUserId)
    : IRequest<ProcurementCategoryDto>;
public sealed class CreateProcurementCategoryCommandHandler
    : IRequestHandler<CreateProcurementCategoryCommand, ProcurementCategoryDto>
{
    private readonly IProcurementCategoryService _service;
    public CreateProcurementCategoryCommandHandler(IProcurementCategoryService service) => _service = service;
    public Task<ProcurementCategoryDto> Handle(CreateProcurementCategoryCommand request, CancellationToken cancellationToken)
        => _service.CreateAsync(request.Request, request.ChangedByUserId);
}

public sealed record UpdateProcurementCategoryCommand(int Id, CreateProcurementCategoryRequest Request, int ChangedByUserId)
    : IRequest<ProcurementCategoryDto?>;
public sealed class UpdateProcurementCategoryCommandHandler
    : IRequestHandler<UpdateProcurementCategoryCommand, ProcurementCategoryDto?>
{
    private readonly IProcurementCategoryService _service;
    public UpdateProcurementCategoryCommandHandler(IProcurementCategoryService service) => _service = service;
    public Task<ProcurementCategoryDto?> Handle(UpdateProcurementCategoryCommand request, CancellationToken cancellationToken)
        => _service.UpdateAsync(request.Id, request.Request, request.ChangedByUserId);
}

public sealed record SetProcurementCategoryActiveStatusCommand(int Id, bool IsActive, int ChangedByUserId) : IRequest<bool>;
public sealed class SetProcurementCategoryActiveStatusCommandHandler
    : IRequestHandler<SetProcurementCategoryActiveStatusCommand, bool>
{
    private readonly IProcurementCategoryService _service;
    public SetProcurementCategoryActiveStatusCommandHandler(IProcurementCategoryService service) => _service = service;
    public Task<bool> Handle(SetProcurementCategoryActiveStatusCommand request, CancellationToken cancellationToken)
        => _service.SetActiveStatusAsync(request.Id, request.IsActive, request.ChangedByUserId);
}

public sealed record GetRolesQuery : IRequest<List<RoleDto>>;
public sealed class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleDto>>
{
    private readonly IRoleService _service;
    public GetRolesQueryHandler(IRoleService service) => _service = service;
    public Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken) => _service.GetAllAsync();
}

public sealed record GetRoleQuery(int Id) : IRequest<RoleDto?>;
public sealed class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, RoleDto?>
{
    private readonly IRoleService _service;
    public GetRoleQueryHandler(IRoleService service) => _service = service;
    public Task<RoleDto?> Handle(GetRoleQuery request, CancellationToken cancellationToken) => _service.GetByIdAsync(request.Id);
}

public sealed record CreateRoleCommand(CreateRoleRequest Request) : IRequest<RoleDto>;
public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, RoleDto>
{
    private readonly IRoleService _service;
    public CreateRoleCommandHandler(IRoleService service) => _service = service;
    public Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken) => _service.CreateAsync(request.Request);
}

public sealed record UpdateRoleCommand(int Id, CreateRoleRequest Request) : IRequest<RoleDto?>;
public sealed class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, RoleDto?>
{
    private readonly IRoleService _service;
    public UpdateRoleCommandHandler(IRoleService service) => _service = service;
    public Task<RoleDto?> Handle(UpdateRoleCommand request, CancellationToken cancellationToken) => _service.UpdateAsync(request.Id, request.Request);
}

public sealed record DeleteRoleCommand(int Id) : IRequest<bool>;
public sealed class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
{
    private readonly IRoleService _service;
    public DeleteRoleCommandHandler(IRoleService service) => _service = service;
    public Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken) => _service.DeleteAsync(request.Id);
}

public sealed record GetUsersQuery : IRequest<List<UserDto>>;
public sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly IUserService _service;
    public GetUsersQueryHandler(IUserService service) => _service = service;
    public Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken) => _service.GetAllAsync();
}

public sealed record GetUserQuery(int Id) : IRequest<UserDto?>;
public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto?>
{
    private readonly IUserService _service;
    public GetUserQueryHandler(IUserService service) => _service = service;
    public Task<UserDto?> Handle(GetUserQuery request, CancellationToken cancellationToken) => _service.GetByIdAsync(request.Id);
}

public sealed record CreateUserCommand(CreateUserRequest Request) : IRequest<UserDto>;
public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserService _service;
    public CreateUserCommandHandler(IUserService service) => _service = service;
    public Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken) => _service.CreateAsync(request.Request);
}

public sealed record UpdateUserCommand(int Id, CreateUserRequest Request) : IRequest<UserDto?>;
public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto?>
{
    private readonly IUserService _service;
    public UpdateUserCommandHandler(IUserService service) => _service = service;
    public Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken) => _service.UpdateAsync(request.Id, request.Request);
}

public sealed record SetUserActiveStatusCommand(int Id, bool IsActive) : IRequest<bool>;
public sealed class SetUserActiveStatusCommandHandler : IRequestHandler<SetUserActiveStatusCommand, bool>
{
    private readonly IUserService _service;
    public SetUserActiveStatusCommandHandler(IUserService service) => _service = service;
    public Task<bool> Handle(SetUserActiveStatusCommand request, CancellationToken cancellationToken) => _service.SetActiveStatusAsync(request.Id, request.IsActive);
}

public sealed record GetUserRolesQuery(int UserId) : IRequest<List<UserRoleDto>>;
public sealed class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, List<UserRoleDto>>
{
    private readonly IUserRoleService _service;
    public GetUserRolesQueryHandler(IUserRoleService service) => _service = service;
    public Task<List<UserRoleDto>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken) => _service.GetUserRolesAsync(request.UserId);
}

public sealed record AssignUserRoleCommand(int UserId, AssignUserRoleRequest Request) : IRequest<UserRoleDto>;
public sealed class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand, UserRoleDto>
{
    private readonly IUserRoleService _service;
    public AssignUserRoleCommandHandler(IUserRoleService service) => _service = service;
    public Task<UserRoleDto> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken) => _service.AssignRoleAsync(request.UserId, request.Request);
}

public sealed record RemoveUserRoleCommand(int UserId, int RoleId) : IRequest<bool>;
public sealed class RemoveUserRoleCommandHandler : IRequestHandler<RemoveUserRoleCommand, bool>
{
    private readonly IUserRoleService _service;
    public RemoveUserRoleCommandHandler(IUserRoleService service) => _service = service;
    public Task<bool> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken) => _service.RemoveRoleAsync(request.UserId, request.RoleId);
}

public sealed record GetUserCategoryPreferencesQuery(int UserId, int PlanningYearId)
    : IRequest<List<UserCategoryPreferenceDto>>;
public sealed class GetUserCategoryPreferencesQueryHandler
    : IRequestHandler<GetUserCategoryPreferencesQuery, List<UserCategoryPreferenceDto>>
{
    private readonly IUserCategoryPreferenceService _service;
    public GetUserCategoryPreferencesQueryHandler(IUserCategoryPreferenceService service) => _service = service;
    public Task<List<UserCategoryPreferenceDto>> Handle(GetUserCategoryPreferencesQuery request, CancellationToken cancellationToken)
        => _service.GetByUserAndPlanningYearAsync(request.UserId, request.PlanningYearId);
}

public sealed record SaveUserCategoryPreferencesCommand(int UserId, SaveUserCategoryPreferencesRequest Request)
    : IRequest<List<UserCategoryPreferenceDto>>;
public sealed class SaveUserCategoryPreferencesCommandHandler
    : IRequestHandler<SaveUserCategoryPreferencesCommand, List<UserCategoryPreferenceDto>>
{
    private readonly IUserCategoryPreferenceService _service;
    public SaveUserCategoryPreferencesCommandHandler(IUserCategoryPreferenceService service) => _service = service;
    public Task<List<UserCategoryPreferenceDto>> Handle(SaveUserCategoryPreferencesCommand request, CancellationToken cancellationToken)
        => _service.SaveAsync(request.UserId, request.Request);
}

public sealed record GetAPPsQuery : IRequest<IReadOnlyList<APPListDto>>;
public sealed class GetAPPsQueryHandler : IRequestHandler<GetAPPsQuery, IReadOnlyList<APPListDto>>
{
    private readonly IAPPService _service;
    public GetAPPsQueryHandler(IAPPService service) => _service = service;
    public Task<IReadOnlyList<APPListDto>> Handle(GetAPPsQuery request, CancellationToken cancellationToken) => _service.GetAllAsync();
}

public sealed record GetAPPQuery(int Id) : IRequest<APPDetailDto?>;
public sealed class GetAPPQueryHandler : IRequestHandler<GetAPPQuery, APPDetailDto?>
{
    private readonly IAPPService _service;
    public GetAPPQueryHandler(IAPPService service) => _service = service;
    public Task<APPDetailDto?> Handle(GetAPPQuery request, CancellationToken cancellationToken) => _service.GetByIdAsync(request.Id);
}

public sealed record GenerateAPPCommand(int PlanningYearId, int CreatedBy) : IRequest<GenerateAPPResultDto>;
public sealed class GenerateAPPCommandHandler : IRequestHandler<GenerateAPPCommand, GenerateAPPResultDto>
{
    private readonly IAPPService _service;
    public GenerateAPPCommandHandler(IAPPService service) => _service = service;
    public Task<GenerateAPPResultDto> Handle(GenerateAPPCommand request, CancellationToken cancellationToken) => _service.GenerateAsync(request.PlanningYearId, request.CreatedBy);
}

public sealed record AddReviewedPPMPsCommand(int AppId, int AddedBy) : IRequest<GenerateAPPResultDto>;
public sealed class AddReviewedPPMPsCommandHandler : IRequestHandler<AddReviewedPPMPsCommand, GenerateAPPResultDto>
{
    private readonly IAPPService _service;
    public AddReviewedPPMPsCommandHandler(IAPPService service) => _service = service;
    public Task<GenerateAPPResultDto> Handle(AddReviewedPPMPsCommand request, CancellationToken cancellationToken) => _service.AddReviewedPPMPsAsync(request.AppId, request.AddedBy);
}
