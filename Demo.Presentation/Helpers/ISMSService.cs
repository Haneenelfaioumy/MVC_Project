using Demo.Presentation.Utilities;
using Twilio.Rest.Api.V2010.Account;

namespace Demo.Presentation.Helpers
{
    public interface ISMSService
    {
        MessageResource SendSMS(SMSMessage smsMessage);
    }
}
