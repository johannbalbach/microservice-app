using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using EnrollmentSystem.Common.Models;

namespace DictionaryMicroService.Controllers
{ 
    [ApiController]
    public class DictionaryApiController : ControllerBase
    { 

        [HttpGet]
        [Route("/dictionary/checkStatus/{importId}")]
        public async Task<ActionResult<Response>> CheckDictionaryImportStatus([FromRoute][Required]Guid? importId)
        { 

        }
        [HttpPost]
        [Route("/dictionary/importDictionary")]
        public async Task<ActionResult<Response>> ImportDictionary([FromQuery][Required()]Object file)
        { 

        }
    }
}
