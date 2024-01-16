

using AutoMapper;
using MOS.Data.Entity;
using MOS.Schema;

namespace MOS.Business.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {

        CreateMap<PersonalRequest, Personal>();
        CreateMap<Personal, PersonalResponse>();

        CreateMap<AdminRequest, Admin>();
        CreateMap<Admin, AdminResponse>();

        CreateMap<PersonalExpenseRequest, Expense>();
        CreateMap<Expense, ExpenseResponse>();

        CreateMap<PaymentRequest, Payment>();
        CreateMap<Payment, PaymentResponse>();


        // CreateMap<PaymentRequest, Payment>();
        // CreateMap<Payment, PaymentResponse>()
        //     .ForMember(dest => dest.CustomerName,
        //         src => src.MapFrom(x => x.Customer.FirstName + " " + x.Customer.LastName));

        // CreateMap<ContactRequest, Contact>();
        // CreateMap<Contact, ContactResponse>()
        //     .ForMember(dest => dest.CustomerName,
        //         src => src.MapFrom(x => x.Customer.FirstName + " " + x.Customer.LastName));

        // CreateMap<AccountRequest, Account>();
        // CreateMap<Account, AccountResponse>()
        //     .ForMember(dest => dest.CustomerName,
        //         src => src.MapFrom(x => x.Customer.FirstName + " " + x.Customer.LastName));

        // CreateMap<AccountTransactionRequest, AccountTransaction>();
        // CreateMap<AccountTransaction, AccountTransactionResponse>()
        //     .ForMember(dest => dest.AccountName, src => src.MapFrom(x => x.Account.Name))
        //     .ForMember(dest => dest.CustomerNumber, src => src.MapFrom(x => x.Account.Customer.CustomerNumber))
        //     .ForMember(dest => dest.CustomerName,
        //         src => src.MapFrom(x => x.Account.Customer.FirstName + " " + x.Account.Customer.LastName));

        // CreateMap<EftTransactionRequest, EftTransaction>();
        // CreateMap<EftTransaction, EftTransactionResponse>();

        // CreateMap<ApplicationUserRequest, ApplicationUser>();
        // CreateMap<ApplicationUser, ApplicationUserResponse>();
    }
}