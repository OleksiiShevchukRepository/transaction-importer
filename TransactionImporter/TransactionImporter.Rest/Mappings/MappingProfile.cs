using AutoMapper;
using System;
using TransactionImporter.DataAccess.Entities;
using TransactionImporter.Rest.Models;

namespace DummyMvc.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile(string profileName) : base(profileName)
        {
            CreateMap<TransactionImportModel, TransactionImportModel>();
            CreateMap<TransactionXml, TransactionImportModel>()
                .ForMember(d => d.Id, s => s.MapFrom(src => src.Id))
                .ForMember(d => d.Amount, s => s.MapFrom(src => src.PaymentDetails.Amount))
                .ForMember(d => d.CurrencyCode, s => s.MapFrom(src => src.PaymentDetails.CurrencyCode))
                .ForMember(d => d.TransactionDate, s => s.MapFrom(src => src.TransactionDate))
                .ForMember(d => d.TransactionStatus, s => s.MapFrom(src => src.TransactionStatus));

            CreateMap<TransactionCsv, TransactionImportModel>()
               .ForMember(d => d.Id, s => s.MapFrom(src => src.Id))
               .ForMember(d => d.Amount, s => s.MapFrom(src => src.Amount))
               .ForMember(d => d.CurrencyCode, s => s.MapFrom(src => src.CurrencyCode))
               .ForMember(d => d.TransactionDate, s => s.MapFrom(src => src.TransactionDate))
               .ForMember(d => d.TransactionStatus, s => s.MapFrom(src => src.TransactionStatus));

            CreateMap<TransactionImportModel, Transaction>()
                .ForMember(d => d.Id, s => s.MapFrom(src => src.Id))
                .ForMember(d => d.Amount, s => s.MapFrom(src => src.Amount))
                .ForMember(d => d.CurrencyCode, s => s.MapFrom(src => src.CurrencyCode))
                .ForMember(d => d.TransactionDate, s => s.MapFrom(src => src.TransactionDate.Value))
                .ForMember(d => d.TransactionStatus, 
                    s => s.MapFrom(src => (TransactionStatusEnum)Enum.Parse(typeof(TransactionStatusEnum), src.TransactionStatus.ToString())));

            CreateMap<Transaction, TransactionImportModel > ()
                .ForMember(d => d.Id, s => s.MapFrom(src => src.Id))
                .ForMember(d => d.Amount, s => s.MapFrom(src => src.Amount))
                .ForMember(d => d.CurrencyCode, s => s.MapFrom(src => src.CurrencyCode))
                .ForMember(d => d.TransactionDate, s => s.MapFrom(src => src.TransactionDate))
                .ForMember(d => d.TransactionStatus,
                    s => s.MapFrom(src => (TransactionStatus)Enum.Parse(typeof(TransactionStatus), src.TransactionStatus.ToString())));

            CreateMap<Transaction, TransactionReadModel>()
                .ForMember(d => d.Id, s => s.MapFrom(src => src.Id))
                .ForMember(d =>d.Payment, s => s.MapFrom(src => $"{src.Amount} {src.CurrencyCode}"))
                .ForMember(d => d.Status, s => s.MapFrom(src => src.TransactionStatus.ToString()));
        }
    }
}