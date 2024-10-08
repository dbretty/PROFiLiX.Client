﻿// <copyright file="IProfilixHubServices.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

using PROFiLiX.Common.ApiClient;

namespace PROFiLiX.Common.Services
{
    /// <summary>
    /// Interface to execute profile buddy hub actions.
    /// </summary>
    public interface IProfilixHubServices
    {
        /// <summary>
        /// Executes Process Action.
        /// </summary>
        /// <param name="clientAction">The action to take.</param>
        /// <param name="adminUserName">The admin user name.</param>
        /// <param name="connectionId">The connection Idy.</param>
        /// <param name="taskId">The Server Side task Id.</param>
        /// <param name="customTaskName">The custom task name.</param>
        /// <param name="actionType">The action type.</param>
        /// <param name="customTaskContent">The Custom Action Code.</param>
        void ProcessHubAction(string clientAction, string adminUserName, string connectionId, int taskId, string customTaskName, ActionType actionType, string customTaskContent);
    }
}