using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Messaging;
using Twilio.Types;

namespace SimpleConsoleProject
{
    internal static class SimpleConsoleClass
    {
        private static void Main()
        {
            const string message = "Hello World";
            
            DeliverToStandardOutput(message);
            DeliverToPhoneNumberAsSms(message);
            // todo: deliver as email
        }

        private static void DeliverToPhoneNumberAsSms(string message)
        {
            var accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            var authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
            TwilioClient.Init(accountSid, authToken);

            var trialNumber = new PhoneNumber("+14243894446");
            var registeredNumber = new PhoneNumber("+33753811986");

            MessageResource.Create(
                body: message,
                from: trialNumber,
                to: registeredNumber
            );
        }

        private static void DeliverToStandardOutput(string message) => Console.WriteLine(message);
    }
}
