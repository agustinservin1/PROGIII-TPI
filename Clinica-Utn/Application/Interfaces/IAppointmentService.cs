﻿using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAppointmentService
    {
        AppointmentDto GetById(int id);
        IEnumerable<AppointmentDto> GetAllByDoctorId(int id);
        IEnumerable<AppointmentDto> GetByDoctorAndDate(int idDoctor, DateTime date);
    }
}