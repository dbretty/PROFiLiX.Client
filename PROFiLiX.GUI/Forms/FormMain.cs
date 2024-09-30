// <auto-generated/>
namespace PROFiLiX.GUI
{
    using PROFiLiX.GUI.General;
    using System.Windows.Forms;
    using PROFiLiX.GUI.Forms;
    using System.IO;
    using PROFiLiX.Common.Profile.Model;
    using PROFiLiX.Common.Configuration;
    using PROFiLiX.Common.Logging.Model;
    using PROFiLiX.Common.ApiClient;
    using PROFiLiX.Common.User.Model;
    using System.Threading.Tasks;
    using Microsoft.Win32;
    using Microsoft.AspNetCore.SignalR.Client;
    using System;
    using PROFiLiX.Common.Services;
    using Microsoft.Toolkit.Uwp.Notifications;
    using System.Linq.Expressions;
	using ProfileType = Common.User.Model.ProfileType;

	public partial class FormMain : Form
    {
        GUIElements guiElements = new GUIElements();
        IAppConfig PROFiLiX = new AppConfig();
        IProfilixHubServices hubServices;
        
        public FormMain()
        {
            this.InitializeComponent();
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            GUIElements.MinimizeApplication(this, this.NotifyMain, "PROFiLiX Client", "Starting Up", true);

            if (string.Equals(PROFiLiX.LogToServer, "Yes", StringComparison.OrdinalIgnoreCase))
            {
                var hubConnection = new HubConnectionBuilder()
                    .WithUrl(PROFiLiX.LoggingServerProtocol + "://" + PROFiLiX.LoggingServerUri + ":" + PROFiLiX.LoggingServerPort + "/PROFiLiXHub")
                    .WithAutomaticReconnect()
                    .Build();
                hubConnection.Closed += (exception) =>
                {
                    PROFiLiX.HubConnection = false;
                    this.Invoke(new MethodInvoker(delegate
                    {
                        this.lblLogToServer.Text = "Error connecting to PROFiLiX Server";
                        this.lblLogToServer.ForeColor = System.Drawing.Color.Red;
                    }));

                    GUIElements.DisplayToastNotification("PROFiLiX Server", $"Connection has failed from your endpoint, please ensure you have access.");

                    PROFiLiX.ServerOnline = false;
                    return Task.CompletedTask;
                };
                
                hubConnection.On<string, string, string, int, string, ActionType, string>("ReceiveMessage", (clientAction, adminUserName, connectionId, taskId, customTaskName, actionType, customTaskContent) =>
                {
                    this.Invoke((Delegate)(() =>
                    {
                        string actionTaken = string.Empty;
                        hubServices = new ProfilixHubServices(PROFiLiX, hubConnection);
                        hubServices.ProcessHubAction(clientAction, adminUserName, connectionId, taskId, customTaskName, actionType, customTaskContent);
                        switch (clientAction)
                        {
                            case "ClearTempFiles":
                                actionTaken = "Clear Temporary Files";
                                break;
                            case "ResetEdge":
                                actionTaken = "Reset Microsoft Edge";
                                break;
                            case "ResetChrome":
                                actionTaken = "Reset Google Chrome";
                                break;
                            case "ResetFirefox":
                                actionTaken = "Reset Mozilla Firefox";
                                break;
                            case "ResetTeamsClassic":
                                actionTaken = "Reset Teams Classic";
                                break;
                            case "ResetTeams":
                                actionTaken = "Reset Teams";
                                break;
                            case "Custom":
                                actionTaken = customTaskName;
                                break;
                            default:
                                actionTaken = "Undefined Action";
                                break;
                        }
                        GUIElements.DisplayToastNotification("PROFiLiX Server", $"{adminUserName} executed: {actionTaken} on your endpoint");
                    }));
                });

                try
                {
                    await hubConnection.StartAsync();
                    PROFiLiX.HubConnectionId = hubConnection.ConnectionId;
                    this.lblLogToServer.Text = "Connected to PROFiLiX Server";
                    this.lblLogToServer.ForeColor = System.Drawing.Color.Green;
                    if (string.Equals(PROFiLiX.LogToServer, "Yes", StringComparison.OrdinalIgnoreCase))
                    {
                        PROFiLiX.ServerOnline = true;
                    }

                    if (string.Equals(PROFiLiX.LogToServer, "Yes", StringComparison.OrdinalIgnoreCase))
                    {
                        PROFiLiX.ServerOnline = true;
                    }
                }
                catch (Exception ex)
                {
                    await PROFiLiX.Logger.LogAsync($"Error connecting to SignalR hub: {ex.Message}");
                    PROFiLiX.HubConnection = false;
                }
            }

            if (PROFiLiX.ServerOnline == true)
            {
                this.lblLogToServer.Text = "Connected to PROFiLiX Server";
                this.lblLogToServer.ForeColor = System.Drawing.Color.MediumSeaGreen;
            } 
            else
            {
                if (string.Equals(PROFiLiX.LogToServer, "No", StringComparison.OrdinalIgnoreCase))
                {
                    this.lblLogToServer.Text = "No PROFiLiX Server Configured";
                    this.lblLogToServer.ForeColor = System.Drawing.Color.CadetBlue;
                }
                else
                {
                    this.lblLogToServer.Text = "Error connecting to PROFiLiX Server";
                    this.lblLogToServer.ForeColor = System.Drawing.Color.IndianRed;
                } 
            }

            await PROFiLiX.Logger.LogAsync("Starting Application");

            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
            GUIElements.MinimizeApplication(this, this.NotifyMain, PROFiLiX.UserDetail.UserName, PROFiLiX.UserDetail.ProfileDirectory);
            EnableUi(false, "Getting user data");

            await PROFiLiX.Logger.LogAsync($"Loading profile file and folder info for: {PROFiLiX.UserDetail.UserName}");
            guiElements.ClearDataGrid(this.dgUserProfileFolders);
            var treeSizeFolders = await PROFiLiX.FilesAndFolders.BuildTreeSizeFoldersAsync(PROFiLiX.UserDetail.ProfileDirectory);
            guiElements.UpdateDataGrid(treeSizeFolders, this.dgUserProfileFolders);
            var treeSizeFiles = await PROFiLiX.FilesAndFolders.BuildTreeSizeFilesAsync(PROFiLiX.UserDetail.ProfileDirectory);
            guiElements.UpdateDataGrid(treeSizeFiles, this.dgUserProfileFolders);

            await PROFiLiX.Logger.LogAsync($"Updating profile labels for: {PROFiLiX.UserDetail.UserName}");
            guiElements.UpdateLabel(lblUserName, PROFiLiX.UserDetail.UserName);
            guiElements.UpdateLabel(lblProfileDirectory, PROFiLiX.UserDetail.ProfileDirectory);
            guiElements.UpdateLabel(lblProfileSize, PROFiLiX.UserDetail.ProfileSize);
            guiElements.UpdateLabel(lblCurrentDirectory, PROFiLiX.UserDetail.ProfileDirectory);
            guiElements.UpdateLabel(lblAppDataLocal, PROFiLiX.UserDetail.AppDataLocal);
            guiElements.UpdateLabel(lblAppDataRoaming, PROFiLiX.UserDetail.AppDataRoaming);
            guiElements.UpdateLabel(lblProfileType, PROFiLiX.UserDetail.UserProfileType.ToString());
            guiElements.SizeDataGrid(this.dgUserProfileFolders);

            EnableUi(true);
            guiElements.UpdateLabel(lblProfileSize, await PROFiLiX.UserDetail.UpdateProfileSizeAsync(this.lblProfileDirectory.Text));

            if (PROFiLiX.ServerOnline == true)
            {
                UserProfile userProfile = new UserProfile();
                userProfile.UserName = PROFiLiX.UserDetail.UserName;

                switch (PROFiLiX.UserDetail.ProfileDefinition)
                {
                    case ProfileTypeDefinition.Local:
                        userProfile.ProfileType = Common.ApiClient.ProfileType.Local;
                        break;
                    case ProfileTypeDefinition.Citrix:
                        userProfile.ProfileType = Common.ApiClient.ProfileType.CitrixProfileManager;
                        break;
                    case ProfileTypeDefinition.FSLogix:
                        userProfile.ProfileType = Common.ApiClient.ProfileType.FSLogix;
                        break;
					case ProfileTypeDefinition.Liquidware:
						userProfile.ProfileType = Common.ApiClient.ProfileType.Liquidware;
						break;
				}
                userProfile.ProfileSize = (long)PROFiLiX.UserDetail.ProfileSizeRaw;
                userProfile.ActiveStatus = true;
                userProfile.HubConnectionId = PROFiLiX.HubConnectionId;

                long tempSize = 0;
                foreach (var folder in PROFiLiX.UserProfile.TempFolders)
                {
                    var tempFolder = Path.Join(PROFiLiX.UserDetail.ProfileDirectory, folder);
                    if (Directory.Exists(tempFolder))
                    {
                        var tempHoldingLocation = await PROFiLiX.FilesAndFolders.BuildTreeSizeFoldersAsync(tempFolder);
                        foreach (var x in tempHoldingLocation)
                        {
                            tempSize = (long)(tempSize + x.RawSize);
                        }
                    }
                }
                userProfile.TempSize = tempSize;
                userProfile.ProfileDirectory = PROFiLiX.UserDetail.ProfileDirectory;
                userProfile.OperatingSystem = PROFiLiX.Device.OperatingSystem;
                userProfile.OperatingSystemVersion = PROFiLiX.Device.OperatingSystemVersion;
                userProfile.NumberOfCPUs = PROFiLiX.Device.NumberOfCPUs;
                userProfile.MemoryInMB = PROFiLiX.Device.MemoryInMB;
                userProfile.MemoryInGB = PROFiLiX.Device.MemoryInGB;
                userProfile.UserDomain = PROFiLiX.Device.UserDomain;

                if (PROFiLiX.UserId == 0)
                {
                    var result = await PROFiLiX.UserProfileClient.AddUserProfileAsync(userProfile);
                    var userId = result.Id;
                    PROFiLiX.Registry.SetRegistryValue("UserProfileId", PROFiLiX.AppRegistryKey, userId, RegistryHive.CurrentUser);
                    PROFiLiX.UserId = userId;
                } else
                {
                    userProfile.Id = PROFiLiX.UserId;
                    var result = await PROFiLiX.UserProfileClient.UpdateUserProfileAsync(userProfile);
                }
            }

            if (PROFiLiX.ClearTempAtStart == "Yes")
            {
                await PROFiLiX.Logger.LogAsync($"Running startup action: Clear Temp At Start");
                PROFiLiX.UserProfile.ExecuteAction(ProfileActionDefinition.ClearTempFiles, this.lblProfileDirectory.Text, PROFiLiX.UserProfile);
            }

            this.dgUserProfileFolders.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dgUserProfileFolders.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void NotifyMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            GUIElements.RestoreApplication(this, this.NotifyMain);
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            GUIElements.MinimizeApplication(this, this.NotifyMain, this.lblUserName.Text, this.lblProfileDirectory.Text);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = GUIElements.DisplayYesNoMessage("Are you sure you want to quit?");
            if (dialogResult == DialogResult.Yes)
            {
                PROFiLiX.Logger.LogAsync($"Stopping Application");
                ExitApplication();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = GUIElements.DisplayYesNoMessage("Are you sure you want to quit?");
            if (dialogResult == DialogResult.Yes)
            {
                PROFiLiX.Logger.LogAsync($"Stopping Application");
                ExitApplication();
            }
        }

        private void showDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIElements.RestoreApplication(this, this.NotifyMain);
        }

