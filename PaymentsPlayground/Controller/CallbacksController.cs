using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaymentsPlayground.Interfaces;
using PaymentsPlayground.Models.Payment.Responses;
using System.Text;

namespace PaymentsPlayground.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CallbacksController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public CallbacksController(IWalletService walletService)
        {
            this._walletService = walletService;
        }

        [HttpPost("liqpay")]
        public IActionResult LiqPayWebHook([FromForm] LiqPayForm response)
        {
            byte[] request_data = Convert.FromBase64String(response.Data);
            string decodedString = Encoding.UTF8.GetString(request_data);
            var liqPayResponse = JsonConvert.DeserializeObject<LiqPayResponse>(decodedString);

            var order_id = liqPayResponse.order_id;

            if (liqPayResponse.status == "sandbox")
            {
                try
                {
                    _walletService.FinishPaymentSuccess(order_id);
                }
                catch (Exception ex)
                {
                    _walletService.FinishPaymentFailure(order_id, ex.Message);
                }
            }
            else
            {
                _walletService.FinishPaymentFailure(order_id, liqPayResponse.err_description);
            }

            return Ok();
        }

        [HttpPost("paypal")]
        public async Task<IActionResult> PayPalWebHook([FromForm] PayPalResponse response)
        {
            var order_id = response.custom;

            if (response.payment_status.Any())
            {
                try
                {
                    _walletService.FinishPaymentSuccess(order_id);
                }
                catch (Exception ex)
                {
                    _walletService.FinishPaymentFailure(order_id, ex.Message);
                }
            }
            else
            {
                _walletService.FinishPaymentFailure(order_id, "Error");
            }

            return Ok();
        }
    }
}
