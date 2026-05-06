using Application.DTOs.CandidateDtos;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Application.Interfaces
{
    public interface ICandidateService
    {
         Task<CandidateProfileDto?> GetProfileByIdAsync(string id);
    }
}