        private async void btnHome_Click(object sender, EventArgs e)
        {
            EnableUi(false, "Navigating to home directory");

            await PROFiLiX.Logger.LogAsync($"Navigating to home directory");

            guiElements.ClearDataGrid(this.dgUserProfileFolders);
            guiElements.UpdateLabel(lblCurrentDirectory, this.lblProfileDirectory.Text);

            await PROFiLiX.Logger.LogAsync($"Loading profile file and folder info ({PROFiLiX.UserDetail.ProfileDirectory}) for: {PROFiLiX.UserDetail.UserName}");
            var newFolders = await PROFiLiX.FilesAndFolders.BuildTreeSizeFoldersAsync(PROFiLiX.UserDetail.ProfileDirectory);
            guiElements.UpdateDataGrid(newFolders, this.dgUserProfileFolders);
            var newFiles = await PROFiLiX.FilesAndFolders.BuildTreeSizeFilesAsync(PROFiLiX.UserDetail.ProfileDirectory);
            guiElements.UpdateDataGrid(newFiles, this.dgUserProfileFolders);

            EnableUi(true);
        }

        private async void btnBack_Click(object sender, EventArgs e)
        {
            var lastFolder = this.lblCurrentDirectory.Text;
            if (lastFolder != this.lblProfileDirectory.Text)
            {
                EnableUi(false, "Navigating back a directory");
                var trimmedFolder = lastFolder.Substring(0, lastFolder.LastIndexOf("\\"));

                await PROFiLiX.Logger.LogAsync($"Navigating back a directory");

                guiElements.ClearDataGrid(this.dgUserProfileFolders);
                guiElements.UpdateLabel(lblCurrentDirectory, trimmedFolder);

                await PROFiLiX.Logger.LogAsync($"Loading profile file and folder info ({trimmedFolder}) for: {PROFiLiX.UserDetail.UserName}");
                var newFolders = await PROFiLiX.FilesAndFolders.BuildTreeSizeFoldersAsync(trimmedFolder);
                guiElements.UpdateDataGrid(newFolders, this.dgUserProfileFolders);
                var newFiles = await PROFiLiX.FilesAndFolders.BuildTreeSizeFilesAsync(trimmedFolder);
                guiElements.UpdateDataGrid(newFiles, this.dgUserProfileFolders);

                EnableUi(true);
            }
            else
            {
                await PROFiLiX.Logger.LogAsync($"User tried to navigate outside of the root profile folder directory.", LogLevel.WARNING);
                GUIElements.DisplayCriticalMessage($"You cannot roam above the root User Profile directory of {this.lblProfileDirectory.Text}");
            }
        }

