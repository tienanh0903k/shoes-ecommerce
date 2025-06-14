using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using ShoesStoreApp.BLL.ViewModels.Payment;
using ShoesStoreApp.DAL.Models;

namespace ShoesStoreApp.BLL.Services.PaymentService;

public class PaymentService : IPaymentService
{
    private readonly IOptions<MomoOptionModel> _options;
    public PaymentService(IOptions<MomoOptionModel> options)
    {
        _options = options;
    }
    
    public async Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(Order model)
    {
        var rawData =
            $"partnerCode={_options.Value.PartnerCode}" +
            $"&accessKey={_options.Value.AccessKey}" +
            $"&requestId={model.OrderId}" +
            $"&amount={model.Total.ToString("F0")}" +
            $"&orderId={model.OrderId}" +
            $"&orderInfo={"Thanh toán đơn hàng"}" +
            $"&returnUrl={_options.Value.ReturnUrl}" +
            $"&notifyUrl={_options.Value.NotifyUrl}" +
            $"&extraData=";
        
        var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);

        var client = new RestClient(_options.Value.MomoApiUrl);
        var request = new RestRequest() { Method = Method.Post };
        request.AddHeader("Content-Type", "application/json; charset=UTF-8");

        var test = model.OrderId.ToString().GetType();
        var test1 = model.Total.ToString().GetType();
        // Create an object representing the request data
        var requestData = new
        {
            accessKey = _options.Value.AccessKey,
            partnerCode = _options.Value.PartnerCode,
            requestType = _options.Value.RequestType,
            notifyUrl = _options.Value.NotifyUrl,
            returnUrl = _options.Value.ReturnUrl,
            orderId = model.OrderId.ToString(),
            amount = model.Total.ToString("F0"),
            orderInfo = "Thanh toán đơn hàng",
            requestId = model.OrderId.ToString(),
            extraData = "",
            signature = signature
        };
        
        request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

        var response = await client.ExecuteAsync(request);
        var momoResponse = JsonConvert.DeserializeObject<MomoCreatePaymentResponseModel>(response.Content);
        return momoResponse;
    }
    
    private string ComputeHmacSha256(string message, string secretKey)
    {
        var keyBytes = Encoding.UTF8.GetBytes(secretKey);
        var messageBytes = Encoding.UTF8.GetBytes(message);

        byte[] hashBytes;

        using (var hmac = new HMACSHA256(keyBytes))
        {
            hashBytes = hmac.ComputeHash(messageBytes);
        }

        var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

        return hashString;
    }
}