﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AspReactTestApp.DTOs;
using AspReactTestApp.Entities.Concrete.CarRelated;
using AspReactTestApp.Services.FueltypeService;
using AspReactTestApp.Services.RegionService;
using AspReactTestApp.Dto;

namespace AspReactTestApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;

        public RegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        [HttpPost("addregion")]
        public async Task<ActionResult<ResponseDto>> AddRegion(Region region)
        {
            var result = await _regionService.AddRegion(region);
            return Ok(result);
        }

        [HttpDelete("deleteregion")]
        public async Task<ActionResult<ResponseDto>> DeleteRegion(int id)
        {
            var result = await _regionService.RemoveRegionById(id);
            return Ok(result);
        }

        [HttpGet("getregions/{languageId}")]
        public async Task<ActionResult<List<GenericEntityDto>>> GetRegions(int languageId)
        {
            var regionList = await _regionService.GetAllRegions(languageId);
            return regionList;
        }
    }
}