        private async void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var lastFolder = this.lblCurrentDirectory.Text;
            if (lastFolder != this.lblProfileDirectory.Text)
            {
                EnableUi(false, "Navigating back a directory");
                var trimmedFolder = lastFolder.Substring(0, lastFolder.LastIndexOf("\\"));

                await PROFiLiX.Logger.LogAsync($"Navigating back a directory");

                guiElements.ClearDataGrid(this.dgUserProfileFolders);
                guiElements.UpdateLabel(lblCurrentDirectory, trimmedFolder);

                await PROFiLiX.Logger.LogAsync($"Loading profile file and folder info ({trimmedFolder}) for: {PROFiLiX.UserDetail.UserName}");
                var newFolders = await PROFiLiX.FilesAndFolders.BuildTreeSizeFoldersAsync(trimmedFolder);
                guiElements.UpdateDataGrid(newFolders, this.dgUserProfileFolders);
                var newFiles = await PROFiLiX.FilesAndFolders.BuildTreeSizeFilesAsync(trimmedFolder);
                guiElements.UpdateDataGrid(newFiles, this.dgUserProfileFolders);

                EnableUi(true);
            }
            else
            {
                await PROFiLiX.Logger.LogAsync($"User tried to navigate outside of the root profile folder directory.", LogLevel.WARNING);
                GUIElements.DisplayCriticalMessage($"You cannot roam above the root User Profile directory of {this.lblProfileDirectory.Text}");
            }
        }

