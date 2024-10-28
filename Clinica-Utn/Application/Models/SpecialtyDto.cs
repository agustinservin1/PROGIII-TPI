using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class SpecialtyDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public bool Status { get; set; }
        public static SpecialtyDto CreateSpecialtyDto(Specialty specialty)
        {
            var specialtyDto = new SpecialtyDto()
            {
                Id = specialty.Id,
                Name = specialty.Name,
                Description = specialty.Description,
                Status = specialty.Status,
            };

            return specialtyDto;
        }

        public static IEnumerable<SpecialtyDto> CreateListDto(IEnumerable<Specialty> list)
        {
            var listDto = new List<SpecialtyDto>();

            foreach (Specialty s in list)
            {
                listDto.Add(CreateSpecialtyDto(s));
            }

            return listDto;
        }
    }
}

