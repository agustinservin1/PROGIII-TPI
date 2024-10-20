using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string Street { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

        public static AddressDto CreateAddressDto(Address address)
        {
            var newAddress = new AddressDto()
            {
                Id = address.Id,
                Street = address.Street,
                PostalCode = address.PostalCode,
                Province = address.Province,
                City = address.City,

            };
            return newAddress;
        }
    }
}
