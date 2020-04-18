using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Messaging;
using Twilio.Types;

namespace SimpleConsoleProject
{
    internal static class SimpleConsoleClass
    {
        private static async Task Main()
        {
            const string message = "Hello World";
            
            DeliverToStandardOutput(message);
            // DeliverToPhoneNumberAsSms(message);
            await DeliverToEmailAddress(message);
            // todo: extract class, then classes, then folders, then project, then solution
        }

        private static async Task DeliverToEmailAddress(string message)
        {
            var key = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(key);
            await client.SendEmailAsync(new SendGridMessage
            {
                From = new EmailAddress("teodorchirileanu@thecarpathiancoder.dev"),
                ReplyTo = new EmailAddress("teodorchirileanu@gmail.com"),
                Subject = "You've got a new message!",
                PlainTextContent = message
            });
        }

        private static void DeliverToPhoneNumberAsSms(string message)
        {
            // todo: put these string in a configuration file e.g. config.net or storage.net
            var accountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID");
            // todo: update auth token
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
