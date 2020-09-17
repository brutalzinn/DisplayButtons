﻿
using DisplayButtons.Backend.Networking;
using DisplayButtons.Backend.Networking.Implementation;
using DisplayButtons.Backend.Objects;
using DisplayButtons.Backend.Objects.Implementation;
using DisplayButtons.Backend.Objects.Implementation.DeckActions.General;
using DisplayButtons.Backend.Utils;
using DisplayButtons.Bibliotecas;
using DisplayButtons.Controls;
using DisplayButtons.Misc;
using DisplayButtons.Properties;
using Cyotek.Windows.Forms;
using NetSparkleUpdater;
using NetSparkleUpdater.Configurations;
using NetSparkleUpdater.SignatureVerifiers;
using Newtonsoft.Json;
using NHotkey;
using NHotkey.WindowsForms;
using NickAc.ModernUIDoneRight.Controls;
using NickAc.ModernUIDoneRight.Objects;
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
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DisplayButtons.Backend.Objects.AbstractDeckAction;
using Timer = System.Windows.Forms.Timer;
using NickAc.ModernUIDoneRight.Objects.MenuItems;
using DisplayButtons.Forms.EventSystem;
using DisplayButtons.Forms.EventSystem.Controls.triggers;
using DisplayButtons.Bibliotecas.DeckEvents;
using static DisplayButtons.Bibliotecas.DeckEvents.FactoryForms;
using static DisplayButtons.Backend.Objects.ProfileVoidHelper;
using DisplayButtons.Bibliotecas.DeckText;

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
            Globals.events.On("languagechanged", (e) => {


                MainForm.Instance.Invoke(new Action(() =>
                {
                    this.Text = Texts.rm.GetString("APPLICATIONNAME", Texts.cultereinfo);
                    perfil_info.Text = Texts.rm.GetString("PERFILINFOLABEL", Texts.cultereinfo);
                    warning_label.Text = Texts.rm.GetString("WARNINGLABELTEXT", Texts.cultereinfo);
                    deckoptions_button.Text = Texts.rm.GetString("BUTTONDECKMISCELLANEOUS", Texts.cultereinfo);

                }));

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
            RegisterMainFolderHotKey();

            RegisterBackFolderHotKey();


          ProfileStaticHelper.SetupPerfil();

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
                        CurrentDevice.CurrentProfile.Mainfolder = new DynamicDeckFolder();
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

             

                appBar1.Actions.Add(itemBiblioteca);
 
                // ApplyTheme(panel1);
                GenerateSidebar(shadedPanel1, true);
           

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

            if (!string.IsNullOrEmpty(resultMainFolder))
            {
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

            if (!string.IsNullOrEmpty(resultBackFolder))
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
                        else if (mb.Tag != null && CurrentDevice.CurrentProfile.Currentfolder != null && CurrentDevice.CurrentProfile.Currentfolder.GetParent() != null && mb.CurrentSlot == 1 && mb.Tag is IDeckItem upItem)
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


                                    if (CurrentDevice.CurrentProfile.Currentfolder.GetParent() != null && mb.CurrentSlot == 1) return;
                                    if (item is IDeckFolder deckFolder)
                                    {
                                        var deckItemToAdd = new DynamicDeckItem
                                        {
                                            DeckAction = action.DeckAction.CloneAction()
                                        };
                                        var id2 = deckFolder.Add(deckItemToAdd);
                                       
                                        deckItemToAdd.DeckImage = new DeckImage(action.DeckAction.GetDefaultItemImage()?.Bitmap ?? Resources.img_item_default);

                                        CurrentDevice.CurrentProfile.Currentfolder = deckFolder;
                                        RefreshAllButtons();
                                        //implementação dos plugins



                                        FocusItem(GetButtonControl(id2), deckItemToAdd);

                                        return;
                                    }
                                    var folder = new DynamicDeckFolder
                                    {
                                        DeckImage = new DeckImage(Resources.img_folder)
                                    };
                                    //Create a new folder instance
                                    CurrentDevice.CheckCurrentFolder();
                                    folder.ParentFolder = CurrentDevice.CurrentProfile.Currentfolder;
                                    folder.Add(1, folderUpItem);
                                    folder.Add(item);

                                    var newItem = new DynamicDeckItem
                                    {
                                        DeckAction = action.DeckAction.CloneAction(),
                                        DeckImage = new DeckImage(action.DeckAction.GetDefaultItemImage()?.Bitmap ?? Resources.img_item_default)
                                    };

                                    var id = folder.Add(newItem);

                                    FocusItem(GetButtonControl(id), newItem);

                                    CurrentDevice.CurrentProfile.Currentfolder.Add(mb.CurrentSlot, folder);

                                    mb.Tag = folder;
                                    mb.Image = Resources.img_folder;
                                    CurrentDevice.CurrentProfile.Currentfolder = folder;
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
                                        LoadPropertiesPlugins(TT, action.ToScript, action.ToExecute, action.ToName);
                                        YY.SetConfigs(action.ToScript);
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

                                var newFolder = new DynamicDeckFolder
                                {
                                    DeckImage = new DeckImage(Resources.img_folder)
                                };
                                //Create a new folder instance



                                //parei aqui

                                if (oldItem is DynamicDeckFolder deckFolder)
                                {
                                    CurrentDevice.CheckCurrentFolder();
                                    deckFolder.ParentFolder = CurrentDevice.CurrentProfile.Currentfolder;
                                    deckFolder.Add(1, folderUpItem);
                                    Debug.WriteLine("Mesclagem de pasta.");
                                    deckFolder.Add(action1.DeckItem);
                                    CurrentDevice.CurrentProfile.Currentfolder.Add(mb.CurrentSlot, deckFolder);

                                    CurrentDevice.CurrentProfile.Currentfolder = deckFolder;


                                    //   mb.DoDragDrop(new DeckItemMoveHelper(action1.DeckItem, deckFolder, mb.CurrentSlot) { CopyOld = ModifierKeys.HasFlag(Keys.Control) }, ModifierKeys.HasFlag(Keys.Control) ? DragDropEffects.Copy : DragDropEffects.Move);


                                }
                                else
                                {

                                    CurrentDevice.CheckCurrentFolder();
                                    newFolder.ParentFolder = CurrentDevice.CurrentProfile.Currentfolder;
                                    newFolder.Add(1, folderUpItem);

                                    newFolder.Add(oldItem);
                                    newFolder.Add(action1.DeckItem);
                                    CurrentDevice.CurrentProfile.Currentfolder.Add(mb.CurrentSlot, newFolder);
                                    CurrentDevice.CurrentProfile.Currentfolder = newFolder;
                                    Debug.WriteLine("Dois itens.");
                                    FolderCallBack(newFolder);
                                    // call event
                                }




                                RefreshAllButtons(true);

                            }
                            else
                            {
                                CurrentDevice.CurrentProfile.Currentfolder.Add(mb.CurrentSlot, item1);
                                RefreshButton(action1.OldSlot, true);
                              //  ClearSingleItemToDevice(CurrentDevice, action1.OldSlot);
                                RefreshButton(mb.CurrentSlot, true);
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

        public void FolderCallBack(DynamicDeckFolder folder, bool isForDelete = false)
        {


            if (!isForDelete)
            {
        //        GlobalHotKeys.Instance.RegisterHotKeyCollector(folder);

            }
            else
            {
                //    GlobalHotKeys.folder_form.Add(new GlobalHotKeys.folders(folder));
         //       GlobalHotKeys.Instance.GarbageHotKeyCollector(folder);
                //   GlobalHotKeys.hotKeyManager.Unregister(GlobalHotKeys.VirtualKeyFromKey(folder.KeyGlobalValue.Keys), GlobalHotKeys.VirtualKeyFromKeyModifier(folder.KeyGlobalValue.ModifierKeys));
            }
            //   GlobalHotKeys.Instance.init();



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
                                            if (senderB.Image != Resources.img_folder && senderB.Image != Resources.img_item_default)
                                            {
                                                senderB.Image.Dispose();
                                            }
                                            if (senderB.Tag is DynamicDeckFolder item)
                                            {



                                                FolderCallBack(item, true);
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
                                                senderB.Image = isFolder ? Resources.img_folder : ((IDeckItem)senderB.Tag).GetDefaultImage()?.Bitmap ?? Resources.img_item_default;
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
                                if (e.Button == MouseButtons.Left && DevicePersistManager.IsVirtualDeviceConnected && ModifierKeys == Keys.Shift)
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
                                                var newFolder = new DynamicDeckFolder
                                                {
                                                    DeckImage = new DeckImage(Resources.img_folder)
                                                };
                                                newFolder.ParentFolder = CurrentDevice.CurrentProfile.Currentfolder;
                                                newFolder.Add(1, folderUpItem);

                                                CurrentDevice.CurrentProfile.Currentfolder.Add(mb.CurrentSlot, newFolder);
                                                FolderCallBack(newFolder);
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
                                        if ((CurrentDevice.CurrentProfile.Currentfolder.GetParent() != null && (mb.CurrentSlot == 1))) return;
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
                            Debug.WriteLine("CLICANDO");
                            Debug.WriteLine(control.Name);

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
                                        RefreshAllButtons();
                                        goto end;
                                    }
                                    if (CurrentDevice.CurrentProfile.Currentfolder.GetParent() != null)
                                    {
                                            //Not on the main folder
                                            if (mb.CurrentSlot == 1)
                                        {
                                            CurrentDevice.CurrentProfile.Currentfolder = CurrentDevice.CurrentProfile.Currentfolder.GetParent();
                                            RefreshAllButtons();
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


                            RefreshAllButtons(true);

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

            if (Globals.can_refresh == false) { return; }

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

                if (item is DynamicDeckItem FF && FF.DeckAction is PluginLuaGenerator TT)
                {
                    // Debug.WriteLine("PluginLuaGenerator: " + GetPropertiesPlugins(FF, "ScriptNamePoint"));


                    LoadPropertiesPlugins(FF, GetPluginScript(GetPropertiesPlugins(FF, "ScriptNamePoint")));
                    TT.SetConfigs(GetPluginScript(GetPropertiesPlugins(FF, "ScriptNamePoint")));





                }
                if (item is DynamicDeckFolder EE && EE != null && EE.KeyGlobalValue != null && EE.UniqueID != null)
                {
                    new GlobalHotKeys().refreshFolder(EE);

                }

                //control.TextLabel(item?.DeckName, this.Font, Brushes.Black, new PointF(25, 3));

                control.NormalImage = item?.GetItemImage().Bitmap;

                control.TextButton = new TextLabel(item);

                control.Tag = item;
                control.Invoke(new Action(control.Refresh));

                CurrentDevice.CheckCurrentFolder();
                if (sendToDevice)
                {

                    SendItemsToDevice(CurrentDevice, CurrentDevice.CurrentProfile.Currentfolder);

                }

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
            ClearSingleItemToDevice(CurrentDevice, slot);

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
           

                    control.NormalImage = item?.GetItemImage().Bitmap;
                    control.Text = item?.Deckname;
                    control.TextButton = new TextLabel(item);
                    control.Tag = item;
                    control.Invoke(new Action(control.Refresh));
 CurrentDevice.CheckCurrentFolder();
            if (sendToDevice)
            {
                SendSingleItemToDevice(CurrentDevice, slot, item);

            }


                }
             
            }
           
            // 
        }
        public static void SendSingleItemToDevice(DeckDevice device,int slot, IDeckItem item)
        {
            var con = device.GetConnection();
            if (con != null)
            {
                bool isFolder = false;
              
                var image = item.GetItemImage() ?? item.GetDefaultImage() ?? (new DeckImage(isFolder ? Resources.img_folder : Resources.img_item_default));
                var seri = image.BitmapSerialized;
                con.SendPacket(new SingleUniversalChangePacket(image)
                {
                    ImageSlot = slot,
                    CurrentItem = item

                });


            }


            }
        public static void ClearSingleItemToDevice(DeckDevice device, int slot)
        {
            var con = device.GetConnection();
            con.SendPacket(new SlotImageClearPacket(slot));
     
        }

            private ImageModernButton GetButtonControl(int id)
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
           
           
               if(perfilselector.Items.Count == 0)
            {
                ProfileStaticHelper.SetupPerfil();
            }
                if (CurrentPerfil != null)
                {
                    ProfileStaticHelper.SelectCurrentDevicePerfil(CurrentPerfil.Value);
                }

          
            //     folder_globals_keys = ListFolders(CurrentDevice.MainFolder as DynamicDeckFolder);

            if (ApplicationSettingsManager.Settings.isFolderBrowserEnabled)
            {
                GenerateFolderList(shadedPanel4);
            }

            //  GetAllFolders(CurrentDevice.MainFolder);
         
           
       
            
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
            if (CurrentDevice.CurrentProfile != null)
            {
                LoadItems(CurrentDevice.CurrentProfile.Currentfolder);
            }
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
                            if (device.CurrentProfile.Currentfolder.GetItemIndex(item) == 1)
                            {
                                if (e.PerformedAction != ButtonInteractPacket.ButtonAction.ButtonUp) return;
                                //Navigate one up!
                                device.CurrentProfile.Currentfolder = device.CurrentProfile.Currentfolder.GetParent();
                                SendItemsToDevice(CurrentDevice, device.CurrentProfile.Currentfolder);
                                //      AddWatermark(deckItem.DeckAction.GetActionName(), ((IDeckItem)imageModernButton1.Origin.Tag).GetDefaultImage().Bitmap, "Arial", 7, 20f, 67f, Brushes.White, item, device.CurrentProfile.Currentfolder.GetParent());

                                RefreshAllButtons(false);
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
                        RefreshAllButtons(false);
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
                bool isFolder = false;
                for (int i = 0; i < Instance.CurrentDevice.CurrentProfile.Matriz.Calc; i++)
                {
                    IDeckItem item = null;
                    if (items.ElementAtOrDefault(i) != null)
                    {
                        item = items[i];
                        addedItems.Add(folder.GetItemIndex(item));
                    }

                    if (item == null) break;
                  
                    if (item is DynamicDeckFolder)
                    {

                        isFolder = true;
                    }
                    var image = item.GetItemImage() ?? item.GetDefaultImage() ?? (new DeckImage(isFolder ? Resources.img_folder : Resources.img_item_default));
                    var seri = image.BitmapSerialized;



                    packet.AddToQueue(folder.GetItemIndex(item), item, image);

                }

                con.SendPacket(packet);
                //  con.SendPacket(packet_label);
                //    con.SendPacket(packet_label);
                var clearPacket = new SlotImageClearChunkPacket();
                //  var clearPacket_labels = new SlotLabelButtonClearChunkPacket();
                for (int i = 1; i < Instance.CurrentDevice.CurrentProfile.Matriz.Calc + 1; i++)
                {
                    if (addedItems.Contains(i)) 
                        continue;
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
                //This is when it loads.
                //It will load from the persisted device.

                bool isFolder = item is IDeckFolder;
                ImageModernButton control = Controls.Find("modernButton" + folder.GetItemIndex(item), true).FirstOrDefault() as ImageModernButton;
                var image = item.GetItemImage() ?? item.GetDefaultImage() ?? (new DeckImage(isFolder ? Resources.img_folder : Resources.img_item_default));
                var seri = image.BitmapSerialized;
                if (item is DynamicDeckItem DI && DI.DeckAction != null)
                {


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
        private static DynamicDeckItem folderUpItem = new DynamicDeckItem() { DeckImage = defaultDeckImage };

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
        public static List<DynamicDeckFolder> ListFolders(DynamicDeckFolder initialFolder)
        {
            var folders = new List<DynamicDeckFolder>();
            folders.Add(initialFolder);
            foreach (var f in initialFolder.GetSubFolders())
            {
                if (f is DynamicDeckFolder DF)
                {

                    folders.AddRange(ListFolders(DF));

                }

            }
            return folders;
        }
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

                        Debug.WriteLine(item.Name + "/" + item.Folder_id);

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



                    folder_form.Add(new folders(PP.Deckname, PP, false, root));
                    // AddFolderInPanelList(shadedPanel1, PP.folder_name, PP, root, false, root);
                    //   Debug.WriteLine(PP.folder_name + "CC:" + PP.GetSubFolders().Count);             

                    if (abacate.GetSubFolders().Count == 0)
                    {
                        if (CurrentDevice.CurrentProfile.Mainfolder.GetSubFolders().Count - 1 > root)
                        {

                            if (CurrentDevice.CurrentProfile.Mainfolder.GetSubFolders()[root] is DynamicDeckFolder pastapai)
                            {
                                folder_form.Add(new folders(pastapai.Deckname, pastapai, true, root));
                            }
                            AddFolderInPanelList(root);
                            root++;
                        }
                        else
                        {
                            if (CurrentDevice.CurrentProfile.Mainfolder.GetSubFolders()[root] is DynamicDeckFolder pastapai)
                            {
                                folder_form.Add(new folders(pastapai.Deckname, pastapai, true, root));
                            }
                            Debug.WriteLine("ROOT: " + root);
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
                        folder.Text = item.Deckname;
                        folder.Height = TextRenderer.MeasureText(item.Deckname, itemFont).Height;
                        toAdd.Add(folder);
                        foreach (DynamicDeckFolder subitem in ListFolders(item).Skip(1))
                        {

                            Label subfolder = new Label();
                            subfolder.Padding = itemPadding;
                            subfolder.TextAlign = ContentAlignment.MiddleLeft;

                            subfolder.Font = itemFont;

                            subfolder.Dock = DockStyle.Top;
                            subfolder.Text = "..." + subitem.Deckname;
                            subfolder.Height = TextRenderer.MeasureText("..." + subitem.Deckname, itemFont).Height;
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
                        folder.Text = item.Deckname;
                        folder.Height = TextRenderer.MeasureText(item.Deckname, itemFont).Height;
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
                          Debug.WriteLine("TAG VINDO: " + i2);
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


                    imageModernButton1.Image = bmp;
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
                        RefreshAllButtons();
                        goto end;
                    }
                    if (CurrentDevice.CurrentProfile.Currentfolder.GetParent() != null)
                    {
                        //Not on the main folder
                        if (mb.CurrentSlot == 1)
                        {
                            CurrentDevice.CurrentProfile.Currentfolder = CurrentDevice.CurrentProfile.Currentfolder.GetParent();
                            RefreshAllButtons();
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

                                    GlobalHotKeys.Instance.GarbageHotKeyCollector(ttt);
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


                    imageModernButton1.Image = bmp;
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

                list.Add(new KeyValuePair<string, int>(desc, i));
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
            [Description("baixo")]
            ANDROID_baixo = 81,

            [Description("cima")]
            ANDROID_cima = 49,

            [Description("meio")]
            ANDROID_meio = 17
        }

        private void FocusItem(ImageModernButton mb, IDeckItem item)
        {
            flowLayoutPanel1.Controls.OfType<Control>().All(c =>
            {
                c.Dispose();
                return true;
            });

            flowLayoutPanel1.Controls.Clear();
            if (item is IDeckItem dI)
            {





                ModernButton myButton = new ModernButton();
                ModernButton myColor = new ModernButton();
                ModernButton myColorShadow = new ModernButton();
                Label myTextNameInformation = new Label();
                Label sizeLabelInfo = new Label();
                Label positionLabelInfo = new Label();
                Label StrokeLabelInfo = new Label();
                Label StrokeLabelColor = new Label();
                Label StrokeLabelDX = new Label();
                Label StrokeLabelDY = new Label();
                Label StrokeLabelRADIUS = new Label();
                TextBox sizeLabelTextBox = new TextBox();
                ComboBox PositionComboBox = new ComboBox();
                TextBox myNameText = new TextBox();
                TextBox myColorText = new TextBox();
                CheckBox IsStrokeCheckbox = new CheckBox();
                CheckBox IsHintText = new CheckBox();
                CheckBox IsBoldText = new CheckBox();
                CheckBox IsItalicText = new CheckBox();
                CheckBox IsNormalText = new CheckBox();
                TextBox shadow_stroke_radiustextfloat = new TextBox();
                TextBox shadow_stroke_dxtextfloat = new TextBox();
                TextBox shadow_stroke_dytextfloat = new TextBox();
                TextBox shadow_stroke_color = new TextBox();
                myColor.Size = new Size(70, 30);
                IsHintText.Text = Texts.rm.GetString("DECKITEMHINTTEXT", Texts.cultereinfo);
                IsBoldText.Text = Texts.rm.GetString("DECKITEMBOLDTEXT", Texts.cultereinfo);
                IsItalicText.Text = Texts.rm.GetString("DECKITEMITALICTEXT", Texts.cultereinfo);
                IsNormalText.Text = Texts.rm.GetString("DECKITEMNORMALTEXT", Texts.cultereinfo);
                IsStrokeCheckbox.Text = "Enable Stroke Effect on text";
                myColor.Text = Texts.rm.GetString("DECKITEMSELECTCOLOR", Texts.cultereinfo);
                myColorShadow.Size = new Size(70, 30);
                myColorShadow.Text = Texts.rm.GetString("DECKITEMSELECTCOLOR", Texts.cultereinfo);
                myTextNameInformation.Text = Texts.rm.GetString("DECKITEMTEXT", Texts.cultereinfo);
                StrokeLabelInfo.Text = Texts.rm.GetString("DECKITEMSTROKE", Texts.cultereinfo);
                StrokeLabelRADIUS.Text = Texts.rm.GetString("DECKITEMSTROKERADIUS", Texts.cultereinfo);
                StrokeLabelDX.Text = Texts.rm.GetString("DECKITEMSTROKEDX", Texts.cultereinfo);
                StrokeLabelDY.Text = Texts.rm.GetString("DECKITEMSTROKEDY", Texts.cultereinfo);
                StrokeLabelColor.Text = Texts.rm.GetString("DECKITEMSTROKECOLOR", Texts.cultereinfo);
                positionLabelInfo.Text = Texts.rm.GetString("DECKITEMPOSITION", Texts.cultereinfo);
                sizeLabelInfo.Text = Texts.rm.GetString("DECKITEMSIZE", Texts.cultereinfo); 
                FlowLayoutPanel painel_name = new FlowLayoutPanel();
                FlowLayoutPanel painel_color = new FlowLayoutPanel();
                FlowLayoutPanel painel_tamanho = new FlowLayoutPanel();
                FlowLayoutPanel painel_position = new FlowLayoutPanel();
                FlowLayoutPanel painel_shadowstroke= new FlowLayoutPanel();
                myColor.Dock = DockStyle.None;
                myColorText.Dock = DockStyle.None;
                sizeLabelTextBox.Dock = DockStyle.None;
                sizeLabelInfo.Dock = DockStyle.None;
                positionLabelInfo.Dock = DockStyle.None;
                StrokeLabelInfo.Dock = DockStyle.None;
                PositionComboBox.Dock = DockStyle.None;
                shadow_stroke_radiustextfloat.Dock = DockStyle.None;
                shadow_stroke_dxtextfloat.Dock = DockStyle.None;
                shadow_stroke_dytextfloat.Dock = DockStyle.None;
                shadow_stroke_color.Dock = DockStyle.None;
                myNameText.Text = dI.Deckname;
                myColorText.Text = dI.Deckcolor;

                IsStrokeCheckbox.Checked = dI.IsStroke;
                shadow_stroke_color.Text = dI.Stroke_color;
                shadow_stroke_dytextfloat.Text = dI.Stroke_Dy.ToString();
                shadow_stroke_dxtextfloat.Text = dI.Stroke_dxtext.ToString();
                shadow_stroke_radiustextfloat.Text = dI.Stroke_radius.ToString();
                IsNormalText.Checked = dI.Isnormaltext;
                IsHintText.Checked = dI.Ishinttext;
                IsItalicText.Checked = dI.Isitalictext;
                IsBoldText.Checked = dI.Isboldtext;

                // PositionComboBox.SelectedValue = (int)dI.DeckPosition;



                sizeLabelTextBox.Text = dI.Decksize.ToString();
                myColor.Click += (s, e) =>
                {

                    using (ColorPickerDialog dialog = new ColorPickerDialog())
                    {
                        dialog.PreviewColorChanged += this.DialogColorChangedHandler;

                        if (dialog.ShowDialog(this) == DialogResult.OK)
                        {
                            //    Debug.WriteLine("COLORCODE:" + dialog.Color.Name);
                            string result = ColorTranslator.ToHtml(Color.FromArgb(dialog.Color.ToArgb()));
                            myColorText.Text = result;
                        }

                        dialog.PreviewColorChanged -= this.DialogColorChangedHandler;
                    }

                };

                myColorShadow.Click += (s, e) =>
                {

                    using (ColorPickerDialog dialog = new ColorPickerDialog())
                    {
                        dialog.PreviewColorChanged += this.DialogColorChangedHandler;

                        if (dialog.ShowDialog(this) == DialogResult.OK)
                        {
                            //    Debug.WriteLine("COLORCODE:" + dialog.Color.Name);
                            string result = ColorTranslator.ToHtml(Color.FromArgb(dialog.Color.ToArgb()));
                            shadow_stroke_color.Text = result;
                        }

                        dialog.PreviewColorChanged -= this.DialogColorChangedHandler;
                    }

                };



                IsHintText.Click += (s, e) =>
                {

                    IsNormalText.Checked = false;
  };
                IsBoldText.Click += (s, e) =>

                {

                    IsNormalText.Checked = false;

                };

                IsItalicText.Click += (s, e) =>

                {
                    IsNormalText.Checked = false;

                };

            IsNormalText.Click += (s, e) =>
                {
                    if (IsNormalText.Checked)
                    {
                        IsHintText.Checked = false;
                        IsBoldText.Checked = false;
                        IsItalicText.Checked = false;
                    }
                
                
                };












                painel_position.Size = new Size(190, 60);

                painel_color.Size = new Size(190, 30);

                painel_name.Size = new Size(190, 180);
                painel_tamanho.Size = new Size(190, 50);
                painel_shadowstroke.Size = new Size(190, 300);
                painel_shadowstroke.WrapContents = true;
      
                painel_tamanho.WrapContents = true;
                painel_name.WrapContents = true;
                //painel 1
                painel_color.WrapContents = true;
                painel_position.WrapContents = true;
                painel_position.Controls.Add(positionLabelInfo);

                painel_position.Controls.Add(PositionComboBox);
                painel_shadowstroke.Controls.Add(StrokeLabelInfo);
                painel_shadowstroke.Controls.Add(IsStrokeCheckbox);
                painel_shadowstroke.Controls.Add(StrokeLabelRADIUS);
                painel_shadowstroke.Controls.Add(shadow_stroke_radiustextfloat);
                painel_shadowstroke.Controls.Add(StrokeLabelDX);
                painel_shadowstroke.Controls.Add(shadow_stroke_dxtextfloat);
                painel_shadowstroke.Controls.Add(StrokeLabelDY);
                painel_shadowstroke.Controls.Add(shadow_stroke_dytextfloat);
                painel_shadowstroke.Controls.Add(StrokeLabelColor);
                painel_shadowstroke.Controls.Add(shadow_stroke_color);
                painel_shadowstroke.Controls.Add(myColorShadow);

                painel_color.Controls.Add(myColor);
                painel_color.Controls.Add(myColorText);

                painel_name.Controls.Add(myTextNameInformation);
                painel_name.Controls.Add(myNameText);
                painel_name.Controls.Add(IsHintText);
                painel_name.Controls.Add(IsItalicText);
                painel_name.Controls.Add(IsBoldText);
                painel_name.Controls.Add(IsNormalText);
                painel_tamanho.Controls.Add(sizeLabelInfo);
                painel_tamanho.Controls.Add(sizeLabelTextBox);




               
                flowLayoutPanel1.Controls.Add(painel_name);
                flowLayoutPanel1.Controls.Add(painel_color);
                flowLayoutPanel1.Controls.Add(painel_tamanho);
                flowLayoutPanel1.Controls.Add(painel_position);
                flowLayoutPanel1.Controls.Add(painel_shadowstroke);

                flowLayoutPanel1.Controls.Add(myButton); 
                myButton.Text = "Salvar";

          

            

                setEnumValues(PositionComboBox, typeof(Position));
                myButton.Click += (s, e) =>
                {
                    dI.Deckname = myNameText.Text;
                    dI.Deckcolor = myColorText.Text;
                    dI.Decksize = Convert.ToInt32(sizeLabelTextBox.Text);
                    dI.Stroke_radius = Convert.ToSingle(shadow_stroke_radiustextfloat.Text);
                    dI.Stroke_Dy = Convert.ToSingle(shadow_stroke_dytextfloat.Text);
                    dI.Stroke_dxtext = Convert.ToSingle(shadow_stroke_dxtextfloat.Text);
                    dI.Stroke_color = shadow_stroke_color.Text;
                    dI.Deckposition = (int)PositionComboBox.SelectedValue;
                    dI.IsStroke = IsStrokeCheckbox.Checked;
                    dI.Isboldtext = IsBoldText.Checked;
                    dI.Ishinttext = IsHintText.Checked;
                    dI.Isitalictext = IsItalicText.Checked;
                    dI.Isnormaltext = IsNormalText.Checked;
                  int slot =  CurrentDevice.CurrentProfile.Currentfolder.GetItemIndex(dI);
                    RefreshButton(slot, true);




                };


                PositionComboBox.SelectedValue = dI.Deckposition;
                if (dI is DynamicDeckItem TT)
                {
                 LoadProperties(TT, flowLayoutPanel1);

                }
                if (dI is DynamicDeckFolder AA)
                {


                    LoadPropertiesFolder(AA, flowLayoutPanel1);
                }

            }
            imageModernButton1.Origin = mb;
            //Write_name_Image("testando", mb, 10f,10f,"Arial",10);
            imageModernButton1.Refresh();
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

                bool shouldUpdateIcon = Attribute.IsDefined(prop, typeof(ActionPropertyUpdateImageOnChangedAttribute));
                MethodInfo helperMethod = item.DeckAction.GetType().GetMethod(prop.Name + "Helper");
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

                    helperButton.Click += (sender, e) => helperMethod.Invoke(item.DeckAction, new object[] { });

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

                        cBox.Text = EnumUtils.GetDescription(prop.PropertyType, (Enum)prop.GetValue(item.DeckAction), ((Enum)prop.GetValue(item.DeckAction)).ToString());

                        cBox.SelectedIndexChanged += (s, e) =>
                        {
                            try
                            {
                                if (cBox.Text == string.Empty) return;
                                prop.SetValue(item.DeckAction, EnumUtils.FromDescription(prop.PropertyType, cBox.Text));
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
                        Text = (string)TypeDescriptor.GetConverter(prop.PropertyType).ConvertTo(prop.GetValue(item.DeckAction), typeof(string))
                    };
                    txt.TextChanged += (sender, e) =>
                    {
                        try
                        {
                            if (txt.Text == string.Empty) return;
                            //After loosing focus, convert type to thingy.
                            prop.SetValue(item.DeckAction, TypeDescriptor.GetConverter(prop.PropertyType).ConvertFrom(txt.Text));
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

        private string GetPropertiesPlugins(DynamicDeckItem item, string properties)
        {
            Type type = item.DeckAction.GetType();

            PropertyInfo propa = type.GetProperty(properties);
            return (string)TypeDescriptor.GetConverter(propa.PropertyType).ConvertTo(propa.GetValue(item.DeckAction), typeof(string));


        }
        private void LoadPropertiesPlugins(DynamicDeckItem item, string script, string entrypoint = "", string name = "")
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
            //  prop.SetValue(item.DeckAction, TypeDescriptor.GetConverter(prop.PropertyType).ConvertFrom(result_string));
            //         UpdateIcon(shouldUpdateIcon);




            //  ModifyColorScheme(flowLayoutPanel1.Controls.OfType<Control>());
        }
        public void button_creator(string name, string entry_point, string script)
        {
            //PluginLuaGenerator testando = new PluginLuaGenerator();

            // testando.script = script;


            List<Control> toAdd = new List<Control>();

            Padding categoryPadding = new Padding(5, 0, 0, 0);
            Font categoryFont = new Font(MainForm.instance.ShadedPanel1.Font.FontFamily, 13, FontStyle.Bold);
            Padding itemPadding = new Padding(25, 0, 0, 0);
            Font itemFont = new Font(MainForm.instance.ShadedPanel1.Font.FontFamily, 12);
            IList<PluginLuaGenerator> list = new List<PluginLuaGenerator>();
            // DisplayButtons.Forms.MainForm.testando(value);
            MainForm.instance.ShadedPanel1.Invoke((MethodInvoker)delegate
            {
                //Globals.launcher_principal.ShadedPanel1.Controls.Clear();
                
              

                //  PluginLuaGenerator testando = new PluginLuaGenerator();
                //  PluginLuaGenerator.name = name_button;

                //     PluginLuaGenerator.LuaExecuteButtonDown = buttondowm;
                //  PluginLuaGenerator.LuaExecuteButtonDown = //buttondowm;
                //     PluginLuaGenerator.DeckActionCategory_string = category;


                //    Debug.WriteLine("RRR" + testando.GetActionName());

                // Globals.launcher_principal.ShadedPanel1.Controls.Add(teste);
                MainForm.instance.ShadedPanel1.Refresh();

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
                                    var leste = new DeckActionHelper(act);
                                    leste.ToExecute = entry_point;
                                    leste.ToName = name;
                                    leste.ToScript = script;
                                    item.DoDragDrop(leste, DragDropEffects.Copy);
                                    //      i2.SetConfigs();
                                    //  LoadPropertiesPlugins(i2, script);

                                }


                            };


                            toAdd.Add(item);
                            //  i2.SetConfigs(name, script);








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
                    var ser = item.GetItemImage().BitmapSerialized;
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

                //    result = "";
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
        public void createPluginButton()
        {
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
           
           

            Package[] installedPackages = Workshop.GetInstalled();

            installedPackages.ToList().ForEach(x =>
            {
                Dictionary<string, string> packageInfo = x.GetInfo();


                MainForm.Instance.button_creator(x.GetInfo()["Name"], x.ArchivePath + "\\" + x.GetInfo()["EntryPoint"], x.ReadFileContents(x.GetInfo()["EntryPoint"]));

                MainForm.Instance.PluginLoaderScript(x.GetInfo()["Name"], x.ReadFileContents(x.GetInfo()["EntryPoint"]));
                //    MainForm.Instance.RefreshAllPluginsDependencies(x.ReadFileContents(x.GetInfo()["EntryPoint"]));
                //  MainForm.Instance.RefreshAllPluginsDependencies(x.ArchivePath + "\\" + x.GetInfo()["EntryPoint"]);

            });
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
                    FF.SetConfigs(script);
                }
                //  ImageModernButton control = Controls.Find("modernButton" + folder.GetItemIndex(item), true).FirstOrDefault() as ImageModernButton;
            }




        }
        private void UpdateIcon(bool shouldUpdateIcon)
        {
            // IDeckFolder folder = CurrentDevice?.CurrentProfile.Currentfolder;
            if (shouldUpdateIcon)
            {

                // IDeckItem item = null;
                // item = folder.GetDeckItems()[i];

                // AddWatermark("RWER", ((IDeckItem)imageModernButton1.Origin.Tag).GetDefaultImage().Bitmap, "Arial", 7, 20f, 67f, Brushes.White, item, folder);

                imageModernButton1.Image = ((IDeckItem)imageModernButton1.Origin.Tag).GetDefaultImage()?.Bitmap ?? Resources.img_item_default;
                //   var teste = new MagickImage((Bitmap)imageModernButton1.Image);
                //   image.Annotate("caption:This is gergerga test.", Gravity.South); // caption:"This is a test."
                // write the image to the appropriate directory
                // image.Write(@"D:\testimage.jpg");
                // SendItemsToDevice(CurrentDevice, CurrentDevice.CurrentProfile.Currentfolder,teste);

                imageModernButton1.Refresh();
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

        #endregion


        #region Events

        private bool mouseDown;
        private Point mouseDownLoc = Cursor.Position;

        private void ItemButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is ImageModernButton senderB)
            {
                if (e.Button == MouseButtons.Left && DevicePersistManager.IsVirtualDeviceConnected && ModifierKeys == Keys.Shift)
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
                                CurrentDevice.CheckCurrentFolder();
                                var newFolder = new DynamicDeckFolder
                                {
                                    DeckImage = new DeckImage(Resources.img_folder)
                                };
                                newFolder.ParentFolder = CurrentDevice.CurrentProfile.Currentfolder;
                                newFolder.Add(1, folderUpItem);

                                CurrentDevice.CurrentProfile.Currentfolder.Add(mb.CurrentSlot, newFolder);
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

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemButton_MouseMove(object sender, MouseEventArgs e)
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
                        if ((CurrentDevice.CurrentProfile.Currentfolder.GetParent() != null && (mb.CurrentSlot == 1))) return;
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
        }

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


            shadedPanel1.Controls.Clear();

            GenerateSidebar(shadedPanel1, false);
            ApplySidebarTheme(shadedPanel1);
        }

        public void openConsoleDeveloper()
        {
            Core.Initialize();
        }

        private void ImageModernButton2_Click(object sender, EventArgs e)
        {

            try
            {
                reloadButtons();
            }
            finally
            {

                reloadExternButtons();
            }
            ApplyTheme(panel_buttons);



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
            imageModernButton5.Text = Texts.rm.GetString("BUTTONRELOADEXTERNBUTTON", Texts.cultereinfo);

            imageModernButton3.Text = Texts.rm.GetString("BUTTONRELOADBUTTONS", Texts.cultereinfo);

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
            if (MessageBox.Show(String.Format(imageModernButton1.Text = Texts.rm.GetString("MESSAGEBOXPERFIL", Texts.cultereinfo), CurrentPerfil.Value.Name), Texts.rm.GetString("MESSAGEBOXPERFIL_ALERT", Texts.cultereinfo), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
    }
    #endregion
}

#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body