using Domain.Entities;
using Domain.Exceptions;
using EmailSender.Models;
using EmailSender.Services.Abstraction;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using System.Web;

namespace EmailSender.Services
{
    public sealed class EmailService : IEmailService
    {
        private readonly UserManager<User>? _userManager;
        private readonly SmtpSettings? _smtpSettings;
        private readonly IUrlHelper? _urlHelper;
        private readonly HttpRequest? _request;
        public EmailService(
            UserManager<User>? userManager, 
            IOptions<SmtpSettings>? smtpSettings, 
            IUrlHelper urlHelper,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _smtpSettings = smtpSettings.Value;
            _urlHelper = urlHelper;
            _request = httpContextAccessor.HttpContext.Request;
        }
        public async Task<bool> SendConfirmationEmailAsync(string? to)
        {
            if (string.IsNullOrEmpty(to))
            {
                throw new ArgumentNullException(nameof(to));
            }

            var user = await _userManager.FindByEmailAsync(to);

            if (await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new EmailAlreadyConfirmedException("Your email is already confirmed");
            }

            var confirmationCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var code = UrlEncode(confirmationCode);

            var callBackUrl = CreateConfirmEmailCallBackUrl(to, code);

            var message = CreateMessage(to, "Confirm your email", $"Please confirm your email by clicking this link: {callBackUrl}.");

            var result = await SendMessageAsync(message);

            return result != false;
        }

        public async Task<bool> ConfirmEmailAsync(string? email, string? code)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    throw new ArgumentNullException(nameof(code));
                }

                code = UrlDecode(code);

                var user = await _userManager.FindByEmailAsync(email);

                IdentityResult result = await _userManager.ConfirmEmailAsync(user, code);

                if (!result.Succeeded)
                {
                    return false;
                }

                var message = CreateMessage(email, "Email Confirmed", "Your email has been successfully confirmed.");

                await SendMessageAsync(message);

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> SendChangeEmailMessageAsync(string? userId, string? newEmail)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (string.IsNullOrEmpty(newEmail))
            {
                throw new ArgumentNullException(nameof(newEmail));
            }

            var user = await _userManager.FindByIdAsync(userId);

            var confirmationCode = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);

            var code = UrlEncode(confirmationCode);

            var callBackUrl = CreateChangeEmailCallBackUrl(userId, newEmail, code);

            var message = CreateMessage(newEmail, "Confirm your email", $"Please confirm your email by clicking this link: {callBackUrl}.");

            var result = await SendMessageAsync(message);

            return result != false;
        }

        public async Task<bool> ChangeEmailAsync(string? userId, string? newEmail, string? code)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    throw new ArgumentNullException(nameof(code));
                }

                code = UrlDecode(code);

                var user = await _userManager.FindByIdAsync(userId);

                IdentityResult result = await _userManager.ChangeEmailAsync(user, newEmail, code);

                if (!result.Succeeded)
                {
                    return false;
                }

                var message = CreateMessage(newEmail, "Email Changed", "Your email has been successfully changed.");

                await SendMessageAsync(message);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string CreateChangeEmailCallBackUrl(string? userId, string? newEmail, string? code)
        {
            var callBackUrl = _urlHelper.Action(new UrlActionContext()
            {
                Action = "ChangeEmail",
                Controller = "Email",
                Protocol = _request.Scheme,
                Values = new { userId, newEmail, code = code }
            });

            return callBackUrl;
        }

        private string CreateConfirmEmailCallBackUrl(string? email, string? code)
        {
            var callBackUrl = _urlHelper.Action(new UrlActionContext()
            {
                Action = "ConfirmEmail",
                Controller = "Email",
                Protocol = _request.Scheme,
                Values = new { email, code }
            });

            return callBackUrl;
        }
        private MimeMessage CreateMessage(string? to, string? subject, string? body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_smtpSettings.DisplayName, _smtpSettings.SenderEmail));
            emailMessage.To.Add(MailboxAddress.Parse(to));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart()
            {
                Text = body
            };

            return emailMessage;
        }

        private async Task<bool> SendMessageAsync(MimeMessage message)
        {
            var client = new SmtpClient();

            try
            {
                await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, true);
                await client.AuthenticateAsync(new NetworkCredential(_smtpSettings.SenderEmail, _smtpSettings.Password));
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                client.Dispose();
            }
        }
        private static string UrlEncode(string? code)
        {
            return HttpUtility.UrlEncode(code).Replace("%", "-");
        }

        private static string UrlDecode(string? code)
        {
            return HttpUtility.UrlDecode(code.Replace("-", "%"));
        }
    }
}
