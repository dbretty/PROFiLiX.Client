﻿// <copyright file="ProfilixHubServices.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

namespace PROFiLiX.Common.Services
{
    using Microsoft.AspNetCore.SignalR.Client;
    using PROFiLiX.Common.Configuration;
    using PROFiLiX.Common.Profile.Model;

    /// <summary>
    /// Class to execute profile buddy hub actions.
    /// </summary>
    public class ProfilixHubServices : IProfilixHubServices
    {
        private readonly IAppConfig appConfig;
        private readonly HubConnection hubConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilixHubServices"/> class.
        /// </summary>
        /// <param name="appConfig">The app config to pass in.</param>
        /// <param name="hubConnection">The hub connection to pass in.</param>
        public ProfilixHubServices(IAppConfig appConfig, HubConnection hubConnection)
        {
            this.appConfig = appConfig;
            this.hubConnection = hubConnection;
        }

        /// <inheritdoc/>
        public async void ProcessHubAction(string clientAction, string adminUserName, string connectionId, int taskId)
        {
            if (clientAction == "ClearTempFiles")
            {
                if (this.appConfig.UserDetail.ProfileDirectory is not null)
                {
                    this.appConfig.UserProfile.ExecuteAction(ProfileActionDefinition.ClearTempFiles, this.appConfig.UserDetail.ProfileDirectory, this.appConfig.UserProfile);
                }

                await this.hubConnection.InvokeAsync("ReceiveMessageFromClient", $"{this.appConfig.UserDetail.UserName} has finished cleaning temporary data", taskId);
            }

            if (clientAction == "ResetEdge")
            {
                if (this.appConfig.UserDetail.ProfileDirectory is not null)
                {
                    this.appConfig.UserProfile.ExecuteAction(ProfileActionDefinition.ResetEdge, this.appConfig.UserDetail.ProfileDirectory, this.appConfig.UserProfile);
                }

                await this.hubConnection.InvokeAsync("ReceiveMessageFromClient", $"{this.appConfig.UserDetail.UserName} has finished resetting Microsoft Edge", taskId);
            }

            if (clientAction == "ResetChrome")
            {
                if (this.appConfig.UserDetail.ProfileDirectory is not null)
                {
                    this.appConfig.UserProfile.ExecuteAction(ProfileActionDefinition.ResetChrome, this.appConfig.UserDetail.ProfileDirectory, this.appConfig.UserProfile);
                }

                await this.hubConnection.InvokeAsync("ReceiveMessageFromClient", $"{this.appConfig.UserDetail.UserName} has finished resetting Google Chrome", taskId);
            }

            if (clientAction == "ResetFirefox")
            {
                if (this.appConfig.UserDetail.ProfileDirectory is not null)
                {
                    this.appConfig.UserProfile.ExecuteAction(ProfileActionDefinition.ResetFirefox, this.appConfig.UserDetail.ProfileDirectory, this.appConfig.UserProfile);
                }

                await this.hubConnection.InvokeAsync("ReceiveMessageFromClient", $"{this.appConfig.UserDetail.UserName} has finished resetting Mozilla Firefox", taskId);
            }

            if (clientAction == "ResetTeamsClassic")
            {
                if (this.appConfig.UserDetail.ProfileDirectory is not null)
                {
                    this.appConfig.UserProfile.ExecuteAction(ProfileActionDefinition.ResetTeamsv1, this.appConfig.UserDetail.ProfileDirectory, this.appConfig.UserProfile);
                }

                await this.hubConnection.InvokeAsync("ReceiveMessageFromClient", $"{this.appConfig.UserDetail.UserName} has finished resetting Microsoft Teams Classic", taskId);
            }

            if (clientAction == "ResetTeams")
            {
                if (this.appConfig.UserDetail.ProfileDirectory is not null)
                {
                    this.appConfig.UserProfile.ExecuteAction(ProfileActionDefinition.ResetTeamsv2, this.appConfig.UserDetail.ProfileDirectory, this.appConfig.UserProfile);
                }

                await this.hubConnection.InvokeAsync("ReceiveMessageFromClient", $"{this.appConfig.UserDetail.UserName} has finished resetting Microsoft Teams", taskId);
            }
        }
    }
}
