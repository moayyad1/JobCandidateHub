﻿using Microsoft.AspNetCore.Mvc;

namespace JobCandidateHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Status = "Healthy" });
        }
    }
}