

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

    }
}