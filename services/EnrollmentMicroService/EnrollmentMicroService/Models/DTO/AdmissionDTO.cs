﻿using EnrollmentMicroService.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EnrollmentMicroService.Models.DTO
{
    public class AdmissionDTO
    {
        public Guid Id { get; set; }
        public Guid ApplicantId { get; set; }
        public DateTime CreatedTime { get; set; }
        public Guid ProgramId { get; set; }
        public string ProgramName { get; set; }
        public int Priority { get; set; }
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public StatusEnum Status { get; set; }
        public Guid? ManagerId { get; set; }
    }
}
