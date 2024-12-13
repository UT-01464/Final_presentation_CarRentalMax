using CarRental_Max.Entities.CarDetails;
using CarRental_Max.I_Services.CarDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRental_Max.Controllers.CarDetails
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService _featureService;

        public FeaturesController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Feature>>> GetAllFeatures()
        {
            return await _featureService.GetAllFeaturesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Feature>> GetFeature(int id)
        {
            var feature = await _featureService.GetFeatureByIdAsync(id);
            if (feature == null)
                return NotFound();
            return feature;
        }

        [HttpPost]
        public async Task<ActionResult> AddFeature(Feature feature)
        {
            await _featureService.AddFeatureAsync(feature);
            return CreatedAtAction(nameof(GetFeature), new { id = feature.Id }, feature);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFeature(int id, Feature feature)
        {
            if (id != feature.Id)
                return BadRequest();

            await _featureService.UpdateFeatureAsync(feature);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFeature(int id)
        {
            await _featureService.DeleteFeatureAsync(id);
            return NoContent();
        }
    }
}
