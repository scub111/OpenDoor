using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RapidNetwork;
using System.Media;

namespace OpenDoor
{
    public partial class frmMain : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8
        }

        /// <summary>
        /// Асинхронный клиент.
        /// </summary>
        AsyncClient Client { get; set; }

        /// <summary>
        /// ID глобальной горячей клавиши.
        /// </summary>
        int OpenHotKeyID { get; set; } 

        /// <summary>
        /// Начальное время.
        /// </summary>
        DateTime t0 { get; set; }

        /// <summary>
        /// Дверь открывается.
        /// </summary>
        int OpeningCount { get; set; }

        private static int GetKeyModifier()
        {
            int result = 0;
            if (Global.Default.varXml.HotKey.Alt)
                result += 1;
            if (Global.Default.varXml.HotKey.Cntr)
                result += 2;
            if (Global.Default.varXml.HotKey.Shift)
                result += 4;
            if (Global.Default.varXml.HotKey.WinKey)
                result += 8;
            return result;
        }

        public frmMain()
        {
            Global.Default.Init();
            InitializeComponent();

            OpenHotKeyID = 0;
            OpeningCount = 0;

            lblServerConnetion.Text = string.Format("{0}:{1}", Global.Default.varXml.Connection.ServerIP, Global.Default.varXml.Connection.Port);

            //RegisterHotKey(this.Handle, OpenHotKeyID, (int)(KeyModifier.Control), Keys.F10.GetHashCode());    

            try
            {
                Keys key = (Keys)Enum.Parse(typeof(Keys), Global.Default.varXml.HotKey.Key);
                if (key != Keys.None)
                {
                    RegisterHotKey(Handle, OpenHotKeyID, GetKeyModifier(), key.GetHashCode());       // Register this combination of keys as global hotkey. 
                }
                else
                    throw new Exception();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось зарегестрировать горячую клавишу.");
            }
            Text = string.Format("Входная дверь {0}", Global.Default.Version);
            niCommon.Text = Text;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 0x0312)
            {
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                int id = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.

                if (id == OpenHotKeyID)
                {
                    btnOpenDoor_Click(this, EventArgs.Empty);
                }
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Client = new AsyncClient();
            Client.Connect(Global.Default.varXml.Connection.ServerIP, Global.Default.varXml.Connection.Port);
            Client.Connected += Client_Connected;
            Client.Disconnected += Client_Disconnected;
            Client.MessageReceived += Client_MessageReceived;
        }

        private void Client_Connected(object sender, EventArgs eventArgs)
        {
            HandleUpdateInterface();
        }

        private void Client_Disconnected(object sender, EventArgs eventArgs)
        {
            HandleUpdateInterface();
        }

        private void btnOpenDoor_Click(object sender, EventArgs e)
        {
            Client.SendAsync("OpenDoor");
        }

        private void UpdateInterface()
        {
            if (Client.IsConnected)
            {
                lblStatus.Text = "Подключен";
                btnOpenDoor.Enabled = true;
                btnPing.Enabled = true;
                Icon = Properties.Resources.DoorGood;
            }
            else
            {
                lblStatus.Text = "Не подключен";
                btnOpenDoor.Enabled = false;
                btnPing.Enabled = false;
                Icon = Properties.Resources.DoorBad;
            }
            niCommon.Icon = Icon;
        }

        private void tmrCommon_Tick(object sender, EventArgs e)
        {
            if (Client.IsConnected)
            {
            }
            else
            {
                lblStatus.Text = "Не подключен";
                Client.Disconnect();
                Client.Connect(Global.Default.varXml.Connection.ServerIP, Global.Default.varXml.Connection.Port);
            }
            btnPing_Click(this, EventArgs.Empty);

            UpdateInterface();
        }

        private void btnPing_Click(object sender, EventArgs e)
        {
            if (Client.IsConnected)
            {
                t0 = DateTime.Now;
                Client.SendAsync("Ping");
            }
        }

        internal delegate void AppendTextDelegate(string message);

        private void HandlePing(string message)
        {
            if (InvokeRequired)
                BeginInvoke(new AppendTextDelegate(HandlePing), new object[] { message });
            else
                btnPing.Text = message;
        }

        internal delegate void SimpleDelegate();

        private void HandleUpdateInterface()
        {
            if (InvokeRequired)
                BeginInvoke(new SimpleDelegate(HandleUpdateInterface));
            else
                UpdateInterface();
        }

        private void HandleOpened()
        {
            if (InvokeRequired)
                BeginInvoke(new SimpleDelegate(HandleOpened));
            else
            {
                OpeningCount = 0;
                niCommon.Icon = Properties.Resources.DoorOpened;
                tmrOpened.Enabled = true;
            }
        }

        private void Client_MessageReceived(object sender, ReceivedEventArgs eventArgs)
        {
            if (eventArgs.Message == "OpenedByThis")
            {
                SystemSounds.Beep.Play();
            }
            else if (eventArgs.Message == "Opened")
            {
                HandleOpened();
            }
            else if (eventArgs.Message == "Pinged")
            {
                TimeSpan diff = DateTime.Now - t0;
                HandlePing(string.Format("{0:0.00} мс", diff.TotalMilliseconds));
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnregisterHotKey(Handle, OpenHotKeyID);
        }

        private void niCommon_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                btnOpenDoor_Click(this, EventArgs.Empty);
            }
            else if (e.Button == MouseButtons.Right)
            {
                Show();
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
            }
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                Hide();
        }

        /// <summary>
        /// Тики перед сворачиванием.
        /// </summary>
        int MinimizeTicks = 0;
        private void tmrMinimize_Tick(object sender, EventArgs e)
        {
            MinimizeTicks++; 
            if (MinimizeTicks >= 0)
            {
                WindowState = FormWindowState.Minimized;
                Hide();
                //ShowInTaskbar = false;
                tmrMinimize.Enabled = false;
            }
        }


        private void tmrOpened_Tick(object sender, EventArgs e)
        {
            if (OpeningCount > 25)
            {
                UpdateInterface();
                tmrOpened.Enabled = false;
            }
            OpeningCount++;
        }
    }
}

