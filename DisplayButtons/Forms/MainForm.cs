
using NHotkey;
using NHotkey.WindowsForms;

using ScribeBot;
using SharpAdbClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using McMaster.NETCore.Plugins;

using Swan;
using WebSocketSharp;
using Misc;
using NetSparkleUpdater;
using BackendAPI.Objects;
using BackendAPI.Networking.Implementation;
using DisplayButtons.BackendAPI.Objects;
using BackendAPI.Utils;
using DisplayButtons.Bibliotecas;
using NickAc.ModernUIDoneRight.Objects;
using BackendAPI.Events;
using DisplayButtons.Helpers;
using static DisplayButtons.Helpers.DeckHelpers;
using BackendAPI.DeckText;
using BackendAPI.Objects.Implementation;
using BackendAPI.Objects.Implementation.DeckActions.General;
using DisplayButtons.Controls;
using BackendAPI.Objects.Implementation.DeckActions.Deck;
using NickAc.ModernUIDoneRight.Controls;
using BackendAPI.Networking;
using DisplayButtons.Properties;
using BackendAPI;
using BackendProxy;
using NetSparkleUpdater.SignatureVerifiers;
using DisplayButtons.Bibliotecas.DeckEvents;
using DisplayButtons.Forms.EventSystem;
using static BackendAPI.Objects.AbstractDeckAction;
using DisplayButtons.Bibliotecas.Helpers.ObjectsHelpers;
using BackendAPI.Sdk;


#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body

namespace DisplayButtons.Forms
{

    public partial class MainForm : TemplateForm
    {
        private static MainForm instance;

        public static MainForm Instance
        {
            get
            {
                return instance;
            }
        }
        static Timer timer = new Timer();
        private const int CLIENT_ARRAY_LENGHT = 1024 * 50;

        #region Constructors

        private static SparkleUpdater _sparkle { get; set; }
        public MainForm()
        {

            instance = this;
            InitializeComponent();

            //Globals.launcher_principal = this;
        

            DevicesTitlebarButton item = new DevicesTitlebarButton(this);
            TitlebarButtons.Add(item);
            if (Program.Silent)
            {
                //Right now, we use the window redraw for device discovery purposes.
                //We need to simulate that with a timer.
                Timer t = new Timer
                {
                    //We should run it every 2 seconds and half.
                    Interval = 2000
                };
                t.Tick += (s, e) =>
                {
                    //The discovery works by reading the Text from the button


                    item.RefreshCurrentDevices();






                };

                void handler(object s, EventArgs e)
                {
                    Hide();
                    Shown -= handler;
                }

                Shown += handler;

                t.Start();


               
              
            }
            ColorSchemeCentral.ThemeChanged += (s, e) =>
            {
                ApplySidebarTheme(shadedPanel1);
            };
            BackendProxy.Wrapper.events.On("languagechanged", (e) => {


                MainForm.Instance.Invoke(new Action(() =>
                {
                    this.Text = Texts.rm.GetString("APPLICATIONNAME", Texts.cultereinfo);
                    perfil_info.Text = Texts.rm.GetString("PERFILINFOLABEL", Texts.cultereinfo);
                    warning_label.Text = Texts.rm.GetString("WARNINGLABELTEXT", Texts.cultereinfo);
                    deckoptions_button.Text = Texts.rm.GetString("BUTTONDECKMISCELLANEOUS", Texts.cultereinfo);
                    RefreshDeveloperMode();
                   // reloadALL();
                }));

            });
            BackendProxy.Wrapper.events.On("DeckFolderEvent", (e) => {
                // Cast event argrument to your event object
                var obj = (DeckFolderEvent)e;
                GlobalHotKeys.refreshFolder(obj._folder);
                // Get (set) your event object data
               // DeckHelpers.RefreshButton(obj._item, obj._mode, obj._item.GetDeckLayerTwo, obj._device);
                // Other code
            });
            BackendProxy.Wrapper.events.On("IDeckInterfaceEvent", (e) => {
                // Cast event argrument to your event object
                var obj = (IDeckInterfaceEvent)e;

                // Get (set) your event object data
                DeckHelpers.RefreshButton(obj._item, obj._mode, obj._item.GetDeckLayerTwo, obj._device);
                // Other code
            });

            BackendProxy.Wrapper.events.On("DeckFolderEventCreate", (e) => {
                // Cast event argrument to your event object
                var obj = (DeckFolderEventCreate)e;

                // Get (set) your event object data
                GlobalHotKeys.RegisterUniqueId(obj._folder);

                // Other code
            });

            BackendProxy.Wrapper.events.On("DeckFolderEventDelete", (e) => {
                // Cast event argrument to your event object
                var obj = (DeckFolderEventDelete)e;

                // Get (set) your event object data
                GlobalHotKeys.RemoveHotKey(obj._folder);

                // Other code
            });

        }

        public void ChangeButtonsVisibility(bool visible)
        {
            visible = true;
            if (Disposing || !IsHandleCreated) return;
            Invoke(new Action(() =>
            {
                Control control2 = Controls["label1"];
                if (control2 != null) control2.Visible = !visible;

                Control control = Controls["panel1"];
                if (control != null) control.Visible = visible;
              //  Control control3 = Controls["shadedPanel1"];
              //  if (control3 != null) control3.Visible = visible;

            }));
        }



        #endregion

        #region Properties


        public DeckDevice CurrentDevice { get; set; }

        #endregion

        #region Methods
        public bool DeviceTest = false;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

          


            using (frmWaitForm frm = new frmWaitForm(StartupMethods))
            {
                frm.ShowDialog(this);
            }

            //warning_label.ForeColor = ColorScheme.SecondaryColor;
           
        }
  
