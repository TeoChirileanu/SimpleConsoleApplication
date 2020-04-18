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
            
            await DeliverToStandardOutput(message);
            await DeliverToPhoneNumberAsSms(message);
            await DeliverToEmailAddress(message);
            // todo: extract class, then classes, then folders, then project, then solution
        }

        private static async Task DeliverToEmailAddress(string message)
        {
            var client = new SendGridClient("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");
            await client.SendEmailAsync(new SendGridMessage
            {
                From = new EmailAddress("teodorchirileanu@thecarpathiancoder.dev"),
                ReplyTo = new EmailAddress("teodorchirileanu@gmail.com"),
                Subject = "You've got a new message!",
                PlainTextContent = message
            });
        }

        private static async Task DeliverToPhoneNumberAsSms(string message)
        {
            // todo: put these string in a configuration file e.g. config.net or storage.net
            // todo: update auth token
            var authToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN");
            TwilioClient.Init("ACa43a7924da320c452114f7592886ea26", "71551eac6dd665468630c555aa192e8b");

            var trialNumber = new PhoneNumber("+14243894446");
            var registeredNumber = new PhoneNumber("+33753811986");

            await MessageResource.CreateAsync(
                body: message,
                from: trialNumber,
                to: registeredNumber
            );
        }

        private static async Task DeliverToStandardOutput(string message)
        {
            Console.WriteLine(message);
            await Task.CompletedTask;
        }
    }
}
