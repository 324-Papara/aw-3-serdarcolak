using Autofac;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Para.Base.Response;
using Para.Bussiness.Cqrs;
using Para.Bussiness.Query;
using Para.Data.Context;
using Para.Data.GenericRepository;
using Para.Data.UnitOfWork;
using Para.Schema;

namespace Para.Bussiness.AutoFac;

public class AutoFacModule: Module
{
    private readonly IConfiguration _configuration;

    public AutoFacModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    
    
    protected override void Load(ContainerBuilder builder)
    {
        // DbContext'in kaydedilmesi
            builder.Register(context =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<ParaDbContext>();
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("MsSqlConnection"));
                return new ParaDbContext(optionsBuilder.Options);
            }).AsSelf().InstancePerLifetimeScope();
            
            //DapperContext kaydedilmesi
            builder.RegisterType<DapperContext>().AsSelf().SingleInstance();
            
            
            // UnitOfWork ve GenericRepository'nin kaydedilmesi
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();

            // MediatR QueryHandler'larının kaydedilmesi
            builder.RegisterType<CustomerQueryHandler>().As<IRequestHandler<GetAllCustomerQuery, ApiResponse<List<CustomerResponse>>>>();
            builder.RegisterType<CustomerQueryHandler>().As<IRequestHandler<GetCustomerByIdQuery, ApiResponse<CustomerResponse>>>();
            builder.RegisterType<CustomerQueryHandler>().As<IRequestHandler<GetCustomersByNameQuery, ApiResponse<List<CustomerResponse>>>>();
            
            builder.RegisterType<CustomerAddressQueryHandler>().As<IRequestHandler<GetAllCustomerAddressQuery, ApiResponse<List<CustomerAddressResponse>>>>();
            builder.RegisterType<CustomerAddressQueryHandler>().As<IRequestHandler<GetCustomerAddressByIdQuery, ApiResponse<CustomerAddressResponse>>>();
            
            builder.RegisterType<CustomerDetailQueryHandler>().As<IRequestHandler<GetAllCustomerDetailQuery, ApiResponse<List<CustomerDetailResponse>>>>();
            builder.RegisterType<CustomerDetailQueryHandler>().As<IRequestHandler<GetCustomerDetailByIdQuery, ApiResponse<CustomerDetailResponse>>>();
            
            builder.RegisterType<CustomerPhoneQueryHandler>().As<IRequestHandler<GetAllCustomerPhoneQuery, ApiResponse<List<CustomerPhoneResponse>>>>();
            builder.RegisterType<CustomerPhoneQueryHandler>().As<IRequestHandler<GetCustomerPhoneByIdQuery, ApiResponse<CustomerPhoneResponse>>>();
    }
}