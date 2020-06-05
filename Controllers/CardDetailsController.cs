using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using wallets_api_wrapper.Auth;
using wallets_api_wrapper.Data;

namespace wallets_api_wrapper.Controllers
{
    [ApiKeyAuth]
    [Route("[controller]")]
    public class CardDetailsController : ControllerBase
    {
        private IAppRepository _repository { get; }
        private ILogger<CardDetailsController> _logger { get; }
        public CardDetailsController(IAppRepository repository, ILogger<CardDetailsController> logger)
        {
            _logger = logger;
            _repository = repository;

        }

        [HttpGet("getCardDetails/{cardBin}")]
        public IActionResult GetCardDetails(string cardBin)
        {
            if (cardBin.Length < 6 || cardBin.Length > 8)
            {
                _logger.LogError($"Invalid length of card bin used, {cardBin}");
                return BadRequest("Card Bin must be 6 to 8 characters long");
            }

            if (!Int64.TryParse(cardBin, out long cardBinAsNum))
            {
                _logger.LogError($"Invalid card bin used, {cardBin}");
                return BadRequest("Invalid card bin, card bin must be numbers only");
            }

            _logger.LogDebug($"Valid card bin, {cardBin}");

            var cardDetails = _repository.GetCardDetails(cardBin);

            if(cardDetails == null)
            {
                _logger.LogError($"Card scheme not found, {cardBin}");

                return NotFound("Card scheme not found");
            }

            _logger.LogDebug($"Card details response: {JsonConvert.SerializeObject(cardDetails)}");

            return Ok(cardDetails);
        }
    }
}