using Demo.Presentation.Settings;
using Demo.Presentation.Utilities;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Demo.Presentation.Helpers
{
    public class SMSService : ISMSService
    {
        private readonly IOptions<SMSSettings> _options;

        public SMSService(IOptions<SMSSettings> options)
        {
            _options = options;
        }
        public MessageResource SendSMS(SMSMessage smsMessage)
        {
            TwilioClient.Init(_options.Value.AccountSID , _options.Value.AuthToken);
            var message = MessageResource.Create(
                body:smsMessage.Body ,
                from:new Twilio.Types.PhoneNumber(_options.Value.TwilioPhoneNumber),
                to:smsMessage.PhoneNumber
            );
            return message;
        }
    }
}
