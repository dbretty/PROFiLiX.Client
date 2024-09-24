// <copyright file="IProfilixHubServices.cs" company="bretty.me.uk">
// Copyright (c) bretty.me.uk. All rights reserved.
// </copyright>

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
        void ProcessHubAction(string clientAction, string adminUserName, string connectionId, int taskId);
    }
}