﻿using ButtonDeck.Backend.Networking.Implementation;
using ButtonDeck.Backend.Objects;
using ButtonDeck.Backend.Objects.Implementation;
using ButtonDeck.Backend.Objects.Implementation.DeckActions.General;
using ButtonDeck.Backend.Utils;
using ButtonDeck.Controls;
using ButtonDeck.Misc;
using ButtonDeck.Properties;
using Cyotek.Windows.Forms;
using ButtonDeck.Backend.Networking;

using NickAc.ModernUIDoneRight.Controls;
using NickAc.ModernUIDoneRight.Objects;
using ScribeBot;
using ScribeBot.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static ButtonDeck.Backend.Objects.AbstractDeckAction;
using System.Threading;
using Timer = System.Windows.Forms.Timer;
using static ButtonDeck.Backend.Utils.DevicePersistManager;
using ButtonDeck.Backend.Networking.TcpLib;
using System.Windows.Threading;
using ConnectionState = ButtonDeck.Backend.Networking.TcpLib.ConnectionState;
using SharpAdbClient;

#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body

namespace ButtonDeck.Forms
{
    public partial class MainForm : TemplateForm
    {
        private static MainForm instance;

        public static MainForm Instance {
            get {
                return instance;
            }
        }
        static Timer timer = new Timer();
        private const int CLIENT_ARRAY_LENGHT = 1024 * 50;

        #region Constructors
      

