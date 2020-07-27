
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly
{
    public class MappingProfile : Profile
    {//klasa poświęcona sekcji o AutoMapper i jak go używać (numer 68)
        public MappingProfile()
        {
            //CreateMap<Customer, CustomerDto>();
            //CreateMap<CustomerDto, Customer>();

            // Domain to Dto
            CreateMap<Customer, CustomerDto>();
            CreateMap<Movie, MovieDto>();
            CreateMap<MembershipType, MembershipTypeDto>();
            CreateMap<Genre, GenreDto>();

            // Dto to Domain
            CreateMap<CustomerDto, Customer>()
                .ForMember(c => c.Id, opt =>opt.Ignore());

            CreateMap<MovieDto, Movie>()
                .ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}
/*
 *  Pobrane z Git Mosh exercise 6 - komentarz beggarboy on 15 Jan 2019 odnosi się do kodu powyżej
 * // Customer Mappings
   // API -> Outbound
   Mapper.CreateMap<Customer, CustomerDto>();
   
   // API <- Inbound
   Mapper.CreateMap<CustomerDto, Customer>()
   .ForMember(c => c.Id, opt => opt.Ignore());

   Oto mój kod z komentarzami, który pomógł mi zrozumieć, co się dzieje.
   Zasadniczo, gdy Twój interfejs API wychodzi na zewnątrz, aby wysyłać rzeczy do ludzi, 
   zawsze przekazujesz swoje obiekty przez ich konkretne Dto. Musi więc zamapować Klienta -> CustomerDto.
   
   Odwracając to,
   kiedy twoje API dostaje rzeczy wysyłane przez ludzi, wszystkie dane przechodzą najpierw 
   przez Dto, a następnie do obiektu klienta.
   
   Teraz, jeśli skopiujesz, wklej linię .ForMember poniżej wychodzącej „trasy” odwzorowującej, 
   powiesz AutoMapperowi „Hej, nie martw się o identyfikator, nie mapuj tego”.
   
   Teraz, jeśli wykonasz żądanie GET z listonoszem w / api / klientów, nadal otrzymasz wszystkie dane. 
   Tyle, że każdy identyfikator ma wartość 0, ponieważ powiedziałeś AutoMapperowi, żeby się tym nie 
   przejmował.
   
   Tak to dla mnie kliknęło, mam nadzieję, że to wyjaśnienie pomoże kiedyś komuś w tej samej pozycji. 
   Myślałem, że zostawię to tutaj. 😄
 */

                