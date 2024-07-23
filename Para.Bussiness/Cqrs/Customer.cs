using MediatR;
using Para.Base.Response;
using Para.Schema;

namespace Para.Bussiness.Cqrs;

public record CreateCustomerCommand(CustomerRequest Request) : IRequest<ApiResponse<CustomerResponse>>;
public record UpdateCustomerCommand(long CustomerId,CustomerRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerCommand(long CustomerId) : IRequest<ApiResponse>;

public record GetAllCustomerQuery() : IRequest<ApiResponse<List<CustomerResponse>>>;
public record GetCustomerByIdQuery(long CustomerId) : IRequest<ApiResponse<CustomerResponse>>;

public record GetCustomersWithIncludesQuery() : IRequest<ApiResponse<List<CustomerResponse>>>;
public record GetCustomersByNameQuery(string Name) : IRequest<ApiResponse<List<CustomerResponse>>>;