        public void StartupMethods()
        {

         
            StartUsbMode();


            ChangeDeveloperMode();
       


         // ProfileStaticHelper.SetupPerfil();

            //       DeckServiceProvider.StartTimers();

            MainForm.Instance.Invoke(new Action(() =>
            {
                // teste
              
                link.Click += (sender, e) =>
                {
                    if (MessageBox.Show(Texts.rm.GetString("ABOUTINFOLINKMESSAGEMESSAGE", Texts.cultereinfo), Texts.rm.GetString("ABOUTINFOLINKMESSAGETITLE", Texts.cultereinfo), MessageBoxButtons.OKCancel) == DialogResult.OK)

                    {

                        //Do something here if OK was clicked.


                        Process myProcess = new Process();

                        try
                        {
                            // true is the default, but it is important not to set it to false
                            myProcess.StartInfo.UseShellExecute = true;
                            myProcess.StartInfo.FileName = "http://displaybuttons.com";
                            myProcess.Start();
                        }
                        catch (Exception ee)
                        {
                            Console.WriteLine(ee.Message);
                        }
                    }

                };
                info.Text = Texts.rm.GetString("ABOUTINFOLABELTEXT", Texts.cultereinfo);
                info.Click += (sender, e) =>
                {

                    new About().ShowDialog();

                };
                imageModernButton6.Text = Texts.rm.GetString("EVENTSYSTEMBUTTON", Texts.cultereinfo);
                if (ApplicationSettingsManager.Settings.isAutoMinimizer) { 
                    NotifyIcon icon = new NotifyIcon
                {
                    Icon = Icon,
                    Text = Text
                };
                icon.DoubleClick += (sender, e) =>
                {
                    Show();
                };
                ContextMenuStrip menu = new ContextMenuStrip();
                menu.Items.Add(Texts.rm.GetString("RESIZETEXTOPEN", Texts.cultereinfo)).Click += (s, e) =>
                {
                    Show();
                };
                menu.Items.Add("-");
                menu.Items.Add(Texts.rm.GetString("RESIZETEXTCLOSE", Texts.cultereinfo)).Click += (s, e) =>
                {
                    Application.Exit();
                };
                FormClosing += (s, e) =>
                {
                   
                        if (e.CloseReason == CloseReason.UserClosing)
                        {
                            Hide();
                            e.Cancel = true;
                        }
                        else if (e.CloseReason == CloseReason.ApplicationExitCall)
                        {
                            icon.Visible = false;
                            icon.Dispose();
                        }
               
                };
                menu.Opening += (s, e) =>
                {
                    menu.Items[0].Select();
                };
                icon.ContextMenuStrip = menu;
                icon.Visible = true;
 }


                var image = ColorScheme.ForegroundColor == Color.White ? Resources.ic_settings_white_48dp_2x : Resources.ic_settings_black_48dp_2x;
                var imageTrash = ColorScheme.ForegroundColor == Color.White ? Resources.ic_delete_white_48dp_2x : Resources.ic_delete_black_48dp_2x;
                var imagePlugins = ColorScheme.ForegroundColor == Color.White ? Resources.Package_16x : Resources.Package_16x;
                var imageBiblioteca = ColorScheme.ForegroundColor == Color.White ? Resources.folder_white : Resources.folder_black;
                var imageMiscelanius = ColorScheme.ForegroundColor == Color.White ? Resources.drawer__archive__files__documents__office_white : Resources.drawer__archive__files__documents__office_white;
                var imageUpdate = ColorScheme.ForegroundColor == Color.White ? Resources.download_arrow_white : Resources.download_arrow_black;

                AppAction item = new AppAction()
                {
                    Image = image
                };
              
                item.Click += (s, ee) =>
                {
              
                   

                        new SettingsForm().ShowDialog();
                    };



                AppAction autoupdate = new AppAction()
                {
                    Image = imageUpdate
                };
                autoupdate.Click += async (s, ee) =>
                {
                    //TODO: Settings

                    _sparkle = new SparkleUpdater(
       Globals.updateurl, // link to your app cast file
       new Ed25519Checker(NetSparkleUpdater.Enums.SecurityMode.Unsafe) // your base 64 public key -- generate this with the NetSparkleUpdater.Tools AppCastGenerator on any OS
    )
                    {


                        UIFactory = new NetSparkleUpdater.UI.WinForms.UIFactory(Resources.button_deck) // or null or choose some other UI factory or build your own!

                    };

                    var _updateInfo = await _sparkle.CheckForUpdatesQuietly();
                     var list = _sparkle.AppCastHandler.GetAvailableUpdates(true);

                    
                   var form = _sparkle.UIFactory.CreateAllReleaseDownloadList(_sparkle, list, false, true);

                    form.HideRemindMeLaterButton();
                    form.HideSkipButton();

                    form.Show(true);



                    //   AutoUpdater.LetUserSelectRemindLater = false;
                    //     AutoUpdater.ShowUpdateForm(AutoUpdater.arg);
                    //  _sparkle.CheckForUpdatesAtUserRequest();
                    //AutoUpdater.Start(Globals.updateurl);
                };


                AppAction itemTrash = new AppAction()
                {
                    Image = imageTrash
                };
                itemTrash.Click += (s, ee) =>
                {
                    if (CurrentDevice == null) return;
                    if (MessageBox.Show("Are you sure you  want to clear everything?" + Environment.NewLine + "All items will be lost!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                      
                        DevicePersistManager.ActualDevice.CurrentProfile.Mainfolder = new DynamicDeckFolder();
                        SendItemsToDevice(CurrentDevice, true);
                        RefreshAllButtons(false);
                    }
                };
                appBar1.Actions.Add(autoupdate);

                appBar1.Actions.Add(itemTrash);
                appBar1.Actions.Add(item);
            




                AppAction itemBiblioteca = new AppAction()
                {
                    Image = imageBiblioteca
                };

                itemBiblioteca.Click += (s, ee) =>
                {
                    new ImageListForm().ShowDialog();
                    //     new ScribeBot.Interface.Window().Show();
                };

               // Globals.calc = ApplicationSettingsManager.Settings.coluna * ApplicationSettingsManager.Settings.linha;

             

            //    appBar1.Actions.Add(itemBiblioteca);
 
                // ApplyTheme(panel1);
                GenerateSidebar(shadedPanel1, true);
                DevicePersistManager.LoadDevices();

                //   ApplySidebarTheme(painel_developer);


                Refresh();



                FillPerfil();
           

                    ProfileStaticHelper.SelectItemByValue(perfilselector, ProfileStaticHelper.SelectPerfilByName(ApplicationSettingsManager.Settings.CurrentProfile)); 

           

            if (!ApplicationSettingsManager.Settings.isFolderBrowserEnabled)
            {
                shadedPanel4.Visible = false;
                }
                else
                {
                    ApplySidebarTheme(shadedPanel4);
                }
                }));
          ApplySidebarTheme(shadedPanel1);
            Texts.initilizeLang();
  FactoryEvents.Init();
 Checkupdates();
            ChangeDebuggerrMode();
            
        }
        private void Checkupdates()
        {

            string TargetDirectory = Assembly.GetExecutingAssembly().Location;
            _sparkle = new SparkleUpdater(
      Globals.updateurl, // link to your app cast file
      new Ed25519Checker(NetSparkleUpdater.Enums.SecurityMode.Unsafe) // your base 64 public key -- generate this with the NetSparkleUpdater.Tools AppCastGenerator on any OS
  )
            {

            
                UIFactory = new NetSparkleUpdater.UI.WinForms.UIFactory(Resources.button_deck) // or null or choose some other UI factory or build your own!

            };

           // _sparkle.RelaunchAfterUpdate = true;
      //      _sparkle.CustomInstallerArguments = @"/c WZUNZIP.EXE - ye - o " + _sparkle.TmpDownloadFilePath + " " + TargetDirectory;
            _sparkle.StartLoop(true,true); // `true` to run an initial check online -- only call StartLoop once for a given SparkleUpdater instance!

        }
        public void ExtractFileToDirectory(string zipFileName, string outputDirectory)
        {
  
        }
        public void setupLanguage()
        {
       

        }
        public void RegisterMainFolderHotKey()
        {

            string value1 = string.Join("+", ApplicationSettingsManager.Settings.keyMainFolder.ModifierKeys.Where(c => c != Keys.None).Select(c => c.ToString()).OrderBy(c => c));
            string value2 = string.Join("+", ApplicationSettingsManager.Settings.keyMainFolder.Keys.Where(c => !(c == Keys.ShiftKey || c == Keys.ControlKey || c == Keys.Menu)));
            string resultMainFolder = string.IsNullOrEmpty(value1) ? value2 : string.IsNullOrEmpty(value2) ? value1 : (string.Join("+", value1, value2));
            resultMainFolder = resultMainFolder.Replace("Control", "Ctrl");

            System.Windows.Forms.Keys retval = System.Windows.Forms.Keys.None;

            if (!resultMainFolder.IsNullOrEmpty())            {
                try
                {
                    System.Windows.Forms.KeysConverter kc = new System.Windows.Forms.KeysConverter();
                    retval = (System.Windows.Forms.Keys)kc.ConvertFromInvariantString(resultMainFolder);

                    HotkeyManager.Current.AddOrReplace("main_folder", retval, MyEventHandler);


                }
                catch (Exception)
                {
                    //Debug.(ex.ToString());
                }
            }


        }
        public void RegisterBackFolderHotKey()
        {

            string value1 = string.Join("+", ApplicationSettingsManager.Settings.keyBackFolder.ModifierKeys.Where(c => c != Keys.None).Select(c => c.ToString()).OrderBy(c => c));
            string value2 = string.Join("+", ApplicationSettingsManager.Settings.keyBackFolder.Keys.Where(c => !(c == Keys.ShiftKey || c == Keys.ControlKey || c == Keys.Menu)));
            string resultBackFolder = string.IsNullOrEmpty(value1) ? value2 : string.IsNullOrEmpty(value2) ? value1 : (string.Join("+", value1, value2));
            resultBackFolder = resultBackFolder.Replace("Control", "Ctrl");

            System.Windows.Forms.Keys retval = System.Windows.Forms.Keys.None;

            if (!resultBackFolder.IsNullOrEmpty())
            {
                try
                {
                    System.Windows.Forms.KeysConverter kc = new System.Windows.Forms.KeysConverter();
                    retval = (System.Windows.Forms.Keys)kc.ConvertFromInvariantString(resultBackFolder);

                    HotkeyManager.Current.AddOrReplace("back_folder", retval, MyEventHandler);


                }
                catch (Exception)
                {
                    //Debug.(ex.ToString());
                }
            }


        }
        public void MyEventHandler(object sender, HotkeyEventArgs e)
        {
            switch (e.Name)
            {
                case "main_folder":
                    CurrentDevice.CurrentProfile.Currentfolder = CurrentDevice.CurrentProfile.Mainfolder;
                    RefreshAllButtons(true);
                    break;
                case "back_folder":
                    if (CurrentDevice.CurrentProfile.Currentfolder.GetParent() != null)
                    {
                        CurrentDevice.CurrentProfile.Currentfolder = CurrentDevice.CurrentProfile.Currentfolder.GetParent();
                        RefreshAllButtons(true);
                    }
                    break;



            }

            e.Handled = true;
        }

        public void RefreshDeveloperMode()
        {

            if (Disposing || !IsHandleCreated) return;
            Invoke(new Action(() =>
            {

                Control control = Controls.Find("painel_developer", true).FirstOrDefault() as ShadedPanel;
                control.Refresh();

            }));

        }
        public void ChangeDeveloperMode()
        {

            if (Disposing || !IsHandleCreated) return;
            Invoke(new Action(() =>
            {

                Control control = Controls.Find("painel_developer", true).FirstOrDefault() as ShadedPanel;
                if (control != null)
                {
                    control.Visible = ApplicationSettingsManager.Settings.isDevelopermode;
                }

            }));

        }
        public void ChangeDebuggerrMode()
        {

            if (Disposing || !IsHandleCreated) return;
            Invoke(new Action(() =>
            {

                Control control = Controls.Find("debugger_tools", true).FirstOrDefault() as ShadedPanel;
                if (control != null)
                {
                    
                    control.Visible = Debugger.IsAttached;

                    
                }

            }));

        }

        const string OFFLINE_PREFIX = "[OFFLINE]";
        public new Padding Padding = new Padding(5);
       
  
        public void StartUsbMode()
        {

            DevicePersistManager.DeviceConnected += DevicePersistManager_DeviceConnected;

            DevicePersistManager.DeviceDisconnected += DevicePersistManager_DeviceDisconnected;


        }
        public void ApplySidebarTheme(Control parent)
        {
            //Headers have the theme's secondary color as background
            //and the theme's foreground color as text color
            ApplicationColorScheme appTheme = ColorSchemeCentral.FromAppTheme(ApplicationSettingsManager.Settings.Theme);
            parent.Controls.OfType<Control>().All((c) =>
            {
                if (c.Tag != null && c.Tag.ToString().ToLowerInvariant() == "header")
                {
                    c.BackColor = appTheme.SecondaryColor;
                    c.ForeColor = appTheme.ForegroundColor;
                }
              
                else
                {
                    c.BackColor = appTheme.BackgroundColor;
                }
                return true;
            });
        }
 
        public void ApplyTheme(Control parent)
        {

            //IMPORTANT
            ApplicationColorScheme appTheme = ColorSchemeCentral.FromAppTheme(ApplicationSettingsManager.Settings.Theme);
            parent.Controls.OfType<Control>().All((c) =>
            {
                if (c is ImageModernButton mb)
                {
                    mb.AllowDrop = true;
                    mb.DragEnter += (s, ee) =>
                    {
                        if (mb.Tag != null && mb.Tag is IDeckFolder folder && !(ee.Data.GetDataPresent(typeof(DeckItemMoveHelper))))
                        {
                            CurrentDevice.CurrentProfile.Currentfolder = folder;
                            RefreshAllButtons(true);
                        }
                        else if (mb.Tag != null && CurrentDevice.CurrentProfile.Currentfolder != null && CurrentDevice.CurrentProfile.Currentfolder.GetParent() != null  && mb.Tag is IDeckItem upItem && upItem is DynamicBackItem)
                        {
                            CurrentDevice.CurrentProfile.Currentfolder = CurrentDevice.CurrentProfile.Currentfolder.GetParent();
                            RefreshAllButtons(true);
                        }

                        if (ee.Data.GetDataPresent(typeof(DeckActionHelper)))
                            ee.Effect = DragDropEffects.Copy;
                        else if (ee.Data.GetDataPresent(typeof(DeckItemMoveHelper)))
                        {
                            var item = ee.Data.GetData(typeof(DeckItemMoveHelper)) as DeckItemMoveHelper;
                            ee.Effect = item.CopyOld ? DragDropEffects.Copy : DragDropEffects.Move;
                        }

                    };
                    mb.DragDrop += (s, ee) =>
                    {
                        if (ee.Data.GetData(typeof(DeckActionHelper)) is DeckActionHelper action)
                        {
                            if (ee.Effect == DragDropEffects.Copy)
                            {

                                if (mb.Tag != null && mb.Tag is IDeckItem item)
                                {


                                //    if (CurrentDevice.CurrentProfile.Currentfolder.GetParent() != null && item is DynamicBackItem) return;
                                    if (item is IDeckFolder deckFolder)
                                    {
                                        var deckItemToAdd = new DynamicDeckItem
                                        {
                                            DeckAction = action.DeckAction.CloneAction()
                                        };
                                        var id2 = deckFolder.Add(deckItemToAdd,CurrentDevice.CurrentProfile);

                                        deckItemToAdd.GetDeckDefaultLayer.DeckImage = new DeckImage(action.DeckAction.GetDefaultItemImage()?.Bitmap ?? Resources.img_item_default);

                                        CurrentDevice.CurrentProfile.Currentfolder = deckFolder;
                                        RefreshAllButtons();
                                        //implementação dos plugins

                                        if (mb.Tag is DynamicDeckItem DDT && DDT.DeckAction is PluginLuaGenerator PG)
                                        {
                                            LoadPropertiesPlugins(DDT, action.ToScript, action.ToExecute, action.ToName, action.dllpath);

                                            PG.SetConfigs(action.dllpath);

                                        }
                                        if (mb.Tag is DynamicDeckItem DLLLDYNAMIC && DLLLDYNAMIC.DeckAction is DllWrapper)
                                        {
                                            LoadDll(DLLLDYNAMIC, action.plugin,action.ToName);
                                        }
                                        if (mb.Tag is DynamicDeckItem DDTTWO && DDTTWO.DeckAction.setLayer() == true)
                                        {
                                            DDTTWO.DeckAction.setLayer(mb.CurrentSlot, DDTTWO);
                                        }
                                        FocusItem(GetButtonControl(id2), deckItemToAdd);

                                        return;
                                    }
                                    var folder = new DynamicDeckFolder();

                                    folder.GetDeckDefaultLayer.DeckImage = new DeckImage(Resources.img_folder);

                                    //Create a new folder instance
                                    CurrentDevice.CheckCurrentFolder();
                                    folder.ParentFolder = CurrentDevice.CurrentProfile.Currentfolder;
                                    folder.Add(1, folderUpItem);
                                    folder.Add(item, CurrentDevice.CurrentProfile);

                                    var newItem = new DynamicDeckItem();

                                    newItem.DeckAction = action.DeckAction.CloneAction();
                                    newItem.GetDeckDefaultLayer.DeckImage = new DeckImage(action.DeckAction.GetDefaultItemImage()?.Bitmap ?? Resources.img_item_default);


                                    var id = folder.Add(newItem,CurrentDevice.CurrentProfile);

                                    FocusItem(GetButtonControl(id), newItem);

                                    CurrentDevice.CurrentProfile.Currentfolder.Add(mb.CurrentSlot, folder);

                                    mb.Tag = folder;
                                    mb.Image = Resources.img_folder;
                                    CurrentDevice.CurrentProfile.Currentfolder = folder;
                                    if (mb.Tag is DynamicDeckItem mycustomitem && mycustomitem.DeckAction.setLayer() == true)
                                    {
                                        mycustomitem.DeckAction.setLayer(mb.CurrentSlot, mycustomitem);
                                    }

                                    if (mb.Tag is DynamicDeckItem TT && TT.DeckAction is PluginLuaGenerator YY)
                                    {
                                        LoadPropertiesPlugins(TT, action.ToScript, action.ToExecute, action.ToName, action.dllpath);

                                        YY.SetConfigs(action.dllpath);

                                    }
                                    if (mb.Tag is DynamicDeckItem DDIDLL && DDIDLL.DeckAction is DllWrapper)
                                    {
                                        LoadDll(DDIDLL, action.plugin,action.ToName);
                                    }
                                    RefreshAllButtons();
                                }
                                else
                                {
                                    mb.Tag = new DynamicDeckItem
                                    {
                                        DeckAction = action.DeckAction.CloneAction()
                                    };
                                    mb.Image = ((DynamicDeckItem)mb.Tag).DeckAction.GetDefaultItemImage()?.Bitmap ?? Resources.img_item_default;

                                    FocusItem(mb, mb.Tag as IDeckItem);
                                    if (mb.Tag is DynamicDeckItem TT && TT.DeckAction is PluginLuaGenerator YY)
                                    {
                                        LoadPropertiesPlugins(TT, action.ToScript, action.ToExecute, action.ToName,action.dllpath);
                                   
                                            YY.SetConfigs(action.dllpath);

                                    }
                                    if (mb.Tag is DynamicDeckItem DDIDLL && DDIDLL.DeckAction is DllWrapper)
                                    {
                                        LoadDll(DDIDLL, action.plugin,action.ToName);
                                    }

                                    if (mb.Tag is DynamicDeckItem mycustomitem && mycustomitem.DeckAction != null && mycustomitem.DeckAction.setLayer() == true)
                                    {
                                        mycustomitem.DeckAction.setLayer(mb.CurrentSlot, mycustomitem);
                                    }
                                }
                            }
                        }
                        else if (ee.Data.GetDataPresent(typeof(DeckItemMoveHelper)))
                        {
                            var action1 = ee.Data.GetData(typeof(DeckItemMoveHelper)) as DeckItemMoveHelper;
                            bool shouldMove = ee.Effect == DragDropEffects.Move;
                            if (shouldMove)
                            {
                                action1.OldFolder.Remove(action1.OldSlot);
                                if (action1.OldFolder.GetParent() != null)
                                {
                                    var oldItems = action1.OldFolder.GetDeckItems();
                                    bool isEmpty = action1.OldFolder.GetParent() != null ? oldItems.Count == 1 : oldItems.Count == 0;
                                    int slot = action1.OldFolder.GetParent().GetItemIndex(action1.OldFolder);
                                    if (isEmpty)
                                    {
                                        action1.OldFolder.GetParent().Remove(slot);
                                           
                                            RefreshButton(slot);
                                   //     ClearSingleItemToDevice(CurrentDevice,slot);
                                    }
                                }
                            }
                            IDeckItem item1 = shouldMove ? action1.DeckItem : action1.DeckItem.DeepClone();
                            if (item1 is IDeckFolder folder && !shouldMove)
                            {
                                FixFolders(folder, false, CurrentDevice.CurrentProfile.Currentfolder);
                            }
                            //alterando agora

                            if (CurrentDevice.CurrentProfile.Currentfolder.GetDeckItems().Any(cItem => CurrentDevice.CurrentProfile.Currentfolder.GetItemIndex(cItem) == mb.CurrentSlot))
                            {
                                //We must create a folder if there is an item
                                var oldItem = action1.OldFolder.GetDeckItems().First(cItem => action1.OldFolder.GetItemIndex(cItem) == mb.CurrentSlot);

                                var newFolder = new DynamicDeckFolder();

                                newFolder.GetDeckDefaultLayer.DeckImage = new DeckImage(Resources.img_folder);
                                if (oldItem is DynamicDeckFolder deckFolder)
                                {
                                    //mesclagem de pasta
                                    CurrentDevice.CheckCurrentFolder();
                                    deckFolder.ParentFolder = CurrentDevice.CurrentProfile.Currentfolder;
                                    deckFolder.Add(1, folderUpItem);
                                    
                                    deckFolder.Add(action1.DeckItem, CurrentDevice.CurrentProfile);
                                    CurrentDevice.CurrentProfile.Currentfolder.Add(mb.CurrentSlot, deckFolder);

                                   // CurrentDevice.CurrentProfile.Currentfolder = deckFolder;

                                }
                                else
                                {
                                    // mesclagem de itens. Se há um item em cima do outro, cria-se uma pasta.
                                    CurrentDevice.CheckCurrentFolder();
                                    newFolder.ParentFolder = CurrentDevice.CurrentProfile.Currentfolder;
                                    newFolder.Add(1, folderUpItem);

                                    newFolder.Add(oldItem,CurrentDevice.CurrentProfile);
                                    newFolder.Add(action1.DeckItem, CurrentDevice.CurrentProfile);
                                    CurrentDevice.CurrentProfile.Currentfolder.Add(mb.CurrentSlot, newFolder);
                                    CurrentDevice.CurrentProfile.Currentfolder = newFolder;
                                 
                                   Wrapper.events.Trigger("DeckFolderEventCreate", new DeckFolderEventCreate(newFolder));

                                    // chamada o registrador de pasta
                                }




                                RefreshAllButtons(true);

                            }
                            else
                            {
                                CurrentDevice.CurrentProfile.Currentfolder.Add(mb.CurrentSlot, item1);
                                RefreshButton(action1.OldSlot, true);
                              //  ClearSingleItemToDevice(CurrentDevice, action1.OldSlot);
                                RefreshButton(mb.CurrentSlot, true);
                                
                                if (mb.Tag is DynamicDeckItem mycustomitem && mycustomitem.DeckAction != null && mycustomitem.DeckAction.setLayer() == true)
                                { 
                                    mycustomitem.DeckAction.setLayer(mb.CurrentSlot,mycustomitem);
                                   // FocusItem(mb, mycustomitems
                                }

                            
                            }

                        }
                    };
                    //  mb.Text = string.Empty;
                    mb.ColorScheme = ColorScheme;
                }
                c.BackColor = appTheme.BackgroundColor;
                return true;
            });

        }

     


        public void MatrizGenerator(Profile profile)
        {

        
            panel_buttons.Controls.Clear();
            //warning_label.Visible = true;
         //   warning_label.Text = Texts.rm.GetString("BUTTONRELOADALL", Texts.cultereinfo);
       //     panel1.Visible = false;
            Invoke(new Action(() =>
            {
                List<Control> toAdd = new List<Control>();

             
            
                int id = 1;
                for (int con = 0; con < profile.Matriz.Lin; con++)
                {
                    for (int lin = 0; lin < profile.Matriz.Column; lin++)
                    {





                        ImageModernButton control = new ImageModernButton();


                        control.Name = "modernButton" + id;

                    
                       
                            control.Size = new Size(80, 80);
                            control.Location = new Point(lin * 120, con * 120);
                        
                        
                        id += 1;
                        control.MouseUp += (sender, e) =>
                        {
                            mouseDown = false;
                            var popupMenu = new ContextMenuStrip();





                            if (sender is ImageModernButton senderB)
                            {
                                if ( e.Button == MouseButtons.Right && DevicePersistManager.IsDeviceTest && ModifierKeys == Keys.Control)
                                {
                                    Debug.WriteLine("Ativando botão esquerdo");
                                    if (senderB.Tag != null && senderB.Tag is DynamicDeckItem item)
                                    {
                                        item.DeckAction?.OnButtonUp(CurrentDevice);
                                    }
                                    return;
                                }


                                if (!senderB.DisplayRectangle.Contains(e.Location)) return;
                                if (e.Button == MouseButtons.Right && CurrentDevice.CurrentProfile.Currentfolder.GetDeckItems().Any(c => CurrentDevice.CurrentProfile.Currentfolder.GetItemIndex(c) == senderB.CurrentSlot))
                                {


                                    popupMenu.Items.Add("Remove item").Click += (s, ee) =>
                                    {
                                        if (senderB != null)
                                        {
                                            if (senderB.Image != Resources.img_folder && senderB.Image != Resources.img_item_default)
                                            {
                                                senderB.Image.Dispose();
                                            }
                                            if (senderB.Tag is DynamicDeckFolder item)
                                            {



                                                Wrapper.events.Trigger("DeckFolderEventDelete", new DeckFolderEventDelete(item));


                                            }
                                            senderB.Tag = null;
                                            senderB.Image = null;
                                            Buttons_Unfocus(sender, e);


                                            CurrentPerfil.Value.Currentfolder.Remove(senderB.CurrentSlot);


                                        }
                                    };

                                    popupMenu.Items.Add("Clear image").Click += (s, ee) =>
                                    {
                                        if (senderB.Image != null && senderB.Image != Resources.img_folder && senderB.Image != Resources.img_item_default)
                                        {
                                            senderB.Image.Dispose();
                                            if (senderB != null && senderB.Tag != null && senderB.Tag is IDeckItem deckItem)
                                            {
                                                bool isFolder = deckItem is IDeckFolder;
                                                senderB.Image = isFolder ? Resources.img_folder : ((IDeckItem)senderB.Tag).GetDeckDefaultLayer.GetDefaultImage()?.Bitmap ?? Resources.img_item_default;
                                            }
                                        }
                                    };


                                    popupMenu.Show(sender as Control, e.Location);



                                }


                                return;
                            }

                        };

                            control.MouseDown += (sender, e) =>
                        {
                            if (sender is ImageModernButton senderB)
                            {
                                if ( e.Button == MouseButtons.Left && DevicePersistManager.IsDeviceTest && ModifierKeys == Keys.Control)
                                {
                                    if (senderB.Tag != null && senderB.Tag is DynamicDeckItem item)
                                    {
                                        item.DeckAction?.OnButtonDown(CurrentDevice);
                                    }
                                    return;
                                }
                            }


                            mouseDown = e.Button == MouseButtons.Left;
                            mouseDownLoc = Cursor.Position;
                            if (e.Button == MouseButtons.Right)
                            {

                                if (sender is ImageModernButton senderT)
                                {
                                    if (senderT.Tag is DynamicDeckItem == false)
                                    {
                                        ContextMenuStrip menu = new ContextMenuStrip();
                                        menu.Items.Add("Adicionar pasta").Click += (s, ee) =>
                                        {

                                            if (sender is ImageModernButton mb)
                                            {
                                                Debug.WriteLine("Adicionando pasta...");
                                                CurrentDevice.CheckCurrentFolder() ;
                                                var newFolder = new DynamicDeckFolder();

                                                var DeckImage = newFolder.GetDeckDefaultLayer.DeckImage = new DeckImage(Resources.img_folder);


                                                newFolder.ParentFolder = CurrentDevice.CurrentProfile.Currentfolder;
                                                newFolder.Add(1, folderUpItem);

                                                CurrentDevice.CurrentProfile.Currentfolder.Add(mb.CurrentSlot, newFolder);
                                                //FolderCallBack(newFolder);
                                                Wrapper.events.Trigger("DeckFolderEventCreate", new DeckFolderEventCreate(newFolder));

                                                //CurrentDevice.CurrentProfile.Currentfolder = newFolder;
                                                RefreshAllButtons(true);

                                            }
                                        };


                                        menu.Items.Add("Atualizar").Click += (s, ee) =>
                                        {

                                            if (sender is ImageModernButton mb)
                                            {
                                                    //CurrentDevice.CurrentProfile.Currentfolder = newFolder;
                                                    RefreshAllButtons(true);

                                            }
                                        };


                                        menu.Show(sender as Control, e.Location);
                                    }
                                }
                            }
                        };

                        control.MouseMove += (sender, e) =>
                        {
                            if (!mouseDown) return;
                            int distanceX = Math.Abs(mouseDownLoc.X - Cursor.Position.X);
                            int distanceY = Math.Abs(mouseDownLoc.Y - Cursor.Position.Y);
                            IDeckFolder folder = CurrentDevice?.CurrentProfile.Currentfolder;

                            var finalPoint = new Point(distanceX, distanceY);
                            bool didMove = SystemInformation.DragSize.Width * 2 > finalPoint.X && SystemInformation.DragSize.Height * 2 > finalPoint.Y;
                            if (didMove && !finalPoint.IsEmpty)
                            {
                                mouseDown = false;
                                if (sender is ImageModernButton mb)
                                {
                                    if (mb.Tag != null && mb.Tag is IDeckItem act)
                                    {
                                        bool isDoubleClick = lastClick.ElapsedMilliseconds != 0 && lastClick.ElapsedMilliseconds <= SystemInformation.DoubleClickTime;
                                        if (isDoubleClick) return;
                                      //  if ((CurrentDevice.CurrentProfile.Currentfolder.GetParent() != null && (mb.CurrentSlot == 1))) return;
                                        mb.DoDragDrop(new DeckItemMoveHelper(act, CurrentDevice.CurrentProfile.Currentfolder, mb.CurrentSlot) { CopyOld = ModifierKeys.HasFlag(Keys.Control) }, ModifierKeys.HasFlag(Keys.Control) ? DragDropEffects.Copy : DragDropEffects.Move);
                                            //if (act is DynamicDeckItem dI && dI.DeckAction != null)
                                            //{
                                            //    Label title_control = Controls.Find("titleLabel" + folder.GetItemIndex(act), true).FirstOrDefault() as Label;

                                            //    title_control.Text = dI.DeckAction.GetActionName();
                                            //    Debug.WriteLine("Clicando no " + dI.DeckAction.GetActionName());
                                            //}
                                        }
                                }
                            }
                        };


                        control.Click += (sender, e) =>
                        {
                      

                            lastClick.Stop();
                       
                            bool isDoubleClick = lastClick.ElapsedMilliseconds != 0 && lastClick.ElapsedMilliseconds <= DeckHelpers.GetDoubleClickTime();
                            
                            if (sender is ImageModernButton mb)
                            {
                                if (mb.Tag != null && mb.Tag is IDeckItem item)
                                {
                                    if (item is IDeckFolder folder)
                                    {
                                        if (!isDoubleClick)
                                        {

                                            FocusItem(mb, item);
                                            camada1.PerformClick();
                                            // camada1.PerformClick();
                                            goto end;
                                        }
                                            //Navigate to the folder
                                            CurrentDevice.CurrentProfile.Currentfolder = folder;
                                        RefreshAllButtons(true);
                                        goto end;
                                    }else if (item is DynamicBackItem)
                                    
                                   {
                                   
                                        if (CurrentDevice.CurrentProfile.Currentfolder.GetParent() != null)
                                        {
                                            //Not on the main folder
                                            if (!isDoubleClick)
                                            {

                                                FocusItem(mb, item);
                                                camada1.PerformClick();
                                                goto end;
                                                // camada1.PerformClick();
                                               
                                            }
                                            CurrentDevice.CurrentProfile.Currentfolder = CurrentDevice.CurrentProfile.Currentfolder.GetParent();
                                                RefreshAllButtons(true);



                                                goto end;
                              
                                            }
                                        
                                       
                                    }
                                        //Show button panel with settable properties
                                        FocusItem(mb, item);
                                    camada1.PerformClick();

                                    lastClick.Reset();
                                }
                                else
                                {
                                    Buttons_Unfocus(sender, e);
                                }
                                return;
                                end:
                               lastClick.Reset();
                               lastClick.Start();


                                
                           RefreshButton(mb.CurrentSlot);
 }
                        };
                            //    control.Controls.Add(control2);




                            toAdd.Add(control);


                        if (toAdd.Count >= profile.Matriz.Calc)
                        {

                            toAdd.AsEnumerable().Reverse().All(m =>
                            {
                                panel_buttons.Controls.Add(m);

                                toAdd.Remove(m);

                                return true;
                            });
                          
                           Globals.can_refresh = true;
                          //  panel1.Visible = true;
                            //warning_label.Visible = false;
                            ApplyTheme(panel_buttons);
                            RefreshAllButtons(true);

                                //            break;

                            }

                           Application.DoEvents();

                            //      Refresh();
                        }



                }

            }));

            // Application.DoEvents();

        }
        
        public void RefreshAllButtons(bool sendToDevice = true)
        {
            if(Globals.can_refresh == false)
            {
                return;
            }
         

            // Buttons_Unfocus(this, EventArgs.Empty);
            IDeckFolder folder = CurrentDevice?.CurrentProfile?.Currentfolder;


            for (int j = 0; j < CurrentDevice.CurrentProfile.Matriz.Calc; j++)
            {
                ImageModernButton control = GetButtonControl(j + 1);
                //  Label control2 = GetLabelControl(j + 1);
                /// control2.Text = null;
                //control2.Tag = null; 
                control.NormalImage = null;
                control.Tag = null;
                control.Camada = 1;
               control.ClearText();
                if (folder == null) control.Invoke(new Action(control.Refresh));
            }
            if (folder == null) return;
            for (int i = 0; i < folder.GetDeckItems().Count; i++)
            {
                IDeckItem item = null;
                item = folder.GetDeckItems()[i];
                ImageModernButton control = Controls.Find("modernButton" + folder.GetItemIndex(item), true).FirstOrDefault() as ImageModernButton;

                // Label control2 = Controls.Find("label" + folder.GetItemIndex(item), true).FirstOrDefault() as Label;

                //     var ser = item.GetItemImage().BitmapSerialized;

                if (item is DynamicDeckItem FF && FF.DeckAction is PluginLuaGenerator)
                {
                    LoadPropertiesPlugins(FF, GetPluginScript(GetPropertiesPlugins(FF, "ScriptNamePoint")));
                }
                if (item is DynamicDeckItem GG && GG.DeckAction is DllWrapper)
                {
                    LoadDll(GG, GetPluginDll(GetPropertiesPlugins(GG, "name")));
                }



                //control.TextLabel(item?.DeckName, this.Font, Brushes.Black, new PointF(25, 3));

                control.NormalImage = item?.GetDeckDefaultLayer?.GetItemImage()?.Bitmap;
                
                control.TextButton = new TextLabel(item.GetDeckDefaultLayer);

                control.Tag = item;
                control.Invoke(new Action(control.Refresh));

                CurrentDevice.CheckCurrentFolder();
              

            }

        
            if (sendToDevice)
                {

          SendItemsToDevice(CurrentDevice, CurrentDevice.CurrentProfile.Currentfolder);

                }

        }
        public void RefreshButton(int slot, bool sendToDevice = true)
        {
           // Buttons_Unfocus(this, EventArgs.Empty);

            IDeckFolder folder = CurrentDevice?.CurrentProfile.Currentfolder;
            ImageModernButton control1 = GetButtonControl(slot);
            //Label control_label = GetLabelControl(slot);
            // Label title_control = Controls.Find("titleLabel" + slot, true).FirstOrDefault() as Label;

            control1.NormalImage = null;
            control1.Tag = null;
            control1.Text = "";
            control1.Camada = 1;
            control1.ClearText();
            if (!DevicePersistManager.IsDeviceTest)
            {
                DeckHelpers.ClearSingleItemToDevice(CurrentDevice, slot);

            }
 

            if (folder == null) control1.Invoke(new Action(control1.Refresh));

            if (folder == null) return;
            for (int i = 0; i < folder.GetDeckItems().Count; i++)
            {
                IDeckItem item = null;
                item = folder.GetDeckItems()[i];

                if (folder.GetItemIndex(item) != slot) continue;
                ImageModernButton control = Controls.Find("modernButton" + folder.GetItemIndex(item), true).FirstOrDefault() as ImageModernButton;
                
            
            
                if (item != null)
                {
           

                    control.NormalImage = item?.GetDeckDefaultLayer?.GetItemImage()?.Bitmap;
                    control.Text = item?.GetDeckDefaultLayer.Deckname;
                    control.TextButton = new TextLabel(item.GetDeckDefaultLayer);
                    control.Tag = item;
                    control.Invoke(new Action(control.Refresh));
 CurrentDevice.CheckCurrentFolder();
                    if (DeviceTest)
                    {
                        return;
                    }
                    if (sendToDevice)
            {
                        DeckHelpers.SendSingleItemToDevice(CurrentDevice, slot, item.GetDeckDefaultLayer);

            }


                }
             
            }
           
    
        }
   

            public ImageModernButton GetButtonControl(int id)
        {
            return Controls.Find("modernButton" + id, true).FirstOrDefault() as ImageModernButton;
        }
        private Label GetLabelControl(int id)
        {
            return Controls.Find("label" + id, true).FirstOrDefault() as Label;
        }
        private void Buttons_Unfocus(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                shadedPanel2.Hide();
                shadedPanel1.Refresh();
                Refresh();
            }));
        }






        public void StartLoad(bool restart)
        {

            //            DevicePersistManager.DeviceConnected += DevicePersistManager_DeviceConnected;

            // DevicePersistManager.DeviceDisconnected += DevicePersistManager_DeviceDisconnected;

            DevicesTitlebarButton item = new DevicesTitlebarButton(this);

            foreach (var device in DevicePersistManager.PersistedDevices.ToList())
            {




            }


            //    TitlebarButtons.Add(item);
            //   timer.Start();
            //Right now, we use the window redraw for device discovery purposes.
            //We need to simulate that with a timer.

        }

        public static void DeviceAdbConnected(object sender, DeviceDataEventArgs e)
        {
            Thread.Sleep(1500);
            try
            {
                var receiver = new ConsoleOutputReceiver();

                Program.client.ExecuteRemoteCommand("pm path net.robertocpaes.displaybuttons", e.Device, receiver);

                if (receiver != null)
                {
                    var product_name = new ConsoleOutputReceiver();
                    var product_manufacter = new ConsoleOutputReceiver();

                    if (String.IsNullOrEmpty(e.Device.Model))
                    {
                        Program.client.ExecuteRemoteCommand("getprop ro.product.name", e.Device, product_name);
                        Program.client.ExecuteRemoteCommand("getprop ro.product.manufacturer", e.Device, product_manufacter);



                        e.Device.Model = product_name.ToString().TrimEnd(new char[] { '\r', '\n' }); ;
                        e.Device.Product = product_manufacter.ToString().TrimEnd(new char[] { '\r', '\n' }); ;
                    }

                    Program.device_list.Add(e.Device);
                    //       Console.WriteLine($"The device {e.Device.Name} has connected to this PC");

                    Console.WriteLine("STARTING SERVER ON SMARTPHONE...");


                }
            }
            catch(Exception ee)
            {

            }


        }
        public static void DeviceAdbDisconnected(object sender, DeviceDataEventArgs e)
        {
            Console.WriteLine("Device desconectado.....");
            try
            {
                Program.device_list.Remove(e.Device);
            }
            catch (Exception) { }

            //   timer.Stop();
        }
        public void Start_configs()
        {

             if (Debugger.IsAttached && DevicePersistManager.IsDeviceTest)
                {

                    ProfileTestDeckHelper.SelectCurrentDevicePerfil(CurrentPerfil.Value, DevicePersistManager.DeviceTest);
                return;  
            }
            if (perfilselector.Items.Count == 0)
            {
                ProfileStaticHelper.SetupPerfil();
            }
                if (CurrentPerfil != null)
                {
               
                
                    ProfileStaticHelper.SelectCurrentDevicePerfil(CurrentPerfil.Value);
                
                }

         
            if (ApplicationSettingsManager.Settings.isFolderBrowserEnabled)
            {
                GenerateFolderList(shadedPanel4);
            }

          
           
       
            
        }
        public void DevicePersistManager_DeviceConnected(object sender, DevicePersistManager.DeviceEventArgs e)
        {

            Invoke(new Action(() =>
            {
                shadedPanel1.Show();
                //GenerateFolderList(shadedPanel1);
                shadedPanel2.Hide();
                Refresh();
                e.Device.CheckCurrentFolder();
                FixFolders(e.Device);
                if (CurrentDevice == null)
                {
                    ChangeToDevice(e.Device);
                }          

               Start_configs(); 
    SendItemsToDevice(CurrentDevice, true);
       
            
            }));

            e.Device.ButtonInteraction += Device_ButtonInteraction;
        }


        public void ChangeToDevice(DeckDevice device)
        {

    
            CurrentDevice = device;
            ProfileStaticHelper.SetupPerfil();

            if (CurrentDevice.CurrentProfile != null)
            {
                LoadItems(CurrentDevice.CurrentProfile.Currentfolder);
            }
            RegisterMainFolderHotKey();

            RegisterBackFolderHotKey();
            GlobalHotKeys.UpdateAllFoldersHotkeys();
        }

        //List<Tuple<Guid, int>> ignoreOnce = new List<Tuple<Guid, int>>();
        private void Device_ButtonInteraction(object sender, DeckDevice.ButtonInteractionEventArgs e)
        {
            if (sender is DeckDevice device)
            {
                /*if (ignoreOnce.Any(c => c.Item1 == device.DeviceGuid && c.Item2 == e.SlotID)) {
                    ignoreOnce.Remove(ignoreOnce.First(c => c.Item1 == device.DeviceGuid && c.Item2 == e.SlotID));
                    return;
                }*/
                var currentItems = device.CurrentProfile.Currentfolder.GetDeckItems();
                if (currentItems.Any(c => device.CurrentProfile.Currentfolder.GetItemIndex(c) == e.SlotID + 1))
                {
                    var item = currentItems.FirstOrDefault(c => device.CurrentProfile.Currentfolder.GetItemIndex(c) == e.SlotID + 1);
                    if (item is DynamicDeckItem deckItem && !(item is IDeckFolder))
                    {
                        if (device.CurrentProfile.Currentfolder.GetParent() != null)
                        {
                            if (item is DynamicBackItem backItem)
                            {
                                if (e.PerformedAction != ButtonInteractPacket.ButtonAction.ButtonUp) return;
                                //Navigate one up!
                                device.CurrentProfile.Currentfolder = device.CurrentProfile.Currentfolder.GetParent();
                                SendItemsToDevice(CurrentDevice, device.CurrentProfile.Currentfolder);
                                //      AddWatermark(deckItem.DeckAction.GetActionName(), ((IDeckItem)imageModernButton1.Origin.Tag).GetDefaultImage().Bitmap, "Arial", 7, 20f, 67f, Brushes.White, item, device.CurrentProfile.Currentfolder.GetParent());

                                this.Invoke(new Action(() => RefreshAllButtons(false)));
                                return;
                            }
                        }
                        if (deckItem.DeckAction != null)
                        {
                            switch (e.PerformedAction)
                            {
                                case ButtonInteractPacket.ButtonAction.ButtonDown:
                                    deckItem.DeckAction.OnButtonDown(device);
                                    break;

                                case ButtonInteractPacket.ButtonAction.ButtonUp:
                                    deckItem.DeckAction.OnButtonUp(device);
                                    break;
                            }
                        }
                    }
                    else if (item is DynamicDeckFolder deckFolder && e.PerformedAction == ButtonInteractPacket.ButtonAction.ButtonUp)
                    {
                        device.CurrentProfile.Currentfolder = deckFolder;
                        //ignoreOnce.Add(new Tuple<Guid, int>(device.DeviceGuid, e.SlotID));
                        SendItemsToDevice(CurrentDevice, deckFolder);
                        //  AddWatermark(deckFolder.folder_name, ((IDeckItem)imageModernButton1.Origin.Tag).GetDefaultImage().Bitmap, "Arial", 7, 20f, 67f, Brushes.White, item, deckFolder);
                       this.Invoke(new Action(() => RefreshAllButtons(false)));
                    }
                }
            }
        }

        private static void SendItemsToDevice(DeckDevice device, bool destroyCurrent = false)
        {
            if (device.CurrentProfile != null)
            {
                if (destroyCurrent) device.CurrentProfile.Currentfolder = null;
                device.CheckCurrentFolder();
                SendItemsToDevice(device, device.CurrentProfile.Currentfolder);
            }
        }


        public static void SendItemsToDevice(DeckDevice device, IDeckFolder folder)
        {



            var con = device.GetConnection();
            if (con != null)
            {

                var packet = new SlotUniversalChangeChunkPacket();

                List<IDeckItem> items = folder.GetDeckItems();

                List<int> addedItems = new List<int>();
              
                for (int i = 0; i < MainForm.Instance.CurrentDevice.CurrentProfile.Matriz.Calc; i++)
                {
                    IDeckItem item = null;
                    if (items.ElementAtOrDefault(i) != null)
                    {
                        item = items[i];
                        addedItems.Add(folder.GetItemIndex(item));
                    }

                    if (item == null) break;

              //    bool isFolder = item is IDeckFolder;
              //     var image = item.GetDeckDefaultLayer.GetItemImage() ?? item.GetDeckDefaultLayer.GetDefaultImage() ?? (new DeckImage(isFolder ? Resources.img_folder : Resources.img_item_default));
                //    var seri = image.BitmapSerialized;

                  //  item.GetDeckDefaultLayer.DeckImage = image;

                    packet.AddToQueue(folder.GetItemIndex(item), item.GetDeckDefaultLayer);

                }

                con.SendPacket(packet);
                //  con.SendPacket(packet_label);
                //    con.SendPacket(packet_label);
                var clearPacket = new SlotImageClearChunkPacket();
                //  var clearPacket_labels = new SlotLabelButtonClearChunkPacket();
                for (int i = 1; i < MainForm.Instance.CurrentDevice.CurrentProfile.Matriz.Calc + 1; i++)
                {
                    if (addedItems.Contains(i))    continue;
                     
                    //packet_label.ClearPacket();
                    clearPacket.AddToQueue(i);
                    //     clearPacket_labels.AddToQueue(i);
                }


                con.SendPacket(clearPacket);

                //    con.SendPacket(clearPacket_labels);
            }

        }

        private void LoadItems(IDeckFolder folder)
        {
            if (Globals.can_refresh == false)
            {

                return;
            }
            List<IDeckItem> items = folder.GetDeckItems();
            foreach (var item in items)
            {
               

                bool isFolder = item is IDeckFolder;
                ImageModernButton control = Controls.Find("modernButton" + folder.GetItemIndex(item), true).FirstOrDefault() as ImageModernButton;
                var image = item.GetDeckDefaultLayer.GetItemImage() ?? item.GetDeckDefaultLayer.GetDefaultImage() ?? (new DeckImage(isFolder ? Resources.img_folder : Resources.img_item_default));
                var seri = image.BitmapSerialized;
                if (item is DynamicDeckItem DI && DI.DeckAction != null)
                {

 DI.DeckAction.setLayer(control.CurrentSlot, DI);
                }
              
                //
                control.NormalImage = image.Bitmap;


                control.Refresh();
                //control.Refresh();
                control.Tag = item;
            }
        }

        private void FixFolders(DeckDevice device)
        {
            if (DevicePersistManager.HasPerfilCreated(device))
            {
                FixFolders(device.CurrentProfile.Mainfolder);
            }
        }

        private static DeckImage defaultDeckImage = new DeckImage(Resources.img_folder_up);
        private static DynamicBackItem folderUpItem = new DynamicBackItem { GetLayerOneImage  = defaultDeckImage };

        private void FixFolders(IDeckFolder folder, bool ignoreFirst = true, IDeckFolder trueParent = null)
        {
            if (!ignoreFirst)
            {
                if (trueParent != null)
                    folder.SetParent(trueParent);
                if (folder.GetParent() != null)
                {
                    folder.Add(1, folderUpItem);
                }
            }

            folder.GetSubFolders().All(c =>
            {
                FixFolders(c);
                c.SetParent(folder);
                if (c.GetParent() != null)
                {
                    c.Add(1, folderUpItem);
                }

                return true;
            });
        }

        private void UnfixFolders(IDeckFolder folder)
        {
            folder.GetSubFolders().All(c =>
            {
                UnfixFolders(c);
                c.SetParent(folder);
                if (c.GetParent() != null)
                {
                    c.Remove(1);
                }

                return true;
            });
        }

        private void DevicePersistManager_DeviceDisconnected(object sender, DevicePersistManager.DeviceEventArgs e)
        {

            if (e.Device.DeviceGuid == CurrentDevice.DeviceGuid)
            {


                if (!DevicePersistManager.IsVirtualDeviceConnected) CurrentDevice = null;


                //Try to find a new device to be the current one.
                if (DevicePersistManager.PersistedDevices.Any(d => DevicePersistManager.IsDeviceConnected(d.DeviceGuid)))
                {
                    CurrentDevice = DevicePersistManager.PersistedDevices.First(d => DevicePersistManager.IsDeviceConnected(d.DeviceGuid));
                    if (IsHandleCreated && !Disposing)
                        Invoke(new Action(() =>
                        {
                            shadedPanel1.Show();
                            Buttons_Unfocus(sender, EventArgs.Empty);

                            e.Device.CheckCurrentFolder();
                            FixFolders(e.Device);

                            RefreshAllButtons(false);
                        }));

                }
            }
            if (IsHandleCreated && !Disposing)
                Invoke(new Action(() =>
                {
                    shadedPanel2.Hide();
                    shadedPanel1.Hide();
                    panel_buttons.Controls.Clear();
                    Refresh();
                }));

            e.Device.ButtonInteraction -= Device_ButtonInteraction;
        }

        public class folders
        {
            private string name;
            private IDeckFolder parent;

            private bool is_father;

            private int folder_id;
            
            public folders(string name, IDeckFolder parent, bool is_father, int folder_id)
            {
                this.name = name;
                this.parent = parent;

                this.is_father = is_father;
                this.folder_id = folder_id;
            }

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public bool Is_father
            {
                get { return is_father; }
                set { is_father = value; }
            }

            public int Folder_id
            {
                get { return folder_id; }
                set { folder_id = value; }
            }

            public IDeckFolder Parent
            {
                get { return parent; }
                set { parent = value; }
            }


        }
        public static List<folders> folder_form = new List<folders>();
        public static List<IDeckFolder> folder_mode = new List<IDeckFolder>();
        public static List<DynamicDeckFolder> folder_globals_keys = new List<DynamicDeckFolder>();
     
        public static string pasta = "";

        public static List<int> additems_fold = new List<int>();
        public static List<IDeckFolder> items_fold = new List<IDeckFolder>();

       
        int root = 0;
        private void NextFolder()
        {


        }

        private void AddFolderInPanelList(int value)
        {
            Control parent = shadedPanel4;

            Padding categoryPadding = new Padding(5, 0, 0, 0);
            Font categoryFont = new Font(parent.Font.FontFamily, 13, FontStyle.Bold);
            Padding itemPadding = new Padding(25, 0, 0, 0);
            Font itemFont = new Font(parent.Font.FontFamily, 12);



            ApplicationColorScheme appTheme = ColorSchemeCentral.FromAppTheme(ApplicationSettingsManager.Settings.Theme);


            parent.Controls.Clear();
            Label folder_root = new Label()
            {

                Padding = categoryPadding,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = categoryFont,
                Dock = DockStyle.Top,


                Height = TextRenderer.MeasureText("Pastas", categoryFont).Height
            };



            Label folder_child = new Label()
            {

                Padding = categoryPadding,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = categoryFont,
                Dock = DockStyle.Top,


                //Height = TextRenderer.MeasureText("Pastas", categoryFont).Height
            };

            folder_root.Click += (s, ee) =>
            {





                RefreshAllButtons(true);

            };


            foreach (var item in folder_form)
            {

                if (item.Folder_id == value)
                    if (item.Is_father == true)
                    {
                        folder_root.Text = item.Name;
                        parent.Controls.Add(folder_root);

                        folder_root.Click += (s, ee) =>
                        {

                            

                            CurrentDevice.CurrentProfile.Currentfolder = item.Parent;

                            RefreshAllButtons(true);

                        };
                    }

                    else
                    {
                        Label folder_children = new Label()
                        {

                            Padding = categoryPadding,
                            TextAlign = ContentAlignment.MiddleLeft,
                            Font = categoryFont,
                            Dock = DockStyle.Top,

                        };

                        folder_children.Text = "............" + item.Name;
                        parent.Controls.Add(folder_children);


                        folder_children.Click += (s, ee) =>
                        {



                            CurrentDevice.CurrentProfile.Currentfolder = item.Parent;

                            RefreshAllButtons(true);

                        };
                    }





            }



            parent.Refresh();



        }
        private void GetAllFolders(IDeckFolder folder)
        {
            // var pasta_mae = folder.GetSubFolders()[root];


            //if(CurrentDevice.MainFolder.GetSubFolders()[root] is DynamicDeckFolder pastapai)
            //  {

            //    folder_form.Add(new folders(pastapai.folder_name, pastapai, true, root));
            //AddFolderInPanelList(shadedPanel1, pastapai.folder_name, pastapai, root, true, root);
            //    Debug.WriteLine("PASTA PAI: " + pastapai.folder_name);


            //


            foreach (var abacate in folder.GetSubFolders())
            {

                if (abacate is DynamicDeckFolder PP)
                {



                    folder_form.Add(new folders(PP.GetDeckDefaultLayer.Deckname, PP, false, root));
                    // AddFolderInPanelList(shadedPanel1, PP.folder_name, PP, root, false, root);
                    //   Debug.WriteLine(PP.folder_name + "CC:" + PP.GetSubFolders().Count);             

                    if (abacate.GetSubFolders().Count == 0)
                    {
                        if (CurrentDevice.CurrentProfile.Mainfolder.GetSubFolders().Count - 1 > root)
                        {

                            if (CurrentDevice.CurrentProfile.Mainfolder.GetSubFolders()[root] is DynamicDeckFolder pastapai)
                            {
                                folder_form.Add(new folders(pastapai.GetDeckDefaultLayer.Deckname, pastapai, true, root));
                            }
                            AddFolderInPanelList(root);
                            root++;
                        }
                        else
                        {
                            if (CurrentDevice.CurrentProfile.Mainfolder.GetSubFolders()[root] is DynamicDeckFolder pastapai)
                            {
                                folder_form.Add(new folders(pastapai.GetDeckDefaultLayer.Deckname, pastapai, true, root));
                            }
                            
                            AddFolderInPanelList(root);
                            root = 0;


                            goto fim;
                        }
                        //  Debug.WriteLine("NUMINDEX:" + root);

                        GetAllFolders(CurrentDevice.CurrentProfile.Mainfolder.GetSubFolders()[root]);

                    }
                    else
                    {


                        GetAllFolders(abacate);

                    }

                }


            }



            fim:;
            root = 0;



        }

   
        public void GenerateFolderList(Control parent)
        {
            Padding categoryPadding = new Padding(5, 0, 0, 0);
            Font categoryFont = new Font(parent.Font.FontFamily, 13, FontStyle.Bold);
            Padding itemPadding = new Padding(25, 0, 0, 0);
            Font itemFont = new Font(parent.Font.FontFamily, 12);

            parent.Controls.Clear();
            ApplicationColorScheme appTheme = ColorSchemeCentral.FromAppTheme(ApplicationSettingsManager.Settings.Theme);
            List<DynamicDeckFolder> subFolders = new List<DynamicDeckFolder>();
            List<Control> toAdd = new List<Control>();
            Label main_foolder = new Label();

            main_foolder.Padding = itemPadding;
            main_foolder.TextAlign = ContentAlignment.MiddleLeft;

            main_foolder.Font = itemFont;

            main_foolder.Dock = DockStyle.Top;
            main_foolder.Text = "MAIN_FOOLDER";
            main_foolder.Height = TextRenderer.MeasureText("MAIN_FOOLDER", itemFont).Height;
            main_foolder.Click += (s, ee) =>
            {

                CurrentDevice.CurrentProfile.Currentfolder = CurrentDevice.CurrentProfile.Mainfolder;

                //  Debug.WriteLine("Pasta selecionada:" + folder_name.Text);
                RefreshAllButtons(true);

            };
            toAdd.Add(main_foolder);


            try
            {


                int i = 0;

                foreach (DynamicDeckFolder item in CurrentDevice.CurrentProfile.Mainfolder.GetSubFolders().ToList())
                {
                    Label folder = new Label();

              


                      if (ListFolders(item).Count > 0)
                    {

                        folder.Padding = itemPadding;
                        folder.TextAlign = ContentAlignment.MiddleLeft;

                        folder.Font = itemFont;

                        folder.Dock = DockStyle.Top;
                        folder.Text = item.GetDeckDefaultLayer.Deckname;
                        folder.Height = TextRenderer.MeasureText(item.GetDeckDefaultLayer.Deckname, itemFont).Height;
                        toAdd.Add(folder);
                        foreach (DynamicDeckFolder subitem in ListFolders(item).Skip(1))
                        {

                            Label subfolder = new Label();
                            subfolder.Padding = itemPadding;
                            subfolder.TextAlign = ContentAlignment.MiddleLeft;

                            subfolder.Font = itemFont;

                            subfolder.Dock = DockStyle.Top;
                            subfolder.Text = "..." + subitem.GetDeckDefaultLayer.Deckname;
                            subfolder.Height = TextRenderer.MeasureText("..." + subitem.GetDeckDefaultLayer.Deckname, itemFont).Height;
                            subfolder.Click += (s, ee) =>
                                                   {

                                                       CurrentDevice.CurrentProfile.Currentfolder = subitem;

                                                   //  Debug.WriteLine("Pasta selecionada:" + folder_name.Text);
                                                   RefreshAllButtons(true);

                                                   };
                            toAdd.Add(subfolder);


                        }
                    }
                    else
                    {

                        folder.Padding = itemPadding;
                        folder.TextAlign = ContentAlignment.MiddleLeft;

                        folder.Font = itemFont;

                        folder.Dock = DockStyle.Top;
                        folder.Text = item.GetDeckDefaultLayer.Deckname;
                        folder.Height = TextRenderer.MeasureText(item.GetDeckDefaultLayer.Deckname, itemFont).Height;
                        toAdd.Add(folder);
                    }



                    folder.Click += (s, ee) =>
                    {

                        CurrentDevice.CurrentProfile.Currentfolder = item;

                        //  Debug.WriteLine("Pasta selecionada:" + folder_name.Text);
                        RefreshAllButtons(true);

                    };
                    i++;
              
                
                }

            }




            catch (Exception e)
            {

                Debug.WriteLine("BUG:" + e.ToString());
            }
            toAdd.AsEnumerable().Reverse().All(m =>
            {
                parent.Controls.Add(m);
                return true;
            });
        }

        public void GenerateSidebar(Control parent, bool loadplugins = false)
        {
            Padding categoryPadding = new Padding(5, 0, 0, 0);
            Font categoryFont = new Font(parent.Font.FontFamily, 13, FontStyle.Bold);
            Padding itemPadding = new Padding(25, 0, 0, 0);
            Font itemFont = new Font(parent.Font.FontFamily, 12);

            var items = ReflectiveEnumerator.GetEnumerableOfType<AbstractDeckAction>();


            List<Control> toAdd = new List<Control>();


            foreach (DeckActionCategory enumItem in Enum.GetValues(typeof(DeckActionCategory)))
            {
                var enumItems = items.Where(i => i.GetActionCategory() == enumItem && i.IsPlugin() == false);
                if (enumItems.Any())
                {
                    toAdd.Add(new Label()
                    {
                        Padding = categoryPadding,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Font = categoryFont,
                        Dock = DockStyle.Top,
                        Text = enumItem.ToString(),
                        Tag = "header",
                        Height = TextRenderer.MeasureText(enumItem.ToString(), categoryFont).Height
                    });


                    foreach (var i2 in enumItems)
                    {
                        Label item = new Label()
                        {
                            Padding = itemPadding,
                            TextAlign = ContentAlignment.MiddleLeft,
                            Font = itemFont,
                            Dock = DockStyle.Top,
                            Text = i2.GetActionName(),
                            Height = TextRenderer.MeasureText(i2.GetActionName(), itemFont).Height,
                            Tag = i2

                        };
                    
                        item.MouseDown += (s, ee) =>
                        {
                            if (item.Tag is AbstractDeckAction act)
                                item.DoDragDrop(new DeckActionHelper(act), DragDropEffects.Copy);
                        };
                        toAdd.Add(item);
                    }
                }
            }
            toAdd.AsEnumerable().Reverse().All(m =>
            {
                parent.Controls.Add(m);
                return true;
            });
            if (loadplugins)
            {
               
                createPluginButton();

              
            }
          

        }




        public int LayerSelected()
        {
            var checkedRadioButton = groupBox1.Controls
                          .OfType<RadioButton>()
                          .FirstOrDefault(r => r.Checked);
            switch (checkedRadioButton.Name)
            {
                case "camada1":
                    return 1;

                case "camada2":
                    return 2;
                default:
                    return 0;
            }
        }

        private void ImageModernButton1_MouseClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Filter = ""
            };
            var herePath = Path.Combine(Directory.GetCurrentDirectory(), "Keys");
            if (Directory.Exists(herePath))
                dlg.InitialDirectory = herePath;

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            string sep = string.Empty;

            dlg.Filter = string.Format("Images ({0})|{0}|All files|*.*",
                string.Join(";", codecs.Select(codec => codec.FilenameExtension).ToArray()));

            dlg.DefaultExt = "png"; // Default file extension

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //We have an image file.
                //Load as bitmap and replace DeckImage
                try
                {
                    Bitmap bmp = new Bitmap(dlg.FileName);
                    if (DeckImage.ImageToByte(bmp).Length > CLIENT_ARRAY_LENGHT)
                    {
                        MessageBox.Show(this, "The selected image is too big to be sent to the device. Please choose another", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ImageModernButton1_MouseClick(sender, new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0));
                        return;
                    }


                  if(ActionImagePlaceHolder.Camada == 1)
                    {
                 
ActionImagePlaceHolder.Image = bmp;
                    }
                    else if (ActionImagePlaceHolder.Camada == 2)
                    {
                     
                        ActionImagePlaceHolder.ImageLayerTwo = bmp;
                    }
                      
                            
                   

                    
                }
                catch (Exception)
                {
                }
            }
        }

        private Stopwatch lastClick = new Stopwatch();

        private void ItemButton_MouseClick(object sender, EventArgs e)
        {
            lastClick.Stop();
            bool isDoubleClick = lastClick.ElapsedMilliseconds != 0 && lastClick.ElapsedMilliseconds <= SystemInformation.DoubleClickTime;
            if (sender is ImageModernButton mb)
            {
                if (mb.Tag != null && mb.Tag is IDeckItem item)
                {
                    if (item is IDeckFolder folder)
                    {
                        if (!isDoubleClick)
                        {
                            FocusItem(mb, item);
                            goto end;
                        }
                        //Navigate to the folder
                        CurrentDevice.CurrentProfile.Currentfolder = folder;
                        RefreshAllButtons(true);
                        goto end;
                    }
                    if (CurrentDevice.CurrentProfile.Currentfolder.GetParent() != null)
                    {
                        //Not on the main folder
                        if (item is DynamicBackItem)
                        {
                            CurrentDevice.CurrentProfile.Currentfolder = CurrentDevice.CurrentProfile.Currentfolder.GetParent();
                            RefreshAllButtons(true);
                            lastClick.Reset();
                            return;
                        }
                    }

                    //Show button panel with settable properties
                    FocusItem(mb, item);

                    lastClick.Reset();
                }
                else
                {
                    Buttons_Unfocus(sender, e);
                }
                return;
                end:
                lastClick.Reset();
                lastClick.Start();
            }

        }

        private void ItemButton_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            var popupMenu = new ContextMenuStrip();





            if (sender is ImageModernButton senderB)
            {
                if (e.Button == MouseButtons.Left && DevicePersistManager.IsVirtualDeviceConnected && ModifierKeys == Keys.Shift)
                {
                    if (senderB.Tag != null && senderB.Tag is DynamicDeckItem item)
                    {
                        item.DeckAction?.OnButtonUp(CurrentDevice);
                    }
                    return;
                }


                if (!senderB.DisplayRectangle.Contains(e.Location)) return;
                if (e.Button == MouseButtons.Right && CurrentDevice.CurrentProfile.Currentfolder.GetDeckItems().Any(c => CurrentDevice.CurrentProfile.Currentfolder.GetItemIndex(c) == senderB.CurrentSlot))
                {


                    popupMenu.Items.Add("Remove item").Click += (s, ee) =>
                    {
                        if (senderB != null)
                        {

                            if (senderB.Tag is DynamicDeckFolder ttt)
                            {
                                if (ttt.KeyGlobalValue.Keys != null)
                                {

                                    Wrapper.events.Trigger("DeckFolderEventDelete", new DeckFolderEventDelete(ttt));

                                }

                            }
                            if (senderB.Image != Resources.img_folder && senderB.Image != Resources.img_item_default)
                            {
                                senderB.Image.Dispose();
                            }
                            senderB.Tag = null;
                            senderB.Image = null;
                            Buttons_Unfocus(sender, e);


                            CurrentDevice.CurrentProfile.Currentfolder.Remove(senderB.CurrentSlot);
                        }
                    };

                    popupMenu.Items.Add("Clear image").Click += (s, ee) =>
                    {
                        if (senderB.Image != null && senderB.Image != Resources.img_folder && senderB.Image != Resources.img_item_default)
                        {
                            senderB.Image.Dispose();
                            if (senderB != null && senderB.Tag != null && senderB.Tag is IDeckItem deckItem)
                            {
                                bool isFolder = deckItem is IDeckFolder;
                                senderB.Image = isFolder ? Resources.img_folder : ((IDeckItem)senderB.Tag).GetDefaultImage()?.Bitmap ?? Resources.img_item_default;
                            }
                        }
                    };


                    popupMenu.Show(sender as Control, e.Location);



                }


                return;
            }
        }
        public static SizeF GetTextSize(String text, Font font, Image img_result)
        {
            using (var img = new Bitmap(200, 100))
            {
                using (var g = Graphics.FromImage(img_result))
                {
                    return g.MeasureString(text, font);
                }
            }
        }
        public static byte[] converterDemo(Image x)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));
            return xByte;
        }




        private void Write_name_Image(string text, Bitmap imageFilePath, float x, float y, string font, int size)
        {

            {


                Image baseImage = imageFilePath;
                Image modifiedImage = (Image)baseImage.Clone();

                try
                {
                    Graphics g = Graphics.FromImage(modifiedImage);
                    using (Font myfont = new Font(font, size))
                    {
                        var format = new StringFormat
                        {
                            Alignment = StringAlignment.Center,

                            LineAlignment = StringAlignment.Center

                        };

                        g.DrawString(text, myfont, Brushes.Black, new PointF(20f, 65f), format);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Source + " : " + ex.Message);
                }
                //  m.WriteTo(Response.OutputStream);

                try
                {
                    Bitmap bmp = new Bitmap(modifiedImage);
                    if (DeckImage.ImageToByte(bmp).Length > CLIENT_ARRAY_LENGHT)
                    {
                        //  MessageBox.Show(this, "The selected image is too big to be sent to the device. Please choose another", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //     ImageModernButton1_MouseClick(sender, new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0));
                        return;
                    }


                    ActionImagePlaceHolder.Image = bmp;
                }
                catch (Exception)
                {
                }



            }
        }



        public static void getEnumValue(ComboBox cxbx, Type typ)
        {

            if (!typ.IsEnum)
            {
                throw new ArgumentException("Only Enum types can be set");
            }


            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();

            foreach (int i in Enum.GetValues(typ))
            {
                string name = Enum.GetName(typ, i);
                string desc = name;
                FieldInfo fi = typ.GetField(name);

                // Get description for enum element
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    string s = attributes[0].Description;
                    if (!string.IsNullOrEmpty(s))
                    {

                        desc = s;
                    }
                }

                list.Add(new KeyValuePair<string, int>(desc, i));
            }

        }

        public static void setEnumValues(ComboBox cxbx, Type typ)
        {
            if (!typ.IsEnum)
            {
                throw new ArgumentException("Only Enum types can be set");
            }

            List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();

            foreach (int i in Enum.GetValues(typ))
            {
                string name = Enum.GetName(typ, i);
                string desc = name;
                FieldInfo fi = typ.GetField(name);

                // Get description for enum element
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    string s = attributes[0].Description;
                    if (!string.IsNullOrEmpty(s))
                    {
                        desc = s;
                    }
                }

                list.Add(new KeyValuePair<string, int>(Texts.rm.GetString(desc, Texts.cultereinfo), i));
            }
            // NOTE: It is very important that DisplayMember and ValueMember are set before DataSource.
            //       If you do, this works fine, and the SelectedValue of the ComboBox will be an int
            //       version of the Enum.
            //       If you don't, it will be a KeyValuePair.
            cxbx.DisplayMember = "Key";
            cxbx.ValueMember = "Value";
            cxbx.DataSource = list;
        }

        public enum Position
        {
            [Description("POSITIONCOMBOBOXDOWN")]
            ANDROID_baixo = 81,

            [Description("POSITIONCOMBOBOXMEIO")]
            ANDROID_meio = 17,
           [Description("POSITIONCOMBOBOXUP")]
            ANDROID_cima = 49

           
        }
        private void FocusItemPropertiesOptions(DeckItemMisc dI)
        {

       
            clearFlowLayout();
          
            var props = dI.GetType().GetProperties().Where(
              prop => Attribute.IsDefined(prop, typeof(ActionPropertyIncludeAttribute)));
            foreach (var prop in props)
            {
                bool shouldUpdateIcon = Attribute.IsDefined(prop, typeof(ActionPropertyUpdateImageOnChangedAttribute));
                MethodInfo helperMethod = dI.GetType().GetMethod(prop.Name + "Helper");
                if (helperMethod != null)
                {
                    flowLayoutPanel1.Controls.Add(new Label()
                    {
                        Text = GetPropertyDescription(prop)
                    });

                    Button helperButton = new ModernButton()
                    {
                        Text = "..."
                    };

                    helperButton.Click += (sender, e) => helperMethod.Invoke(dI, new object[] { });

                    helperButton.Width = flowLayoutPanel1.DisplayRectangle.Width - 16;
                    flowLayoutPanel1.Controls.Add(helperButton);
                }
                else
                {
                    if (prop.PropertyType.IsSubclassOf(typeof(Enum)))
                    {
                        var values = Enum.GetValues(prop.PropertyType);
                        flowLayoutPanel1.Controls.Add(new Label()
                        {
                            Text = GetPropertyDescription(prop)
                        });
                        ComboBox cBox = new ComboBox
                        {
                            DropDownStyle = ComboBoxStyle.DropDownList
                        };
                        cBox.Items.AddRange(values.OfType<Enum>().Select(c => EnumUtils.GetDescription(prop.PropertyType, c, c.ToString())).ToArray());

                        cBox.Text = EnumUtils.GetDescription(prop.PropertyType, (Enum)prop.GetValue(dI), ((Enum)prop.GetValue(dI)).ToString());

                        cBox.SelectedIndexChanged += (s, e) =>
                        {
                            try
                            {
                                if (cBox.Text == string.Empty) return;
                                prop.SetValue(dI, EnumUtils.FromDescription(prop.PropertyType, cBox.Text));
                         //   UpdateIcon(shouldUpdateIcon);
                            }
                            catch (Exception)
                            {
                                //Ignore all errors
                            }
                        };
                        flowLayoutPanel1.Controls.Add(cBox);
                        return;
                    }

                    if (prop.PropertyType == typeof(string) || prop.PropertyType == typeof(float)|| prop.PropertyType == typeof(int))
                    {
                        //
                        flowLayoutPanel1.Controls.Add(new Label()
                        {
                            Text = GetPropertyDescription(prop)
                        });

                        var txt = new TextBox
                        {
                            Text = (string)TypeDescriptor.GetConverter(prop.PropertyType).ConvertTo(prop.GetValue(dI), typeof(string))
                        };
                        txt.TextChanged += (sender, e) =>
                        {
                            try
                            {
                                if (txt.Text == string.Empty) return;
                            //After loosing focus, convert type to thingy.
                            prop.SetValue(dI, TypeDescriptor.GetConverter(prop.PropertyType).ConvertFrom(txt.Text));
                            //   UpdateIcon(shouldUpdateIcon);
                        }
                            catch (Exception)
                            {
                            //Ignore all errors
                        }
                        };
                        txt.Width = flowLayoutPanel1.DisplayRectangle.Width - SystemInformation.VerticalScrollBarWidth * 2;
                        flowLayoutPanel1.Controls.Add(txt);
                    }
                    // checkbox test
                    if (prop.PropertyType == typeof(bool))
                    {
                    //    continue;
                        flowLayoutPanel1.Controls.Add(new Label()
                        {
                            Text = GetPropertyDescription(prop)
                        });

                        var cbk = new CheckBox
                        {
                           
                            Checked = (bool)prop.GetValue(dI)

                        };
                        cbk.CheckedChanged += (sender, e) =>
                        {
                            try
                            {

                            //After loosing focus, convert type to thingy.
                            prop.SetValue(dI, TypeDescriptor.GetConverter(prop.PropertyType).ConvertFrom(cbk.Checked));
                            //   UpdateIcon(shouldUpdateIcon);
                        }
                            catch (Exception)
                            {
                            //Ignore all errors
                        }
                        };
                        cbk.Width = flowLayoutPanel1.DisplayRectangle.Width - SystemInformation.VerticalScrollBarWidth * 2;
                        flowLayoutPanel1.Controls.Add(cbk);
                    }
                }
            }
           


            //            ModifyColorScheme(flowLayoutPanel1.Controls.OfType<Control>());

        }
        private void ChangeCamadaLayerByComboBox(DynamicDeckItem item)
        {
            
                if ( item.DeckAction.getLayer())
                {
                    camada2.Visible = true;
                    if (item.GetDeckLayerTwo == null)
                    {
                        item.GetDeckLayerTwo = new DeckItemMisc();
                    }
                
                }
                else
                {
                    camada2.Visible = false;
                    item.GetDeckLayerTwo = null;
                  
                }
                

            
        }
        private void clearFlowLayout()
        {
  
            flowLayoutPanel1.Controls.OfType<Control>().All(c =>
            {
                    
          
                c.Dispose();
             
                return true;
            });
            flowLayoutPanel1.Controls.Clear();


        }
        void ClearControl(Control c)
        {

            flowLayoutPanel1.Controls.Remove(c);
            c.Dispose();
            //etc

            foreach (Control child in c.Controls)
                ClearControl(child);
        }
        private void FocusItem(ImageModernButton mb, IDeckItem item)
        {
            if(item is DynamicDeckItem TTT && TTT.DeckAction != null)
            {
            ChangeCamadaLayerByComboBox(TTT);
             
            }
            
            ActionImagePlaceHolder.Origin = mb;

            camada1.Tag = item;
            camada2.Tag = item;
  

            ActionImagePlaceHolder.Refresh();

            shadedPanel2.Show();
            shadedPanel1.Refresh();


        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DialogColorChangedHandler(object sender, EventArgs e)
        {
            //   dialogColorPreviewPanel.Color = ((ColorPickerDialog)sender).Color;
        }


        private void LoadPropertiesFolder(DynamicDeckFolder item, FlowLayoutPanel panel)
        {


            var props = item.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(ActionPropertyIncludeAttribute)));
            foreach (var prop in props)
            {
                bool shouldUpdateIcon = Attribute.IsDefined(prop, typeof(ActionPropertyUpdateImageOnChangedAttribute));
                MethodInfo helperMethod = item.GetType().GetMethod(prop.Name + "Helper");
                if (helperMethod != null)
                {
                    panel.Controls.Add(new Label()
                    {
                        Text = GetPropertyDescription(prop)
                    });

                    Button helperButton = new ModernButton()
                    {
                        Text = "..."
                    };

                    helperButton.Click += (sender, e) => helperMethod.Invoke(item, new object[] { });

                    helperButton.Width = panel.DisplayRectangle.Width - 16;
                    panel.Controls.Add(helperButton);
                }
                else
                {
                    if (prop.PropertyType.IsSubclassOf(typeof(Enum)))
                    {
                        var values = Enum.GetValues(prop.PropertyType);
                        panel.Controls.Add(new Label()
                        {
                            Text = GetPropertyDescription(prop)
                        });
                        ComboBox cBox = new ComboBox
                        {
                            DropDownStyle = ComboBoxStyle.DropDownList
                        };
                        cBox.Items.AddRange(values.OfType<Enum>().Select(c => EnumUtils.GetDescription(prop.PropertyType, c, c.ToString())).ToArray());

                        cBox.Text = EnumUtils.GetDescription(prop.PropertyType, (Enum)prop.GetValue(item), ((Enum)prop.GetValue(item)).ToString());

                        cBox.SelectedIndexChanged += (s, e) =>
                        {
                            try
                            {
                                if (cBox.Text == string.Empty) return;
                                prop.SetValue(item, EnumUtils.FromDescription(prop.PropertyType, cBox.Text));
                                UpdateIcon(shouldUpdateIcon);
                            }
                            catch (Exception)
                            {
                                //Ignore all errors
                            }
                        };
                        panel.Controls.Add(cBox);
                        return;
                    }

                    if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom
                (typeof(string))) continue;
                    panel.Controls.Add(new Label()
                    {
                        Text = GetPropertyDescription(prop)
                    });

                    var txt = new TextBox
                    {
                        Text = (string)TypeDescriptor.GetConverter(prop.PropertyType).ConvertTo(prop.GetValue(item), typeof(string))
                    };
                    txt.TextChanged += (sender, e) =>
                    {
                        try
                        {
                            if (txt.Text == string.Empty) return;
                            //After loosing focus, convert type to thingy.
                            prop.SetValue(item, TypeDescriptor.GetConverter(prop.PropertyType).ConvertFrom(txt.Text));
                            UpdateIcon(shouldUpdateIcon);
                        }
                        catch (Exception)
                        {
                            //Ignore all errors
                        }
                    };
                    txt.Width = panel.DisplayRectangle.Width - SystemInformation.VerticalScrollBarWidth * 2;
                    panel.Controls.Add(txt);
                }
            }

            ModifyColorScheme(flowLayoutPanel1.Controls.OfType<Control>());
        }
        private void LoadProperties(DynamicDeckItem item, FlowLayoutPanel panel)
        {

        
            var props = item.DeckAction.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(ActionPropertyIncludeAttribute)));
            foreach (var prop in props)
            {
             
                
                //action_label.MaximumSize = new Size(110, 0);
                //action_label.AutoSize = true;
                //action_label.Text = item.DeckAction.GetActionName();
                bool shouldUpdateIcon = Attribute.IsDefined(prop, typeof(ActionPropertyUpdateImageOnChangedAttribute));
                MethodInfo helperMethod = item.DeckAction.GetType().GetMethod(prop.Name + "Helper");
                action_label.Text = item.DeckAction.GetActionName();
                if (helperMethod != null)
                {
                    panel.Controls.Add(new Label()
                    {
                        Text = GetPropertyDescription(prop)
                        
                    });
                    panel.ResumeLayout(false);

                    Button helperButton = new ModernButton()
                    {
                        Text = "..."
                    };

                    helperButton.Click += (sender, e) => helperMethod.Invoke(item.DeckAction, new object[] { });

                    helperButton.Width = panel.DisplayRectangle.Width - 16;
                    panel.Controls.Add(helperButton);
                  //  panel.ResumeLayout(false);

                }
                else
                {
                    if (prop.PropertyType.IsSubclassOf(typeof(Enum)))
                    {
                        var values = Enum.GetValues(prop.PropertyType);
                        panel.Controls.Add(new Label()
                        {
                            Text = GetPropertyDescription(prop)
                        });
                      //  panel.ResumeLayout(false);

                        ComboBox cBox = new ComboBox
                        {
                            DropDownStyle = ComboBoxStyle.DropDownList
                        };
                        cBox.Items.AddRange(values.OfType<Enum>().Select(c => EnumUtils.GetDescriptionTranslator(prop.PropertyType, c, c.ToString())).ToArray());

                        cBox.Text = EnumUtils.GetDescriptionTranslator(prop.PropertyType, (Enum)prop.GetValue(item.DeckAction), ((Enum)prop.GetValue(item.DeckAction)).ToString());
                 //   ChangeCamadaLayerByComboBox(item);
                        cBox.SelectedIndexChanged += (s, e) =>
                        {
                            try
                            {
                                if (cBox.Text == string.Empty) return;
                                prop.SetValue(item.DeckAction, EnumUtils.FromDescriptionTRanslator(prop.PropertyType, cBox.Text));
                              
                                //UpdateIcon(shouldUpdateIcon);  
                            }
                            catch (Exception)
                            {
                                //Ignore all errors
                            }
                        };
                        panel.Controls.Add(cBox);
                   //     panel.ResumeLayout(false);

                        return;
                    }

                    if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom
                (typeof(string))) continue;
                    panel.Controls.Add(new Label()
                    {
                        Text = GetPropertyDescription(prop)
                    });
                //    panel.ResumeLayout(false);

                    var txt = new TextBox
                    {
                        Text = (string)TypeDescriptor.GetConverter(prop.PropertyType).ConvertTo(prop.GetValue(item.DeckAction), typeof(string))
                    };
                    txt.TextChanged += (sender, e) =>
                    {
                        try
                        {
                            if (txt.Text == string.Empty) return;
                            //After loosing focus, convert type to thingy.
                            prop.SetValue(item.DeckAction, TypeDescriptor.GetConverter(prop.PropertyType).ConvertFrom(txt.Text));
                           // UpdateIcon(shouldUpdateIcon);
                        }
                        catch (Exception)
                        {
                            //Ignore all errors
                        }
                    };
               //     panel.ResumeLayout(false);

                    txt.Width = panel.DisplayRectangle.Width - SystemInformation.VerticalScrollBarWidth * 2;
                    panel.Controls.Add(txt);
                 //   panel.ResumeLayout(false);

                }
            }
        

        }

        private string GetPropertiesPlugins(DynamicDeckItem item, string properties)
        {
            Type type = item.DeckAction.GetType();

            PropertyInfo propa = type.GetProperty(properties);
            return (string)TypeDescriptor.GetConverter(propa.PropertyType).ConvertTo(propa.GetValue(item.DeckAction), typeof(string));


        }
        private void LoadDll(DynamicDeckItem item, PluginLoader plugin, string name= "")
        {
            Type type = item.DeckAction.GetType();
            if (plugin != null)
            {
                PropertyInfo propb = type.GetProperty("myPlugin");
                propb.SetValue(item.DeckAction, plugin, null);
            }
            if (name != null)
            {
                PropertyInfo propb = type.GetProperty("name");
                propb.SetValue(item.DeckAction, name, null);
            }
        }
            private void LoadPropertiesPlugins(DynamicDeckItem item, string script, string entrypoint = "", string name = "",string dllpath = "")
        {

            Type type = item.DeckAction.GetType();
            if (script != "")
            {
                PropertyInfo propb = type.GetProperty("ToScript");
                propb.SetValue(item.DeckAction, script, null);
            }
            if (name != "")
            {
                PropertyInfo propa = type.GetProperty("ScriptNamePoint");
                propa.SetValue(item.DeckAction, name, null);

            }

            if (entrypoint != "")
            {
                PropertyInfo prop = type.GetProperty("ScriptEntryPoint");
                prop.SetValue(item.DeckAction, entrypoint, null);
            }
            if (dllpath != "")
            {
                PropertyInfo prop = type.GetProperty("dllpath");
                prop.SetValue(item.DeckAction, dllpath, null);
               
            }

            //  prop.SetValue(item.DeckAction, TypeDescriptor.GetConverter(prop.PropertyType).ConvertFrom(result_string));
            //         UpdateIcon(shouldUpdateIcon);




            //  ModifyColorScheme(flowLayoutPanel1.Controls.OfType<Control>());
        }
        public void button_creator(string name, string entry_point, string script,string dllpath)
        {
            List<Control> toAdd = new List<Control>();
        
            Padding categoryPadding = new Padding(5, 0, 0, 0);
            Font categoryFont = new Font(MainForm.instance.ShadedPanel1.Font.FontFamily, 13, FontStyle.Bold);
            Padding itemPadding = new Padding(25, 0, 0, 0);
            Font itemFont = new Font(MainForm.instance.ShadedPanel1.Font.FontFamily, 12);
            // DisplayButtons.Forms.MainForm.testando(value);
            MainForm.instance.ShadedPanel1.Invoke((MethodInvoker)delegate
            {
               
              

                var items = ReflectiveEnumerator.GetEnumerableOfType<AbstractDeckAction>();

                foreach (DeckActionCategory enumItem in Enum.GetValues(typeof(DeckActionCategory)))
                {
                    var enumItems = items.Where(i => i.GetActionCategory() == enumItem && i.IsPlugin() == true && i is PluginLuaGenerator);
                    if (enumItems.Any())
                    {
                        foreach (var i2 in enumItems)
                        {
                            Label item = new Label()
                            {

                                Padding = itemPadding,
                                TextAlign = ContentAlignment.MiddleLeft,
                                Font = itemFont,
                                Dock = DockStyle.Top,
                                Text = name,
                                Height = TextRenderer.MeasureText(name, itemFont).Height,
                                Tag = i2,

                            };
                            //    Debug.WriteLine("TAG VINDO: " + i2);
                            item.MouseDown += (s, ee) =>
                            {

                                if (item.Tag is AbstractDeckAction act)
                                {
                                    var _deckaction = new DeckActionHelper(act);
                                    _deckaction.ToExecute = entry_point;
                                    _deckaction.ToName = name;
                                    _deckaction.dllpath = dllpath;
                                    _deckaction.ToScript = script;
                                    item.DoDragDrop(_deckaction, DragDropEffects.Copy);
                                    //      i2.SetConfigs();
                                    //  LoadPropertiesPlugins(i2, script);

                                }


                            };


                            toAdd.Add(item);
           
                        }

                    }
                }

                
              
            });




toAdd.AsEnumerable().Reverse().All(m =>
                {
                    ShadedPanel1.Controls.Add(m);

                    return true;
                });
        }
        public void button_dll(PluginLoader plugin)
        {
            List<Control> toAdd = new List<Control>();

            Padding categoryPadding = new Padding(5, 0, 0, 0);
            Font categoryFont = new Font(MainForm.instance.ShadedPanel1.Font.FontFamily, 13, FontStyle.Bold);
            Padding itemPadding = new Padding(25, 0, 0, 0);
            Font itemFont = new Font(MainForm.instance.ShadedPanel1.Font.FontFamily, 12);
            // DisplayButtons.Forms.MainForm.testando(value);
            MainForm.instance.ShadedPanel1.Invoke((MethodInvoker)delegate
            {




                foreach (var pluginType in plugin
                                    .LoadDefaultAssembly()
                                    .GetTypes()
                                    .Where(t => typeof(AbstractDeckAction).IsAssignableFrom(t) && !t.IsAbstract))
                {
                    // This assumes the implementation of IPlugin has a parameterless constructor
                    AbstractDeckAction meuplugin = (AbstractDeckAction)Activator.CreateInstance(pluginType);

                    //var items = ReflectiveEnumerator.GetEnumerableOfType<AbstractDeckAction>(meuplugin);

                    foreach (DeckActionCategory enumItem in Enum.GetValues(typeof(DeckActionCategory)))
                    {
                        //  var enumItems = items.Where(i => i.GetActionCategory() == enumItem && i.IsPlugin() == true);
                        if (meuplugin.GetActionCategory() == enumItem)
                        {
                            toAdd.Add(new Label()
                            {
                                Padding = categoryPadding,
                                TextAlign = ContentAlignment.MiddleLeft,
                                Font = categoryFont,
                                Dock = DockStyle.Top,
                                Text = enumItem.ToString(),
                                Tag = "header",
                                Height = TextRenderer.MeasureText(enumItem.ToString(), categoryFont).Height
                            });



                            Label item = new Label()
                            {
                                Padding = itemPadding,
                                TextAlign = ContentAlignment.MiddleLeft,
                                Font = itemFont,
                                Dock = DockStyle.Top,
                                Text = meuplugin.GetActionName(),
                                Height = TextRenderer.MeasureText(meuplugin.GetActionName(), itemFont).Height,
                                Tag = meuplugin

                            };

                            item.MouseDown += (s, ee) =>
                            {
                                if (item.Tag is AbstractDeckAction act)
                                {
                                    var deckaction = new DeckActionHelper(act);
                                    deckaction.plugin = plugin;
                                    deckaction.ToName = meuplugin.GetActionName();
                                    item.DoDragDrop(deckaction, DragDropEffects.Copy);
                                }
                            };
                            toAdd.Add(item);
                        }
                    }

                    }
                    toAdd.AsEnumerable().Reverse().All(m =>
                    {
                        ShadedPanel1.Controls.Add(m);
                        return true;
                    });
                

                    
                



            });
        




           
        }

        public void RefreshButtonPlugin(int slot, string script, bool sendToDevice = true)
        {
            Buttons_Unfocus(this, EventArgs.Empty);

            IDeckFolder folder = CurrentDevice?.CurrentProfile.Currentfolder;
            ImageModernButton control1 = GetButtonControl(slot);
            //Label control_label = GetLabelControl(slot);
            // Label title_control = Controls.Find("titleLabel" + slot, true).FirstOrDefault() as Label;

            control1.NormalImage = null;
            control1.Tag = null;
            control1.Text = "";


            if (folder == null) control1.Invoke(new Action(control1.Refresh));

            if (folder == null) return;
            for (int i = 0; i < folder.GetDeckItems().Count; i++)
            {
                IDeckItem item = null;
                item = folder.GetDeckItems()[i];

                if (folder.GetItemIndex(item) != slot) continue;
                ImageModernButton control = Controls.Find("modernButton" + folder.GetItemIndex(item), true).FirstOrDefault() as ImageModernButton;
                Label control2 = Controls.Find("label" + folder.GetItemIndex(item), true).FirstOrDefault() as Label;

                //Label title_control = Controls.Find("titleLabel" + folder.GetItemIndex(item), true).FirstOrDefault() as Label;
                if (item != null)
                {
                    var ser = item.GetDeckDefaultLayer.GetItemImage().BitmapSerialized;
                    //  control.NormalImage = null







                    if (item is DynamicDeckItem TT && TT.DeckAction is PluginLuaGenerator FF)
                    {

                        //        LoadPropertiesPlugins(TT, script);
                    }
                    // control2.Text = item.DeckName;


                    //control.NormalImage = item?.GetItemImage().Bitmap; //Write_name_Image(dI.DeckAction.GetActionName(), item?.GetItemImage().Bitmap, 10f, 10f, "Arial", 10);


                    control.Tag = item;
                    control.Invoke(new Action(control.Refresh));



                }
            }
            CurrentDevice.CheckCurrentFolder();
            if (sendToDevice)
            {
                SendItemsToDevice(CurrentDevice, folder);

            }
            // 
        }
        public Dictionary<string, string> PluginsLists = new Dictionary<string, string>();
        public Dictionary<string, PluginLoader> PluginsOnlyDll = new Dictionary<string, PluginLoader>();
        public void PluginLoaderDll(string name, PluginLoader plugin)
        {
            if (MainForm.Instance.PluginsOnlyDll.ContainsKey(name))
            {
                MainForm.Instance.PluginsOnlyDll[name] = plugin;
            }
            else
            {
                MainForm.Instance.PluginsOnlyDll.Add(name, plugin);
            }
        }
        public static PluginLoader GetPluginDll(string key)
        {
            PluginLoader result = null;
            try
            {
                if (MainForm.Instance.PluginsOnlyDll.ContainsKey(key))
                {
                    result = MainForm.Instance.PluginsOnlyDll[key];
                }
                else
                {

                    result = null;
                }
            }
            catch (Exception)
            {

                Debug.WriteLine("NULL");
            }
            return result;
        }
        public static string GetPluginScript(string key)
        {
            string result = null;
            try
            {
                if (MainForm.Instance.PluginsLists.ContainsKey(key))
                {
                    result = MainForm.Instance.PluginsLists[key];
                }
                else
                {

                    result = "";
                }
            }
            catch (Exception)
            {

                Debug.WriteLine("NULL");
            }
            return result;
        }

        public void PluginLoaderScript(string NameSpace, string Script)
        {
            if (MainForm.Instance.PluginsLists.ContainsKey(NameSpace))
            {
                MainForm.Instance.PluginsLists[NameSpace] = Script;
            }
            else
            {
                MainForm.Instance.PluginsLists.Add(NameSpace, Script);
            }
        }
      
        public void dllAssing(string pluginDll)
        {
         
            if (File.Exists(pluginDll))
                {
                var loader = PluginLoader.CreateFromAssemblyFile(
                       pluginDll,
                    
                       config => config.PreferSharedTypes = true);
            

                foreach (var pluginType in loader
                               .LoadDefaultAssembly()
                               .GetTypes()
                               .Where(t => typeof(InterfaceDll.InterfaceDllClass).IsAssignableFrom(t) && !t.IsAbstract))
                {
                    // This assumes the implementation of IPlugin has a parameterless constructor
                    InterfaceDll.InterfaceDllClass plugin = (InterfaceDll.InterfaceDllClass)Activator.CreateInstance(pluginType);


                    Thread t2 = new Thread(delegate ()
                    {
                        plugin.SetLang(ApplicationSettingsManager.Settings.Language);
                        plugin.LoadScripts(Scripter.Environment);

                    });

                    t2.Start();
                    // t2.Join();
                    Debug.WriteLine($"Created plugin instance '{plugin.GetActionName()}'.");
                }




            }

        }
        private void CreateOnlyDllInstance(string path)
        {
            if (File.Exists(path))
            {
                var loader = PluginLoader.CreateFromAssemblyFile(
                       path,

                       config => config.PreferSharedTypes = true);

                foreach (var pluginType in loader
                                  .LoadDefaultAssembly()
                                  .GetTypes()
                                  .Where(t => typeof(AbstractDeckAction).IsAssignableFrom(t) && !t.IsAbstract))
                {
                    // This assumes the implementation of IPlugin has a parameterless constructor
                    AbstractDeckAction meuplugin = (AbstractDeckAction)Activator.CreateInstance(pluginType);
                    XMLUtils.PluginList.Add(meuplugin);
                }
                //           DevicePersistManager.LoadDevices();
                //   PluginLoaderDll(meuplugin.GetActionName(), loader);
                button_dll(loader);

                
            }
        }
        public void createPluginButton()
        {

      
           Scripter.Initialize();
         
            Package[] installedPackages = Workshop.GetInstalled();
         
            if(installedPackages.Count() == 0)
            {
                return;
            }
            
            installedPackages.ToList().ForEach(x =>
            {
                Dictionary<string, string> packageInfo = x.GetInfo();

          
               
                   
                
                if (packageInfo["EntryPoint"].IsNullOrEmpty())
                {
               CreateOnlyDllInstance(x.ReturnAbsolutePathEntry(packageInfo["Custom_dll"]));

                }
                else
                {
                    button_creator(packageInfo["Name"], x.ReturnPathEntry(packageInfo["EntryPoint"]), x.ReadFileContents(packageInfo["EntryPoint"]), x.ReturnPathEntry(packageInfo["Custom_dll"]));
                    PluginLoaderScript(packageInfo["Name"], x.ReadFileContents(packageInfo["EntryPoint"]));

                    dllAssing(x.ReturnAbsolutePathEntry(packageInfo["Custom_dll"]));


                }
            });

          
          
         
      

        
          
            Padding categoryPadding = new Padding(5, 0, 0, 0);
            Font categoryFont = new Font(MainForm.instance.ShadedPanel1.Font.FontFamily, 13, FontStyle.Bold);
            Padding itemPadding = new Padding(25, 0, 0, 0);
            Font itemFont = new Font(MainForm.instance.ShadedPanel1.Font.FontFamily, 12);
            Label TextTitle = new Label()
            {
                Padding = categoryPadding,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = categoryFont,
                Dock = DockStyle.Top,
                Text = Texts.rm.GetString("GENERATESIDEBARTITLEPLUGINS", Texts.cultereinfo),
                Height = TextRenderer.MeasureText(Text, categoryFont).Height,
                Tag = "header"

            };
            shadedPanel1.Controls.Add(TextTitle);

        }








        public void RefreshAllPluginsDependencies(string script)
        {



            // Buttons_Unfocus(this, EventArgs.Empty);
            IDeckFolder folder = CurrentDevice?.CurrentProfile.Currentfolder;


            if (folder == null) return;
            for (int i = 0; i < folder.GetDeckItems().Count; i++)
            {
                IDeckItem item = null;
                item = folder.GetDeckItems()[i];
                if (item is DynamicDeckItem TT && TT.DeckAction is PluginLuaGenerator FF)
                {

                    LoadPropertiesPlugins(TT, script);
                   // FF.SetConfigs(script);
                }
                //  ImageModernButton control = Controls.Find("modernButton" + folder.GetItemIndex(item), true).FirstOrDefault() as ImageModernButton;
            }




        }
        public void UpdateLayerView()
        {
            
            // AddWatermark("RWER", ((IDeckItem)imageModernButton1.Origin.Tag).GetDefaultImage().Bitmap, "Arial", 7, 20f, 67f, Brushes.White, item, folder);
            if (ActionImagePlaceHolder.Camada == 1)
            {

                ActionImagePlaceHolder.Image = ((IDeckItem)ActionImagePlaceHolder.Origin.Tag).GetDeckDefaultLayer.GetItemImage()?.Bitmap ?? Resources.img_item_default;

            }
            else if (ActionImagePlaceHolder.Camada == 2)
            {
                ActionImagePlaceHolder.ImageLayerTwo = ((IDeckItem)ActionImagePlaceHolder.Origin.Tag).GetDeckLayerTwo.GetItemImage()?.Bitmap ?? Resources.img_item_default;


            }


            ActionImagePlaceHolder.Refresh();
        }
            private void UpdateIcon(bool shouldUpdateIcon)
        {
            // IDeckFolder folder = CurrentDevice?.CurrentProfile.Currentfolder;
            if (shouldUpdateIcon)
            {

                // IDeckItem item = null;
                // item = folder.GetDeckItems()[i];

                // AddWatermark("RWER", ((IDeckItem)imageModernButton1.Origin.Tag).GetDefaultImage().Bitmap, "Arial", 7, 20f, 67f, Brushes.White, item, folder);
                if(ActionImagePlaceHolder.Camada == 1)
                {

                ActionImagePlaceHolder.Image = ((IDeckItem)ActionImagePlaceHolder.Origin.Tag).GetDeckDefaultLayer.GetDefaultImage()?.Bitmap ?? Resources.img_item_default;

                }else if (ActionImagePlaceHolder.Camada == 2)
                {
                    ActionImagePlaceHolder.ImageLayerTwo = ((IDeckItem)ActionImagePlaceHolder.Origin.Tag).GetDeckLayerTwo.GetDefaultImage()?.Bitmap ?? Resources.img_item_default;


                }


                ActionImagePlaceHolder.Refresh();
            }
        }
        public void UpdatePluginImg()
        {
            IDeckFolder folder = CurrentDevice?.CurrentProfile.Currentfolder;

            if (folder == null) return;
            for (int i = 0; i < folder.GetDeckItems().Count; i++)
            {
                IDeckItem item = null;

                item = folder.GetDeckItems()[i];
                ImageModernButton control = Controls.Find("modernButton" + folder.GetItemIndex(item), true).FirstOrDefault() as ImageModernButton;

                if (item != null)
                {
                    if (item is DynamicDeckItem DI && DI.DeckAction != null)
                    {
                        //AddWatermark(DI.DeckAction.GetActionName(), ((IDeckItem)imageModernButton1.Origin.Tag).GetDefaultImage().Bitmap, "Arial", 7, 20f, 67f, Brushes.White, item, folder);


                        //imageModernButton1.Refresh();

                    }


                    control.Tag = item;
                    control.Invoke(new Action(control.Refresh));


                }
                CurrentDevice.CheckCurrentFolder();

                //     imageModernButton1.Image = ((IDeckItem)imageModernButton1.Origin.Tag).GetDefaultImage().Bitmap ?? Resources.img_item_default;
                //    imageModernButton1.Refresh();

            }





        }
        private string GetPropertyDescription(PropertyInfo prop)
        {
            if (Attribute.IsDefined(prop, typeof(ActionPropertyDescriptionAttribute)))
            {
                var attrib = prop.GetCustomAttribute(typeof(ActionPropertyDescriptionAttribute)) as ActionPropertyDescriptionAttribute;
                return attrib.Description;
            }
            return prop.Name;
        }


        private bool mouseDown;
        private Point mouseDownLoc = Cursor.Position;

        

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
      
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void appBar1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // CreateButtons();

            //  ButtonCreator();
            //   ApplyTheme(panel1);

            //     Refresh();
            // var con = MainForm.Instance.CurrentDevice.GetConnection();



        }

        public void reloadALL()
        {

            List<Control> toAdd = new List<Control>();
            foreach (Control c in shadedPanel1.Controls)
            {

                if (c.Tag != null && c.Tag.ToString().ToLowerInvariant() == "header")
                {
                    toAdd.Add(c);

                }

            }

            toAdd.AsEnumerable().Reverse().All(m =>
            {
                shadedPanel1.Controls.Remove(m);
                return true;
            });

            try
            {
                reloadButtons();
            }
            finally
            {

                reloadExternButtons();
            }
        }
        public void reloadExternButtons()
        {

            List<Control> toAdd = new List<Control>();

            foreach (Control c in shadedPanel1.Controls)
            {

               
                if (c.Tag is AbstractDeckAction act)
                    if (act.IsPlugin() == true)
                    {
                        toAdd.Add(c);


                    }
            }
            toAdd.AsEnumerable().Reverse().All(m =>
            {
                shadedPanel1.Controls.Remove(m);
                return true;
            });
            createPluginButton();
          
            ApplySidebarTheme(shadedPanel1);


        }

       
        public void reloadButtons()
        {



            List<Control> toAdd = new List<Control>();

            foreach (Control c in shadedPanel1.Controls)
            {

               
                if (c.Tag is AbstractDeckAction act   )
                    if (act.IsPlugin() == false)
                    {
                        toAdd.Add(c);


                    }
            }
            toAdd.AsEnumerable().Reverse().All(m =>
            {
                shadedPanel1.Controls.Remove(m);
                return true;
            });

            GenerateSidebar(shadedPanel1, false);
            ApplySidebarTheme(shadedPanel1);
        }

        public void openConsoleDeveloper()
        {
            Core.Initialize();
        }

        private void ImageModernButton2_Click(object sender, EventArgs e)
        {
            reloadALL();
       //     ApplyTheme(panel_buttons);



        }

        private void ImageModernButton6_Click(object sender, EventArgs e)
        {
            reloadExternButtons();
        }

        private void ImageModernButton3_Click(object sender, EventArgs e)
        {
            reloadButtons();
        }

        private void ImageModernButton4_Click(object sender, EventArgs e)
        {
            openConsoleDeveloper();
        }

        private void Painel_developer_Paint(object sender, PaintEventArgs e)
        {

            imageModernButton2.Text = Texts.rm.GetString("BUTTONRELOADALL", Texts.cultereinfo);
         
            imageModernButton4.Text = Texts.rm.GetString("BUTTONOPENCONSOLE", Texts.cultereinfo);




        }
    //    protected override void OnResize(EventArgs e)
    //    {
        
    //base.OnResize(e);
    //        if (ApplicationSettingsManager.Settings.isAutoMinimizer)
    //        {



    //            bool cursorNotInBar = Screen.GetWorkingArea(this).Contains(Cursor.Position);

    //            if (this.WindowState == FormWindowState.Minimized && cursorNotInBar)
    //            {

    //                this.ShowInTaskbar = false;
    //                notifyIcon1.Visible = true;
    //                this.Hide();
    //            }
    //            else
    //            {
    //                notifyIcon1.Visible = false;
    //            }
    //        }
    //    }
        private void MainForm_Resize(object sender, EventArgs e)
        {

        }

        private void Panel1_Resize(object sender, EventArgs e)
        {

        }

        private void NotifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
 
        }

        private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
  
     
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
           // executeCMD();
          //  Checkupdates();
        }

        private void imageModernButton6_Click_1(object sender, EventArgs e)
        {
            Events teste = new Events();
            teste.ShowDialog();
        }
    public ProfileVoidHelper.GlobalPerfilBox CurrentPerfil { get; set; }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (perfilselector.Items.Count <= 0)
            {
                return;
            }
            int intselectedindex = perfilselector.SelectedIndex;
            if (intselectedindex >= 0)
            {
                CurrentPerfil = (ProfileVoidHelper.GlobalPerfilBox)perfilselector.Items[intselectedindex];

                //CurrentDevice.CheckCurrentFolder();
                if (Debugger.IsAttached && DevicePersistManager.IsDeviceTest)
                {

                    ProfileTestDeckHelper.SelectCurrentDevicePerfil(CurrentPerfil.Value, DevicePersistManager.DeviceTest);
                    RefreshAllButtons(false);
                    return;
                }
               
                ProfileStaticHelper.SelectCurrentDevicePerfil(CurrentPerfil.Value);

              RefreshAllButtons(true);

                
              
                //do something
                //MessageBox.Show(listView1.Items[intselectedindex].Text); 
            }


          



        }
        public void PerfilSelector()
        {



            //  perfilselector.SelectedIndex = perfilselector.Items.IndexOf(ApplicationSettingsManager.Settings.CurrentProfile);

         





        }
     
        public void FillPerfil()
        {
            MainForm.Instance.Invoke(new Action(() =>
            {

                perfilselector.Items.Clear();
    
                foreach (var perfil in DevicePersistManager.PersistedDevices.ToList())
                {

                    foreach (var list in perfil.profiles)
                    {
                        ProfileVoidHelper.GlobalPerfilBox teste = new ProfileVoidHelper.GlobalPerfilBox();
                        teste.Text = list.Name;
                        teste.Value = list;
                        perfilselector.Items.Add(teste);


                    }
                }
            
            }));
        }
        
        
        private void imageModernButton7_Click(object sender, EventArgs e)
        {
            new ProfileVoidHelper().AddPerfil();
        }

        private void imageModernButton8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(String.Format(ActionImagePlaceHolder.Text = Texts.rm.GetString("MESSAGEBOXPERFIL", Texts.cultereinfo), CurrentPerfil.Value.Name), Texts.rm.GetString("MESSAGEBOXPERFIL_ALERT", Texts.cultereinfo), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ProfileStaticHelper.RemovePerfil(CurrentPerfil.Value);
            }
           
        }

      

        private void statusStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        
        }

        private void imageModernButton9_Click(object sender, EventArgs e)
        {
            new MagnetiteForm().ShowDialog();
        }

        private void camada1_Click(object sender, EventArgs e)
        {
            RadioButton mb = (sender as RadioButton);
                if(mb.Tag is IDeckItem FF)
            {
                ActionImagePlaceHolder.TextButton = new TextLabel(FF.GetDeckDefaultLayer);

                FocusItemPropertiesOptions(FF.GetDeckDefaultLayer);
              
            } if(mb.Tag is DynamicDeckItem DeckItem && DeckItem.DeckAction != null)
            { 
                LoadProperties(DeckItem, flowLayoutPanel1);

            }
             if (mb.Tag is DynamicDeckFolder DeckFolder)
            {
                LoadPropertiesFolder(DeckFolder, flowLayoutPanel1);

            }
            ActionImagePlaceHolder.Camada = 1;
                UpdateLayerView();

        }

        private void camada2_Click(object sender, EventArgs e)
        {
            RadioButton mb = (sender as RadioButton);
            if (mb.Tag is IDeckItem FF)
            {
                ActionImagePlaceHolder.TextButton = new TextLabel(FF.GetDeckLayerTwo);

                FocusItemPropertiesOptions(FF.GetDeckLayerTwo);

            }
             if (mb.Tag is DynamicDeckItem DeckItem && DeckItem.DeckAction != null) 
            {
                LoadProperties(DeckItem, flowLayoutPanel1);

            }
             if (mb.Tag is DynamicDeckFolder DeckFolder)
            {
                LoadPropertiesFolder(DeckFolder, flowLayoutPanel1);

            }
            ActionImagePlaceHolder.Camada = 2;

            UpdateLayerView();

        }

        private void imageModernButton1_Click(object sender, EventArgs e)
        {
           // new TransparentTwitchChatWPF.MainWindow().Show();

            DevicePersistManager.IsDeviceTest = true;

            DevicePersistManager.DeviceTest = new DeckDevice(new Guid("161fb525-7004-4cb1-9487-6f5106af32da"), "Teste");
           
            CurrentDevice = DevicePersistManager.DeviceTest;
           
           //DevicePersistManager.DeviceTest = deckDevice;
            DevicePersistManager.PersistDevice(DevicePersistManager.DeviceTest);
           


         
            ProfileTestDeckHelper.SetupPerfil(DevicePersistManager.DeviceTest);

    //        ProfileTestDeckHelper.SelectCurrentDevicePerfil(CurrentPerfil.Value, DevicePersistManager.DeviceTest);
            DevicePersistManager.OnDeviceConnected(this, DevicePersistManager.DeviceTest);
        }

        private void imageModernButton3_Click_1(object sender, EventArgs e)
        {
            DevicePersistManager.SaveDevices();
        }

        private void imageModernButton5_Click(object sender, EventArgs e)
        {

            DevicePersistManager.LoadDevices();
            MatrizGenerator(DevicePersistManager.DeviceTest.CurrentProfile);
            ChangeToDevice(DevicePersistManager.DeviceTest);

        }
    }
    #endregion
}

#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body