        private async void drilldownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgUserProfileFolders.CurrentCell.ColumnIndex == 0)
            {
                var currentValue = dgUserProfileFolders.CurrentCell.Value.ToString();
                if (currentValue.IndexOf('\\') > 0)
                {
                    EnableUi(false, "Drilldown to folder");

                    await PROFiLiX.Logger.LogAsync($"Drilldown to folder");

                    guiElements.ClearDataGrid(this.dgUserProfileFolders);
                    guiElements.UpdateLabel(lblCurrentDirectory, currentValue);

                    await PROFiLiX.Logger.LogAsync($"Loading profile file and folder info ({currentValue}) for: {PROFiLiX.UserDetail.UserName}");
                    var newFolders = await PROFiLiX.FilesAndFolders.BuildTreeSizeFoldersAsync(currentValue);
                    guiElements.UpdateDataGrid(newFolders, this.dgUserProfileFolders);
                    var newFiles = await PROFiLiX.FilesAndFolders.BuildTreeSizeFilesAsync(currentValue);
                    guiElements.UpdateDataGrid(newFiles, this.dgUserProfileFolders);

                    EnableUi(true);
                }
                else
                {
                    await PROFiLiX.Logger.LogAsync($"User tried to drilldown into a file ({currentValue}).", LogLevel.WARNING);
                    GUIElements.DisplayCriticalMessage("Cannot drilldown into a file");
                }
            }
        }

        private void btnProfileDetail_Click(object sender, EventArgs e)
        {
            FormDetail formDetail = new FormDetail(PROFiLiX.UserDetail.UserProfileType, PROFiLiX.UserDetail.ProfileDefinition, this.lblUserName.Text, this.lblProfileDirectory.Text);
            PROFiLiX.Logger.LogAsync($"Showing profile details form for: {PROFiLiX.UserDetail.UserName}");
            formDetail.ShowDialog();
        }

        private async void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgUserProfileFolders.CurrentCell.ColumnIndex == 0)
            {
                var folderToDelete = this.dgUserProfileFolders.CurrentCell.Value;
                DialogResult dialogResult = GUIElements.DisplayYesNoMessage($"Are you sure you want to delete {folderToDelete}?");
                if (dialogResult == DialogResult.Yes)
                {
                    EnableUi(false, "Deleting file or folder");

                    ProfilixTask task = new ProfilixTask();
                    var taskId = 0;

                    if (PROFiLiX.ServerOnline == true)
                    {
                        task.TaskName = $"Deleting folder: {(string)folderToDelete}";
                        task.UserName = PROFiLiX.UserDetail.UserName;
                        task.TaskState = ProfilixTaskState.Running;

                        var result = await PROFiLiX.TaskClient.AddProfilixTaskAsync(task);
                        taskId = result.Id;
                    }

                    string selectedItem = (string)folderToDelete;
                    if (selectedItem.IndexOf('\\') > 0)
                    {
                        await PROFiLiX.Logger.LogAsync($"Deleting folder: {(string)folderToDelete}");
                        await PROFiLiX.FilesAndFolders.DeleteFolderAsync((string)folderToDelete);
                    }
                    else
                    {
                        var fileToDelete = Path.Combine(
                            this.lblCurrentDirectory.Text,
                            selectedItem);
                        await PROFiLiX.Logger.LogAsync($"Deleting file: {fileToDelete}");
                        await PROFiLiX.FilesAndFolders.DeleteFileAsync(fileToDelete);
                    }

                    await PROFiLiX.UserDetail.UpdateProfileSizeAsync(PROFiLiX.UserDetail.ProfileDirectory);
                    guiElements.UpdateLabel(lblProfileSize, PROFiLiX.UserDetail.ProfileSize);

                    guiElements.ClearDataGrid(this.dgUserProfileFolders);

                    await PROFiLiX.Logger.LogAsync($"Loading profile file and folder info ({this.lblCurrentDirectory.Text}) for: {PROFiLiX.UserDetail.UserName}");
                    var newFolders = await PROFiLiX.FilesAndFolders.BuildTreeSizeFoldersAsync(this.lblCurrentDirectory.Text);
                    guiElements.UpdateDataGrid(newFolders, this.dgUserProfileFolders);
                    var newFiles = await PROFiLiX.FilesAndFolders.BuildTreeSizeFilesAsync(this.lblCurrentDirectory.Text);
                    guiElements.UpdateDataGrid(newFiles, this.dgUserProfileFolders);

                    if (PROFiLiX.ServerOnline == true)
                    {
                        var currentTask = await PROFiLiX.TaskClient.GetSingleProfilixTaskAsync(taskId);
                        currentTask.TaskState = ProfilixTaskState.Completed;
                        await PROFiLiX.TaskClient.UpdateProfilixTaskAsync(currentTask);
                    }

                    EnableUi(true);
                }
            }
        }

        private void EnableUi(bool enabled, string labelText = "Ready")
        {
            if (enabled)
            {
                this.pbMain.MarqueeAnimationSpeed = 0;
                this.pbMain.Refresh();
            }
            else
            {
                this.pbMain.MarqueeAnimationSpeed = 100;
                this.pbMain.Refresh();
            }

            this.btnExit.Enabled = enabled;
            this.btnMinimize.Enabled = enabled;
            this.btnBack.Enabled = enabled;
            this.btnHome.Enabled = enabled;
            this.btnProfileDetail.Enabled = enabled;
            this.lblStatus.Text = labelText;
        }

        private async void dgFoldersDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgUserProfileFolders.CurrentCell.ColumnIndex == 0)
            {
                var currentValue = dgUserProfileFolders.CurrentCell.Value.ToString();
                if (currentValue.IndexOf('\\') > 0)
                {
                    EnableUi(false, "Drilldown to folder");

                    await PROFiLiX.Logger.LogAsync($"Drilldown to folder");

                    guiElements.ClearDataGrid(this.dgUserProfileFolders);
                    guiElements.UpdateLabel(lblCurrentDirectory, currentValue);

                    await PROFiLiX.Logger.LogAsync($"Loading profile file and folder info ({currentValue}) for: {PROFiLiX.UserDetail.UserName}");
                    var newFolders = await PROFiLiX.FilesAndFolders.BuildTreeSizeFoldersAsync(currentValue);
                    guiElements.UpdateDataGrid(newFolders, this.dgUserProfileFolders);
                    var newFiles = await PROFiLiX.FilesAndFolders.BuildTreeSizeFilesAsync(currentValue);
                    guiElements.UpdateDataGrid(newFiles, this.dgUserProfileFolders);

                    EnableUi(true);
                }
                else
                {
                    await PROFiLiX.Logger.LogAsync($"User tried to drilldown into a file ({currentValue}).", LogLevel.WARNING);
                    GUIElements.DisplayCriticalMessage("Cannot drilldown into a file");
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FormSettings formSettings = new FormSettings(this);
            PROFiLiX.Logger.LogAsync($"Showing application settings screen");
            formSettings.ShowDialog();
        }
        private async void ExitApplication()
        {
            if (PROFiLiX.ServerOnline == true)
            {
                UserProfile userProfile = (UserProfile)await PROFiLiX.UserProfileClient.GetSingleUserProfileAsync(PROFiLiX.UserId);
                userProfile.ActiveStatus = false;
                var result = await PROFiLiX.UserProfileClient.UpdateUserProfileAsync(userProfile);
            }
            Application.Exit();
        }
    }
}
