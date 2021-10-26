﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using NikeFrontend.Data;
using NikeFrontend.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NikeFrontend.Pages
{
    public partial class Login
    {
        [Inject]
        public IUserService _userService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public Blazored.SessionStorage.ISessionStorageService sessionStorage { get; set; }

        private User user = new User();

        private ClaimsPrincipal claimsPrincipal;
        public string LoginMesssage { get; set; }

        private async Task<bool> CheckUser()
        {
            var returnUser = await _userService.LoginAsync(user);

            if (returnUser.succeeded)
            {
                await sessionStorage.SetItemAsync("email", user.email);
                await sessionStorage.SetItemAsync("token", returnUser.data.token);

                ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsAuthenticated(user.email);
                NavigationManager.NavigateTo("/");
            }
            else
            {
                LoginMesssage = "Tài khoản hoặc mật khấu sai";
            }

            return await Task.FromResult(true);
        }
    }
}