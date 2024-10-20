using Application.Models;
using Application.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPatientService
    {
        PatientDto? GetPatientByIdWithAddress(int id);
        IEnumerable<PatientDto> GetPatientsWithAddress();
        PatientDto CreatePatient(PatientCreateRequest patient);
        PatientDto UpdatePatient(int id, PatientUpdateForRequest patient);
        PatientDto DeletePatient(int id);

    }
}