        public MainForm()
        {

            instance = this;
            InitializeComponent();

            //Globals.launcher_principal = this;
            panel1.Freeze();
            DevicesTitlebarButton item = new DevicesTitlebarButton(this);
            TitlebarButtons.Add(item);
            if (Program.Silent) {
                //Right now, we use the window redraw for device discovery purposes.
                //We need to simulate that with a timer.
                 Timer t = new Timer
                {
                    //We should run it every 2 seconds and half.
                    Interval = 2000
                };
                t.Tick += (s, e) => {
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
             
              
                NotifyIcon icon = new NotifyIcon
                {
                    Icon = Icon,
                    Text = Text
                };
                icon.DoubleClick += (sender, e) => {
                    Show();
                };
                ContextMenuStrip menu = new ContextMenuStrip();
                menu.Items.Add("Open application").Click += (s, e) => {
                    Show();
                };
                menu.Items.Add("-");
                menu.Items.Add("Exit application").Click += (s, e) => {
                    Application.Exit();
                };
                FormClosing += (s, e) => {
                    if (e.CloseReason == CloseReason.UserClosing) {
                        Hide();
                        e.Cancel = true;
                    } else if (e.CloseReason == CloseReason.ApplicationExitCall) {
                        icon.Visible = false;
                        icon.Dispose();
                    }
                };
                menu.Opening += (s, e) => {
                    menu.Items[0].Select();
                };
                icon.ContextMenuStrip = menu;
                icon.Visible = true;
            }
            ColorSchemeCentral.ThemeChanged += (s, e) =>
            ApplySidebarTheme(shadedPanel1);
            

        }
       
        public void ChangeButtonsVisibility(bool visible)
        {
            visible = true;
            if (Disposing || !IsHandleCreated) return;
            Invoke(new Action(() => {
                Control control2 = Controls["label1"];
                if (control2 != null) control2.Visible = !visible;

                Control control = Controls["panel1"];
                if (control != null) control.Visible = visible;
                Control control3 = Controls["shadedPanel1"];
                if (control3 != null) control3.Visible = visible;
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

            Globals.calc = ApplicationSettingsManager.Settings.coluna * ApplicationSettingsManager.Settings.linha;

            StartUsbMode();
            
      






            var image = ColorScheme.ForegroundColor == Color.White ? Resources.ic_settings_white_48dp_2x : Resources.ic_settings_black_48dp_2x;
            var imageTrash = ColorScheme.ForegroundColor == Color.White ? Resources.ic_delete_white_48dp_2x : Resources.ic_delete_black_48dp_2x;
            var imagePlugins = ColorScheme.ForegroundColor == Color.White ? Resources.Package_16x : Resources.Package_16x;
            var imageBiblioteca = ColorScheme.ForegroundColor == Color.White ? Resources.Folder_grey_16x : Resources.Folder_grey_16x;

            AppAction item = new AppAction()
            {
                Image = image
            };
            item.Click += (s, ee) => {
                //TODO: Settings
                new SettingsForm().ShowDialog();
            };
            appBar1.Actions.Add(item);

            AppAction itemTrash = new AppAction()
            {
                Image = imageTrash
            };
            itemTrash.Click += (s, ee) => {
                if (CurrentDevice == null) return;
                if (MessageBox.Show("Are you sure you  want to clear everything?" + Environment.NewLine + "All items will be lost!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                    CurrentDevice.MainFolder = new DynamicDeckFolder();
                    SendItemsToDevice(CurrentDevice, true);
                    RefreshAllButtons(false);
                }
            };
            appBar1.Actions.Add(itemTrash);

            AppAction itemMagnetite = new AppAction();

            itemMagnetite.Click += (s, ee) => {
                new MagnetiteForm().ShowDialog();
            };

            AppAction itemPlugins = new AppAction()
            {
                Image = imagePlugins
            };

            itemPlugins.Click += (s, ee) => {
                Core.Initialize();
                //     new ScribeBot.Interface.Window().Show();
            };

            AppAction itemBiblioteca = new AppAction()
            {
                Image = imageBiblioteca
            };

            itemBiblioteca.Click += (s, ee) => {
                new ImageListForm().ShowDialog();
                //     new ScribeBot.Interface.Window().Show();
            };

            appBar1.Actions.Add(itemMagnetite);
            appBar1.Actions.Add(itemPlugins);
            appBar1.Actions.Add(itemBiblioteca);
           // ApplyTheme(panel1);
            GenerateSidebar(shadedPanel1);
            ApplySidebarTheme(shadedPanel1);
            shadedPanel2.Hide();
            shadedPanel1.Hide();
            Refresh();

       

           

            Package[] installedPackages = Workshop.GetInstalled();

            installedPackages.ToList().ForEach(x =>
            {
                Dictionary<string, string> packageInfo = x.GetInfo();

                PackageInfo p = new PackageInfo();
                //     p.NameLabel.Text = packageInfo["Name"];
                //     p.AuthorLabel.Text = packageInfo["Authors"];
                //      p.DescLabel.Text = packageInfo["Description"];
                //     p.Namespace.Text = packageInfo["Namespace"];
                //      p.Package = x;

                //     p.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;

                //     InstalledPackagesList.Controls.Add(p);
                x.Run(false);

            });



          






            warning_label.ForeColor = ColorScheme.SecondaryColor;
        }


        const string OFFLINE_PREFIX = "[OFFLINE]";
        public new Padding Padding = new Padding(5);
        private bool _selected;
        private DeckDevice _deckDevice;
        private string deviceNamePrefix;
        public void StartUsbMode()
        {
       
            DevicePersistManager.DeviceConnected += DevicePersistManager_DeviceConnected;

            DevicePersistManager.DeviceDisconnected += DevicePersistManager_DeviceDisconnected;

      
        }
        private void ApplySidebarTheme(Control parent)
        {
            //Headers have the theme's secondary color as background
            //and the theme's foreground color as text color
            ApplicationColorScheme appTheme = ColorSchemeCentral.FromAppTheme(ApplicationSettingsManager.Settings.Theme);
            parent.Controls.OfType<Control>().All((c) => {
                if (c.Tag != null && c.Tag.ToString().ToLowerInvariant() == "header") {
                    c.BackColor = appTheme.SecondaryColor;
                    c.ForeColor = appTheme.ForegroundColor;
                } else {
                    c.BackColor = appTheme.BackgroundColor;
                }
                return true;
            });
        }
    
        private void ApplyTheme(Control parent)
        {
           
                //IMPORTANT
                ApplicationColorScheme appTheme = ColorSchemeCentral.FromAppTheme(ApplicationSettingsManager.Settings.Theme);
            parent.Controls.OfType<Control>().All((c) => {
                if (c is ImageModernButton mb) {
                    mb.AllowDrop = true;
                    mb.DragEnter += (s, ee) => {
                        if (mb.Tag != null && mb.Tag is IDeckFolder folder && !(ee.Data.GetDataPresent(typeof(DeckItemMoveHelper)))) {
                            CurrentDevice.CurrentFolder = folder;
                            RefreshAllButtons(true);
                        } else if (mb.Tag != null && CurrentDevice.CurrentFolder != null && CurrentDevice.CurrentFolder.GetParent() != null && mb.CurrentSlot == 1 && mb.Tag is IDeckItem upItem) {
                            CurrentDevice.CurrentFolder = CurrentDevice.CurrentFolder.GetParent();
                            RefreshAllButtons(true);
                        }

                        if (ee.Data.GetDataPresent(typeof(DeckActionHelper)))
                            ee.Effect = DragDropEffects.Copy;
                        else if (ee.Data.GetDataPresent(typeof(DeckItemMoveHelper))) {
                            var item = ee.Data.GetData(typeof(DeckItemMoveHelper)) as DeckItemMoveHelper;
                            ee.Effect = item.CopyOld ? DragDropEffects.Copy : DragDropEffects.Move;
                        }
                    };
                    mb.DragDrop += (s, ee) => {
                        if (ee.Data.GetData(typeof(DeckActionHelper)) is DeckActionHelper action)
                        {
                            if (ee.Effect == DragDropEffects.Copy)
                            {
                                if (mb.Tag != null && mb.Tag is IDeckItem item)
                                {
                                    if (CurrentDevice.CurrentFolder.GetParent() != null && mb.CurrentSlot == 1) return;
                                    if (item is IDeckFolder deckFolder)
                                    {
                                        var deckItemToAdd = new DynamicDeckItem
                                        {
                                            DeckAction = action.DeckAction.CloneAction()
                                        };
                                        var id2 = deckFolder.Add(deckItemToAdd);
                                        deckItemToAdd.DeckImage = new DeckImage(action.DeckAction.GetDefaultItemImage()?.Bitmap ?? Resources.img_item_default);

                                        CurrentDevice.CurrentFolder = deckFolder;
                                        RefreshAllButtons();

                                        FocusItem(GetButtonControl(id2), deckItemToAdd);

                                        return;
                                    }
                                    var folder = new DynamicDeckFolder
                                    {
                                        DeckImage = new DeckImage(Resources.img_folder)
                                    };
                                    //Create a new folder instance
                                    CurrentDevice.CheckCurrentFolder();
                                    folder.ParentFolder = CurrentDevice.CurrentFolder;
                                    folder.Add(1, folderUpItem);
                                    folder.Add(item);

                                    var newItem = new DynamicDeckItem
                                    {
                                        DeckAction = action.DeckAction.CloneAction(),
                                        DeckImage = new DeckImage(action.DeckAction.GetDefaultItemImage()?.Bitmap ?? Resources.img_item_default)
                                    };

                                    var id = folder.Add(newItem);

                                    FocusItem(GetButtonControl(id), newItem);

                                    CurrentDevice.CurrentFolder.Add(mb.CurrentSlot, folder);

                                    mb.Tag = folder;
                                    mb.Image = Resources.img_folder;
                                    CurrentDevice.CurrentFolder = folder;
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
                                    }
                                }
                            }
                            IDeckItem item1 = shouldMove ? action1.DeckItem : action1.DeckItem.DeepClone();
                            if (item1 is IDeckFolder folder && !shouldMove)
                            {
                                FixFolders(folder, false, CurrentDevice.CurrentFolder);
                            }
                            //alterando agora

                            if (CurrentDevice.CurrentFolder.GetDeckItems().Any(cItem => CurrentDevice.CurrentFolder.GetItemIndex(cItem) == mb.CurrentSlot))
                            {
                                //We must create a folder if there is an item
                                var oldItem = action1.OldFolder.GetDeckItems().First(cItem => action1.OldFolder.GetItemIndex(cItem) == mb.CurrentSlot);

                                var newFolder = new DynamicDeckFolder
                                {
                                    DeckImage = new DeckImage(Resources.img_folder)
                                };
                                //Create a new folder instance



                                //parei aqui

                                if (oldItem is IDeckFolder deckFolder)
                                {
                                    CurrentDevice.CheckCurrentFolder();
                                    Debug.WriteLine("Mesclagem de pasta.");
                                    deckFolder.Add(action1.DeckItem);
                                    CurrentDevice.CurrentFolder = deckFolder;
                                    //   mb.DoDragDrop(new DeckItemMoveHelper(action1.DeckItem, deckFolder, mb.CurrentSlot) { CopyOld = ModifierKeys.HasFlag(Keys.Control) }, ModifierKeys.HasFlag(Keys.Control) ? DragDropEffects.Copy : DragDropEffects.Move);


                                }
                                else
                                {

                                    CurrentDevice.CheckCurrentFolder();
                                    newFolder.ParentFolder = CurrentDevice.CurrentFolder;
                                    newFolder.Add(1, folderUpItem);

                                    newFolder.Add(oldItem);
                                    newFolder.Add(action1.DeckItem);
                                    CurrentDevice.CurrentFolder.Add(mb.CurrentSlot, newFolder);
                                    CurrentDevice.CurrentFolder = newFolder;
                                    Debug.WriteLine("Dois itens.");
                                }




                                RefreshAllButtons(true);

                            }
                            else
                            {
                                CurrentDevice.CurrentFolder.Add(mb.CurrentSlot, item1);
                                RefreshButton(action1.OldSlot, true);
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
        
        public void ButtonCreator()
        {

            Invoke(new Action(() =>
            {

                panel1.Controls.Clear();
                int x = 0;
                int y = 0;
                int id = 1;
                for (int con = 0; con < ApplicationSettingsManager.Settings.linha; con++)
                {
                    for (int lin = 0; lin < ApplicationSettingsManager.Settings.coluna; lin++)
                    {





                        ImageModernButton control = new ImageModernButton();


                        control.Name = "modernButton" + id;

                        control.Size = new Size(80, 80);
                        control.Location = new Point(lin * 110 + 10, con * 110 + 10);
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
                                if (e.Button == MouseButtons.Right && CurrentDevice.CurrentFolder.GetDeckItems().Any(c => CurrentDevice.CurrentFolder.GetItemIndex(c) == senderB.CurrentSlot))
                                {


                                    popupMenu.Items.Add("Remove item").Click += (s, ee) =>
                                    {
                                        if (senderB != null)
                                        {
                                            if (senderB.Image != Resources.img_folder && senderB.Image != Resources.img_item_default)
                                            {
                                                senderB.Image.Dispose();
                                            }
                                            senderB.Tag = null;
                                            senderB.Image = null;
                                            Buttons_Unfocus(sender, e);
                                            CurrentDevice.CurrentFolder.Remove(senderB.CurrentSlot);
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
                                                CurrentDevice.CheckCurrentFolder();
                                                var newFolder = new DynamicDeckFolder
                                                {
                                                    DeckImage = new DeckImage(Resources.img_folder)
                                                };
                                                newFolder.ParentFolder = CurrentDevice.CurrentFolder;
                                                newFolder.Add(1, folderUpItem);

                                                CurrentDevice.CurrentFolder.Add(mb.CurrentSlot, newFolder);
                                            //CurrentDevice.CurrentFolder = newFolder;
                                            RefreshAllButtons(true);

                                            }
                                        };


                                        menu.Items.Add("Atualizar").Click += (s, ee) =>
                                        {

                                            if (sender is ImageModernButton mb)
                                            {
                                            //CurrentDevice.CurrentFolder = newFolder;
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
                            IDeckFolder folder = CurrentDevice?.CurrentFolder;

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
                                        if ((CurrentDevice.CurrentFolder.GetParent() != null && (mb.CurrentSlot == 1))) return;
                                        mb.DoDragDrop(new DeckItemMoveHelper(act, CurrentDevice.CurrentFolder, mb.CurrentSlot) { CopyOld = ModifierKeys.HasFlag(Keys.Control) }, ModifierKeys.HasFlag(Keys.Control) ? DragDropEffects.Copy : DragDropEffects.Move);
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
                                        CurrentDevice.CurrentFolder = folder;
                                        RefreshAllButtons();
                                        goto end;
                                    }
                                    if (CurrentDevice.CurrentFolder.GetParent() != null)
                                    {
                                        //Not on the main folder
                                        if (mb.CurrentSlot == 1)
                                        {
                                            CurrentDevice.CurrentFolder = CurrentDevice.CurrentFolder.GetParent();
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
                        panel1.Controls.Add(control);




                        if (Globals.calc < id)
                        {


                            ApplyTheme(panel1);
                            Globals.can_refresh = true;


                        }

                        Application.DoEvents();
                        Refresh();
                    }



                }

            }));

            // Application.DoEvents();

        }

        public void RefreshAllButtons(bool sendToDevice = true)
        {

            if (Globals.can_refresh == false) { return; }
           
           // Buttons_Unfocus(this, EventArgs.Empty);
            IDeckFolder folder = CurrentDevice?.CurrentFolder;
           

            for (int j = 0; j < Globals.calc; j++)
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





                control.TextLabel(item?.DeckName, this.Font, Brushes.Black, new PointF(25, 3));
             
         control.NormalImage = item?.GetItemImage().Bitmap;


               
                control.Tag = item;
                control.Invoke(new Action(control.Refresh));
            
                CurrentDevice.CheckCurrentFolder();
                if (sendToDevice)
                {

                    SendItemsToDevice(CurrentDevice, CurrentDevice.CurrentFolder);

                }

            }

        }
        public void RefreshButton(int slot, bool sendToDevice = true)
        {
            Buttons_Unfocus(this, EventArgs.Empty);

            IDeckFolder folder = CurrentDevice?.CurrentFolder;
            ImageModernButton control1 = GetButtonControl(slot);
            //Label control_label = GetLabelControl(slot);
            // Label title_control = Controls.Find("titleLabel" + slot, true).FirstOrDefault() as Label;

            control1.NormalImage = null;
            control1.Tag = null;
            control1.Text = "";

            
            if (folder == null) control1.Invoke(new Action(control1.Refresh));

            if (folder == null) return;
            for (int i = 0; i < folder.GetDeckItems().Count; i++) {
                IDeckItem item = null;
                item = folder.GetDeckItems()[i];

                if (folder.GetItemIndex(item) != slot) continue;
                ImageModernButton control = Controls.Find("modernButton" + folder.GetItemIndex(item), true).FirstOrDefault() as ImageModernButton;
                Label control2 = Controls.Find("label" + folder.GetItemIndex(item), true).FirstOrDefault() as Label;

                //Label title_control = Controls.Find("titleLabel" + folder.GetItemIndex(item), true).FirstOrDefault() as Label;
                if (item != null) {
                    var ser = item.GetItemImage().BitmapSerialized;
                    //  control.NormalImage = null






                  
                        control.NormalImage = item?.GetItemImage().Bitmap;
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
            Invoke(new Action(() => {
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
            Console.WriteLine($"The device {e.Device.Name} has connected to this PC");

            Console.WriteLine("STARTING SERVER ON SMARTPHONE...");

//        timer.Start();




        }
        public static void DeviceAdbDisconnected(object sender, DeviceDataEventArgs e)
        {
            Console.WriteLine("Device desconectado.....");

         //   timer.Stop();
        }
        public void Start_configs()
            {
       Thread thread1 = new Thread(() => ButtonCreator());
        //  thread1.SetApartmentState(ApartmentState.STA);
         thread1.Start();
            //   thread1.Join();
            //      ApplyTheme(panel1);

            var con = MainForm.Instance.CurrentDevice.GetConnection();
                if (con != null)
                {


                    var Matriz = new MatrizPacket();
                    con.SendPacket(Matriz);
         RefreshAllButtons(true);
                }
     
          


       

        }
        public void DevicePersistManager_DeviceConnected(object sender, DevicePersistManager.DeviceEventArgs e)
        {
           
            Invoke(new Action(() => {

                if(Program.mode == 1)
                {

                    Start_configs();
                }


               
                shadedPanel1.Show();
                //GenerateFolderList(shadedPanel1);
                shadedPanel2.Hide();
                Refresh();
              

                e.Device.CheckCurrentFolder();
                FixFolders(e.Device);

                if (CurrentDevice == null) {
                    ChangeToDevice(e.Device);
                    if(Program.mode == 0)
                    {
  Start_configs();

                    }
                 
                }
                SendItemsToDevice(CurrentDevice, true);
                GenerateFolderList(shadedPanel1);
            

                  
                
             }));
       
            e.Device.ButtonInteraction += Device_ButtonInteraction;
        }

        public void ChangeToDevice(DeckDevice device)
        {
            
            CurrentDevice = device;
        
            LoadItems(CurrentDevice.CurrentFolder);
           
        }

        //List<Tuple<Guid, int>> ignoreOnce = new List<Tuple<Guid, int>>();
        private void Device_ButtonInteraction(object sender, DeckDevice.ButtonInteractionEventArgs e)
        {
            if (sender is DeckDevice device) {
                /*if (ignoreOnce.Any(c => c.Item1 == device.DeviceGuid && c.Item2 == e.SlotID)) {
                    ignoreOnce.Remove(ignoreOnce.First(c => c.Item1 == device.DeviceGuid && c.Item2 == e.SlotID));
                    return;
                }*/
                var currentItems = device.CurrentFolder.GetDeckItems();
                if (currentItems.Any(c => device.CurrentFolder.GetItemIndex(c) == e.SlotID + 1)) {
                    var item = currentItems.FirstOrDefault(c => device.CurrentFolder.GetItemIndex(c) == e.SlotID + 1);
                    if (item is DynamicDeckItem deckItem && !(item is IDeckFolder)) {
                        if (device.CurrentFolder.GetParent() != null) {
                            if (device.CurrentFolder.GetItemIndex(item) == 1) {
                                if (e.PerformedAction != ButtonInteractPacket.ButtonAction.ButtonUp) return;
                                //Navigate one up!
                                device.CurrentFolder = device.CurrentFolder.GetParent();
                                SendItemsToDevice(CurrentDevice, device.CurrentFolder);
                                //      AddWatermark(deckItem.DeckAction.GetActionName(), ((IDeckItem)imageModernButton1.Origin.Tag).GetDefaultImage().Bitmap, "Arial", 7, 20f, 67f, Brushes.White, item, device.CurrentFolder.GetParent());

                                RefreshAllButtons(false);
                                return;
                            }
                        }
                        if (deckItem.DeckAction != null) {
                            switch (e.PerformedAction) {
                                case ButtonInteractPacket.ButtonAction.ButtonDown:
                                    deckItem.DeckAction.OnButtonDown(device);
                                    break;

                                case ButtonInteractPacket.ButtonAction.ButtonUp:
                                    deckItem.DeckAction.OnButtonUp(device);
                                    break;
                            }
                        }
                    } else if (item is DynamicDeckFolder deckFolder && e.PerformedAction == ButtonInteractPacket.ButtonAction.ButtonUp) {
                        device.CurrentFolder = deckFolder;
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
            if (destroyCurrent) device.CurrentFolder = null;
            device.CheckCurrentFolder();
            SendItemsToDevice(device, device.CurrentFolder);
        }


        private static void SendItemsToDevice(DeckDevice device, IDeckFolder folder)
        {



            var con = device.GetConnection();
            if (con != null) {
              //  if (Globals.status == false) return;
                var packet = new SlotImageChangeChunkPacket();
                var packet_label = new SlotLabelButtonChangeChunkPacket();
                List<IDeckItem> items = folder.GetDeckItems();
               // int calc = ApplicationSettingsManager.Settings.linha * ApplicationSettingsManager.Settings.coluna;
                List<int> addedItems = new List<int>();
                bool isFolder = false;
                for (int i = 0; i < Globals.calc; i++)
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
                    //var teste = new DeckImage(image_receivd.ToBitmap());

                    var image = item.GetItemImage() ?? item.GetDefaultImage() ?? (new DeckImage(isFolder ? Resources.img_folder : Resources.img_item_default));
                    // var seri = image.BitmapSerialized;
                    ///     ImageModernButton control = Controls.Find("modernButton" + folder.GetItemIndex(item), true).FirstOrDefault() as ImageModernButton;
                    //  IDeckFolder folder_t = CurrentDevice?.CurrentFolder;
                    //        var seri = teste.BitmapSerialized;
                   
                    
                        //  packet.AddToQueue(folder.GetItemIndex(item), new DeckImage(ReceiveWaterMark(DI.DeckAction.GetActionName(),image.Bitmap)));
                        packet_label.AddToQueue(folder.GetItemIndex(item), item?.DeckName, "", item.DeckSize, item.DeckPosition, item?.DeckColor);
                       
                    packet.AddToQueue(folder.GetItemIndex(item), image);
                //    packet.AddToQueue(folder.GetItemIndex(item), image);
              
             
                    //    packet.AddToQueue(folder.GetItemIndex(item), image);
                    //  con.SendPacket(packet);


                    //   teste.modernButton1.NormalImage;

                    //     packet.AddToQueue(folder.GetItemIndex(item), image);
                }

                con.SendPacket( packet);
                con.SendPacket(packet_label);
                //    con.SendPacket(packet_label);
                var clearPacket = new SlotImageClearChunkPacket();
              //  var clearPacket_labels = new SlotLabelButtonClearChunkPacket();
                for (int i = 1; i < Globals.calc + 1; i++) {
                    if (addedItems.Contains(i)) continue;
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
            if(Globals.can_refresh == false)
            {

                return;
            }
            List<IDeckItem> items = folder.GetDeckItems();
            foreach (var item in items) {
                //This is when it loads.
                //It will load from the persisted device.

                bool isFolder = item is IDeckFolder;
                ImageModernButton control = Controls.Find("modernButton" + folder.GetItemIndex(item), true).FirstOrDefault() as ImageModernButton;
                var image = item.GetItemImage() ?? item.GetDefaultImage() ?? (new DeckImage(isFolder ? Resources.img_folder : Resources.img_item_default));
                var seri = image.BitmapSerialized;
                if (item is DynamicDeckItem DI && DI.DeckAction != null)
                {


                    //     item.GetItemImage().BitmapSerialized = converterDemo(AddWatermark(DI.DeckAction.GetActionName(), item?.GetItemImage().Bitmap, "Arial", 7, 20f, 67f, Brushes.White,item));
                    //  item.GetItemImage().BitmapSerialized = converterDemo(item?.GetItemImage().Bitmap);
                    //     var ser = item.GetItemImage().BitmapSerialized;
                    //    item.BitmapSerialized = item?.GetItemImage().Bitmap;
                 //   AddWatermark(DI.DeckAction.GetActionName(), image.Bitmap, "Arial", 7, 20f, 67f, Brushes.White, item, folder);
                    //item.GetItemImage().BitmapSerialized = converterDemo( AddWatermark(DI.DeckAction.GetActionName(), item?.GetItemImage().Bitmap, "Arial", 7, 20f, 67f, Brushes.White)); 
                    //   Write_name_Image("testeee", item?.GetItemImage().Bitmap, 10f, 10f, "Arial", 10);

                }
                else if (item is DynamicDeckFolder FO)
                {
                    //AddWatermark(FO.folder_name, image.Bitmap, "Arial", 7, 20f, 67f, Brushes.White, item, folder);



                }
                else
                {
                    //
                    control.NormalImage = image.Bitmap;
                }






                control.Refresh();
                //control.Refresh();
                control.Tag = item;
            }
        }

        private void FixFolders(DeckDevice device)
        {
            FixFolders(device.MainFolder);
        }

        private static DeckImage defaultDeckImage = new DeckImage(Resources.img_folder_up);
        private static DynamicDeckItem folderUpItem = new DynamicDeckItem() { DeckImage = defaultDeckImage };

        private void FixFolders(IDeckFolder folder, bool ignoreFirst = true, IDeckFolder trueParent = null)
        {
            if (!ignoreFirst) {
                if (trueParent != null)
                    folder.SetParent(trueParent);
                if (folder.GetParent() != null) {
                    folder.Add(1, folderUpItem);
                }
            }

            folder.GetSubFolders().All(c => {
                FixFolders(c);
                c.SetParent(folder);
                if (c.GetParent() != null) {
                    c.Add(1, folderUpItem);
                }

                return true;
            });
        }

        private void UnfixFolders(IDeckFolder folder)
        {
            folder.GetSubFolders().All(c => {
                UnfixFolders(c);
                c.SetParent(folder);
                if (c.GetParent() != null) {
                    c.Remove(1);
                }

                return true;
            });
        }
      
            private void DevicePersistManager_DeviceDisconnected(object sender, DevicePersistManager.DeviceEventArgs e)
        {

            if (e.Device.DeviceGuid == CurrentDevice.DeviceGuid) {

    
                if (!DevicePersistManager.IsVirtualDeviceConnected) CurrentDevice = null;

            
                //Try to find a new device to be the current one.
                if (DevicePersistManager.PersistedDevices.Any(d => DevicePersistManager.IsDeviceConnected(d.DeviceGuid))) {
                    CurrentDevice = DevicePersistManager.PersistedDevices.First(d => DevicePersistManager.IsDeviceConnected(d.DeviceGuid));
                    if (IsHandleCreated && !Disposing)
                        Invoke(new Action(() => {
                            shadedPanel1.Show();
                            Buttons_Unfocus(sender, EventArgs.Empty);

                            e.Device.CheckCurrentFolder();
                            FixFolders(e.Device);

                            RefreshAllButtons(false);
                        }));

                } 
            }
            if (IsHandleCreated && !Disposing)
                Invoke(new Action(() => {
                    shadedPanel2.Hide();
                    shadedPanel1.Hide();
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

        bool canskip = false;
        int root = 0;
        private void NextFolder()
        {


        }

        private void AddFolderInPanelList(int value)
        {
            Control parent = shadedPanel1;

            Padding categoryPadding = new Padding(5, 0, 0, 0);
            Font categoryFont = new Font(parent.Font.FontFamily, 13, FontStyle.Bold);
            Padding itemPadding = new Padding(25, 0, 0, 0);
            Font itemFont = new Font(parent.Font.FontFamily, 12);



            ApplicationColorScheme appTheme = ColorSchemeCentral.FromAppTheme(ApplicationSettingsManager.Settings.Theme);



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

            folder_root.Click += (s, ee) => {





                RefreshAllButtons(true);

            };


            foreach (var item in folder_form)
            {

                if (item.Folder_id == value)
                    if (item.Is_father == true)
                    {
                        folder_root.Text = item.Name;
                        parent.Controls.Add(folder_root);

                        folder_root.Click += (s, ee) => {



                            CurrentDevice.CurrentFolder = item.Parent;

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

                        folder_children.Click += (s, ee) => {



                            CurrentDevice.CurrentFolder = item.Parent;

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



                    folder_form.Add(new folders(PP.folder_name, PP, false, root));
                    // AddFolderInPanelList(shadedPanel1, PP.folder_name, PP, root, false, root);
                    //   Debug.WriteLine(PP.folder_name + "CC:" + PP.GetSubFolders().Count);             

                    if (abacate.GetSubFolders().Count == 0)
                    {
                        if (CurrentDevice.MainFolder.GetSubFolders().Count - 1 > root)
                        {

                            if (CurrentDevice.MainFolder.GetSubFolders()[root] is DynamicDeckFolder pastapai)
                            {
                                folder_form.Add(new folders(pastapai.folder_name, pastapai, true, root));
                            }
                            AddFolderInPanelList(root);
                            root++;
                        }
                        else
                        {
                            if (CurrentDevice.MainFolder.GetSubFolders()[root] is DynamicDeckFolder pastapai)
                            {
                                folder_form.Add(new folders(pastapai.folder_name, pastapai, true, root));
                            }
                            Debug.WriteLine("ROOT: " + root);
                            AddFolderInPanelList(root);
                            root = 0;


                            goto fim;
                        }
                        //  Debug.WriteLine("NUMINDEX:" + root);

                        GetAllFolders(CurrentDevice.MainFolder.GetSubFolders()[root]);

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

            List<Control> toFolders = new List<Control>();
            List<Control> toSubFolders = new List<Control>();
            ApplicationColorScheme appTheme = ColorSchemeCentral.FromAppTheme(ApplicationSettingsManager.Settings.Theme);



            try
            {

                Label header_folder = new Label()
                {
                    Padding = categoryPadding,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = categoryFont,
                    BackColor = appTheme.SecondaryColor,
                    ForeColor = appTheme.ForegroundColor,
                    Dock = DockStyle.Top,
                    Text = "Pastas",
                    Tag = "header",
                    Height = TextRenderer.MeasureText("Pastas", categoryFont).Height
                };



                Label folder_root = new Label()
                {
                    Padding = itemPadding,
                    TextAlign = ContentAlignment.MiddleLeft,

                    Font = itemFont,

                    Dock = DockStyle.Top,
                    Text = "MAIN ROOT",
                    Tag = "Main_Folder",
                    Height = TextRenderer.MeasureText("MAIN ROOT", itemFont).Height,

                };
                folder_root.Click += (s, ee) => {

                    CurrentDevice.CurrentFolder = CurrentDevice.MainFolder;

                    //  Debug.WriteLine("Pasta selecionada:" + folder_name.Text);
                    RefreshAllButtons(true);

                };


                IDeckFolder folder = CurrentDevice.MainFolder;


                if (folder != null)
                {

                    //   GetAllFolders(folder.GetSubFolders()[root]);

                }



                foreach (var item in toFolders)
                {

                    //parent.Controls.Add(item);


                }




                //         parent.Controls.Add(folder_root);
                //         parent.Controls.Add(header_folder);
            }

            catch (Exception e)
            {

                Debug.WriteLine("BUG:" + e.ToString());
            }

        }

        private void GenerateSidebar(Control parent)
        {
            Padding categoryPadding = new Padding(5, 0, 0, 0);
            Font categoryFont = new Font(parent.Font.FontFamily, 13, FontStyle.Bold);
            Padding itemPadding = new Padding(25, 0, 0, 0);
            Font itemFont = new Font(parent.Font.FontFamily, 12);

            var items = ReflectiveEnumerator.GetEnumerableOfType<AbstractDeckAction>();

            List<Control> toAdd = new List<Control>();




            foreach (DeckActionCategory enumItem in Enum.GetValues(typeof(DeckActionCategory))) {
                var enumItems = items.Where(i => i.GetActionCategory() == enumItem && i.IsPlugin() == false);
                if (enumItems.Any()) {
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


                    foreach (var i2 in enumItems) {
                        Label item = new Label()
                        {
                            Padding = itemPadding,
                            TextAlign = ContentAlignment.MiddleLeft,
                            Font = itemFont,
                            Dock = DockStyle.Top,
                            Text = i2.GetActionName(),
                            Height = TextRenderer.MeasureText(i2.GetActionName(), itemFont).Height,
                            Tag = i2,

                        };
                        //    Debug.WriteLine("TAG VINDO: " + i2);
                        item.MouseDown += (s, ee) => {
                            if (item.Tag is AbstractDeckAction act)
                                item.DoDragDrop(new DeckActionHelper(act), DragDropEffects.Copy);
                        };
                        toAdd.Add(item);
                    }
                }
            }
            toAdd.AsEnumerable().Reverse().All(m => {
                parent.Controls.Add(m);
                return true;
            });
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

            if (dlg.ShowDialog() == DialogResult.OK) {
                //We have an image file.
                //Load as bitmap and replace DeckImage
                try {
                    Bitmap bmp = new Bitmap(dlg.FileName);
                    if (DeckImage.ImageToByte(bmp).Length > CLIENT_ARRAY_LENGHT) {
                        MessageBox.Show(this, "The selected image is too big to be sent to the device. Please choose another", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ImageModernButton1_MouseClick(sender, new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0));
                        return;
                    }


                    imageModernButton1.Image = bmp;
                } catch (Exception) {
                }
            }
        }

        private Stopwatch lastClick = new Stopwatch();

        private void ItemButton_MouseClick(object sender, EventArgs e)
        {
            lastClick.Stop();
            bool isDoubleClick = lastClick.ElapsedMilliseconds != 0 && lastClick.ElapsedMilliseconds <= SystemInformation.DoubleClickTime;
            if (sender is ImageModernButton mb) {
                if (mb.Tag != null && mb.Tag is IDeckItem item) {
                    if (item is IDeckFolder folder) {
                        if (!isDoubleClick) {
                            FocusItem(mb, item);
                            goto end;
                        }
                        //Navigate to the folder
                        CurrentDevice.CurrentFolder = folder;
                        RefreshAllButtons();
                        goto end;
                    }
                    if (CurrentDevice.CurrentFolder.GetParent() != null) {
                        //Not on the main folder
                        if (mb.CurrentSlot == 1) {
                            CurrentDevice.CurrentFolder = CurrentDevice.CurrentFolder.GetParent();
                            RefreshAllButtons();
                            lastClick.Reset();
                            return;
                        }
                    }

                    //Show button panel with settable properties
                    FocusItem(mb, item);

                    lastClick.Reset();
                } else {
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





            if (sender is ImageModernButton senderB) {
                if (e.Button == MouseButtons.Left && DevicePersistManager.IsVirtualDeviceConnected && ModifierKeys == Keys.Shift) {
                    if (senderB.Tag != null && senderB.Tag is DynamicDeckItem item) {
                        item.DeckAction?.OnButtonUp(CurrentDevice);
                    }
                    return;
                }


                if (!senderB.DisplayRectangle.Contains(e.Location)) return;
                if (e.Button == MouseButtons.Right && CurrentDevice.CurrentFolder.GetDeckItems().Any(c => CurrentDevice.CurrentFolder.GetItemIndex(c) == senderB.CurrentSlot))
                {


                    popupMenu.Items.Add("Remove item").Click += (s, ee) =>
                    {
                        if (senderB != null)
                        {
                            if (senderB.Image != Resources.img_folder && senderB.Image != Resources.img_item_default)
                            {
                                senderB.Image.Dispose();
                            }
                            senderB.Tag = null;
                            senderB.Image = null;
                            Buttons_Unfocus(sender, e);
                            CurrentDevice.CurrentFolder.Remove(senderB.CurrentSlot);
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
       
         
        

        private void Write_name_Image( string text, Bitmap imageFilePath, float x, float y, string font, int size)
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
            flowLayoutPanel1.Controls.OfType<Control>().All(c => {
                c.Dispose();
                return true;
            });

            flowLayoutPanel1.Controls.Clear();
            if (item is IDeckItem dI) {
               
            
         if(dI is DynamicDeckFolder TA)
                {
                    if(TA.GetSubFolders().Count > 0)
                    {

                    }

                }

              
                ModernButton myButton = new ModernButton();
                ModernButton myColor = new ModernButton();
                Label myTextNameInformation = new Label();
                Label sizeLabelInfo = new Label();
                Label positionLabelInfo = new Label();
                TextBox sizeLabelTextBox = new TextBox();
                ComboBox PositionComboBox= new ComboBox();
                TextBox myNameText = new TextBox();
                TextBox myColorText = new TextBox();
                myColor.Size = new Size(70, 30);
                myColor.Text = "Selecionar Cor";
               
 FlowLayoutPanel painel_name = new FlowLayoutPanel();
                FlowLayoutPanel painel_color = new FlowLayoutPanel();
                FlowLayoutPanel painel_tamanho = new FlowLayoutPanel();
                FlowLayoutPanel painel_position = new FlowLayoutPanel();
                myColor.Dock = DockStyle.None;
                myColorText.Dock = DockStyle.None;
                sizeLabelTextBox.Dock = DockStyle.None;
                sizeLabelInfo.Dock = DockStyle.None;
                positionLabelInfo.Dock = DockStyle.None;
                PositionComboBox.Dock = DockStyle.None;
         
                myNameText.Text = dI.DeckName;
                myColorText.Text = dI.DeckColor;

   myColorText.Text = dI.DeckColor;
            
               // PositionComboBox.SelectedValue = (int)dI.DeckPosition;



                sizeLabelTextBox.Text = dI.DeckSize.ToString();
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










                






                painel_position.Size = new Size(190, 60);

                painel_color.Size = new Size(190, 30);

                painel_name.Size = new Size(190, 50);
                painel_tamanho.Size = new Size(190, 50);
                painel_tamanho.WrapContents = true;
                painel_name.WrapContents = true;
                //painel 1
                painel_color.WrapContents = true;
                painel_position.WrapContents = true;
                painel_position.Controls.Add(positionLabelInfo);
              
                painel_position.Controls.Add(PositionComboBox);
 
            
                painel_color.Controls.Add(myColor);
                painel_color.Controls.Add(myColorText);
                
                painel_name.Controls.Add(myTextNameInformation);
                painel_name.Controls.Add(myNameText);
                painel_tamanho.Controls.Add(sizeLabelInfo);
                painel_tamanho.Controls.Add(sizeLabelTextBox);

        
   
             
           
                flowLayoutPanel1.Controls.Add(painel_name);
                flowLayoutPanel1.Controls.Add(painel_color);
                flowLayoutPanel1.Controls.Add(painel_tamanho);
                flowLayoutPanel1.Controls.Add(painel_position);

                flowLayoutPanel1.Controls.Add(myButton);
                myButton.Text = "Salvar";

                myTextNameInformation.Text = "Nome:";

                sizeLabelInfo.Text = "Tamanho:";
                
 setEnumValues(PositionComboBox, typeof(Position));
                myButton.Click += (s, e) =>
                {
                    dI.DeckName = myNameText.Text;
                    dI.DeckColor = myColorText.Text;
                    dI.DeckSize = Convert.ToInt32(sizeLabelTextBox.Text);
             
                    dI.DeckPosition = (int)PositionComboBox.SelectedValue;

                };
               

                PositionComboBox.SelectedValue = dI.DeckPosition;
                if (dI is DynamicDeckItem TT)
                {
                    LoadProperties(TT, flowLayoutPanel1);

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

        private void LoadProperties(DynamicDeckItem item, FlowLayoutPanel panel)
        {


            var props = item.DeckAction.GetType().GetProperties().Where(
                prop => Attribute.IsDefined(prop, typeof(ActionPropertyIncludeAttribute)));
            foreach (var prop in props) {
                bool shouldUpdateIcon = Attribute.IsDefined(prop, typeof(ActionPropertyUpdateImageOnChangedAttribute));
                MethodInfo helperMethod = item.DeckAction.GetType().GetMethod(prop.Name + "Helper");
                if (helperMethod != null) {
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
                } else {
                    if (prop.PropertyType.IsSubclassOf(typeof(Enum))) {
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

                        cBox.SelectedIndexChanged += (s, e) => {
                            try {
                                if (cBox.Text == string.Empty) return;
                                prop.SetValue(item.DeckAction, EnumUtils.FromDescription(prop.PropertyType, cBox.Text));
                                UpdateIcon(shouldUpdateIcon);
                            } catch (Exception) {
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
                    txt.TextChanged += (sender, e) => {
                        try {
                            if (txt.Text == string.Empty) return;
                            //After loosing focus, convert type to thingy.
                            prop.SetValue(item.DeckAction, TypeDescriptor.GetConverter(prop.PropertyType).ConvertFrom(txt.Text));
                            UpdateIcon(shouldUpdateIcon);
                        } catch (Exception) {
                            //Ignore all errors
                        }
                    };
                    txt.Width = panel.DisplayRectangle.Width - SystemInformation.VerticalScrollBarWidth * 2;
                    panel.Controls.Add(txt);
                }
            }

            ModifyColorScheme(flowLayoutPanel1.Controls.OfType<Control>());
        }
        public static  void button_creator(string name,string name_space, string script)
        {
            //FolderAddAction testando = new FolderAddAction();
            FolderAddAction.name = name;
            FolderAddAction.script = script;
           // testando.script = script;



            FolderAddAction.name_space = name_space;
            Padding categoryPadding = new Padding(5, 0, 0, 0);
            Font categoryFont = new Font(MainForm.instance.ShadedPanel1.Font.FontFamily, 13, FontStyle.Bold);
            Padding itemPadding = new Padding(25, 0, 0, 0);
            Font itemFont = new Font(MainForm.instance.ShadedPanel1.Font.FontFamily, 12);
            IList<FolderAddAction> list = new List<FolderAddAction>();
            // ButtonDeck.Forms.MainForm.testando(value);
            MainForm.instance.ShadedPanel1.Invoke((MethodInvoker)delegate {
            //Globals.launcher_principal.ShadedPanel1.Controls.Clear();
            Label teste = new Label()
            {
                Padding = categoryPadding,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = categoryFont,
                Dock = DockStyle.Top,
                Text = name,
                Tag = "header",

            };


                //  FolderAddAction testando = new FolderAddAction();
                //  FolderAddAction.name = name_button;

                //     FolderAddAction.LuaExecuteButtonDown = buttondowm;
                //  FolderAddAction.LuaExecuteButtonDown = //buttondowm;
                //     FolderAddAction.DeckActionCategory_string = category;


                //    Debug.WriteLine("RRR" + testando.GetActionName());

                // Globals.launcher_principal.ShadedPanel1.Controls.Add(teste);
                MainForm.instance.ShadedPanel1.Refresh();

                var items = ReflectiveEnumerator.GetEnumerableOfType<AbstractDeckAction>();

                List<Control> toAdd = new List<Control>();




                foreach (DeckActionCategory enumItem in Enum.GetValues(typeof(DeckActionCategory)))
                {
                    var enumItems = items.Where(i => i.GetActionCategory() == enumItem && i.IsPlugin() == true);
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
                                Tag = i2,

                            };
                            //    Debug.WriteLine("TAG VINDO: " + i2);
                            item.MouseDown += (s, ee) => {
                                if (item.Tag is AbstractDeckAction act)
                                    item.DoDragDrop(new DeckActionHelper(act), DragDropEffects.Copy);
                            };
                            toAdd.Add(item);
                        }
                    }
                }
                toAdd.AsEnumerable().Reverse().All(m => {
                    MainForm.instance.ShadedPanel1.Controls.Add(m);
                    return true;
                });
                Debug.WriteLine("GRANDO SIDEBAR " + name);
            });
            //   return "";
        }
        private void UpdateIcon(bool shouldUpdateIcon)
        {
           // IDeckFolder folder = CurrentDevice?.CurrentFolder;
            if (shouldUpdateIcon) {

               // IDeckItem item = null;
                // item = folder.GetDeckItems()[i];

               // AddWatermark("RWER", ((IDeckItem)imageModernButton1.Origin.Tag).GetDefaultImage().Bitmap, "Arial", 7, 20f, 67f, Brushes.White, item, folder);
                
              imageModernButton1.Image = ((IDeckItem)imageModernButton1.Origin.Tag).GetDefaultImage()?.Bitmap ?? Resources.img_item_default;
             //   var teste = new MagickImage((Bitmap)imageModernButton1.Image);
                //   image.Annotate("caption:This is gergerga test.", Gravity.South); // caption:"This is a test."
                // write the image to the appropriate directory
                // image.Write(@"D:\testimage.jpg");
               // SendItemsToDevice(CurrentDevice, CurrentDevice.CurrentFolder,teste);

                imageModernButton1.Refresh();
            }
        }
        public void UpdatePluginImg()
        {
            IDeckFolder folder = CurrentDevice?.CurrentFolder;
           
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
            if (Attribute.IsDefined(prop, typeof(ActionPropertyDescriptionAttribute))) {
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
            if (sender is ImageModernButton senderB) {
                if (e.Button == MouseButtons.Left && DevicePersistManager.IsVirtualDeviceConnected && ModifierKeys == Keys.Shift) {
                    if (senderB.Tag != null && senderB.Tag is DynamicDeckItem item) {
                        item.DeckAction?.OnButtonDown(CurrentDevice);
                    }
                    return;
                }
            }


            mouseDown = e.Button == MouseButtons.Left;
            mouseDownLoc = Cursor.Position;
            if (e.Button == MouseButtons.Right )
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
                                newFolder.ParentFolder = CurrentDevice.CurrentFolder;
                                newFolder.Add(1, folderUpItem);

                                CurrentDevice.CurrentFolder.Add(mb.CurrentSlot, newFolder);
                            //CurrentDevice.CurrentFolder = newFolder;
                            RefreshAllButtons(true);

                            }
                        };


                        menu.Items.Add("Atualizar").Click += (s, ee) =>
                        {

                            if (sender is ImageModernButton mb)
                            {
                             //CurrentDevice.CurrentFolder = newFolder;
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
            IDeckFolder folder = CurrentDevice?.CurrentFolder;

            var finalPoint = new Point(distanceX, distanceY);
            bool didMove = SystemInformation.DragSize.Width * 2 > finalPoint.X && SystemInformation.DragSize.Height * 2 > finalPoint.Y;
            if (didMove && !finalPoint.IsEmpty) {
                mouseDown = false;
                if (sender is ImageModernButton mb) {
                    if (mb.Tag != null && mb.Tag is IDeckItem act) {
                        bool isDoubleClick = lastClick.ElapsedMilliseconds != 0 && lastClick.ElapsedMilliseconds <= SystemInformation.DoubleClickTime;
                        if (isDoubleClick) return;
                        if ((CurrentDevice.CurrentFolder.GetParent() != null && (mb.CurrentSlot == 1))) return;
                     mb.DoDragDrop(new DeckItemMoveHelper(act, CurrentDevice.CurrentFolder, mb.CurrentSlot) { CopyOld = ModifierKeys.HasFlag(Keys.Control) }, ModifierKeys.HasFlag(Keys.Control) ? DragDropEffects.Copy : DragDropEffects.Move);
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
    }
    #endregion
}

#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body