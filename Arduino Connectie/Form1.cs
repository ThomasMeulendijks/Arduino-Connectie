using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LivingColorRemoteControl;

namespace Arduino_Connectie
{
    public enum IRValue
    {
        on,
        off,
        play,
        pause
    }

    public partial class Form1 : Form
    {
        // Create a SerialMessanger instance for each arduino 
        private SerialMessenger serialMessengerlock;
        private SerialMessenger serialMessengerTv;
        private Timer readMessageTimer;

       

        /// <summary>
        /// Wets up the Timer and SerialMessengers,
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            SerialMessangerCreationDialog();
            readMessageTimer = new Timer();
            readMessageTimer.Interval = 10;
            readMessageTimer.Tick += new EventHandler(ReadMessageTimer_Tick);
            try
            {
                serialMessengerlock.Connect();
                serialMessengerTv.Connect();
                readMessageTimer.Enabled = true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        /// <summary>
        /// Is used to select the comm ports and create the serial messanger instances
        /// </summary>
        private void SerialMessangerCreationDialog()
        {
            ComPortSelector newDialog = new ComPortSelector();
            DialogResult result = newDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                MessageBuilder messageBuilder = new MessageBuilder('#', '%');

                serialMessengerlock = new SerialMessenger(newDialog.CommLock, 115200, messageBuilder);
                serialMessengerTv = new SerialMessenger(newDialog.CommTv, 115200, messageBuilder);
            }
            else
            {
                //throw new Exception("DialogResult not OK");
                MessageBox.Show("DialogResult not OK");
                SerialMessangerCreationDialog();
            }
        }

        /// <summary>
        /// Every tick check read if their are new messages if so process them
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadMessageTimer_Tick(object sender, EventArgs e)
        {
            string[] messagesLock = serialMessengerlock.ReadMessages();
            if (messagesLock != null)
            {
                foreach (string message in messagesLock)
                {
                    processReceivedMessage(message);
                }
            }
            string[] messagesTv = serialMessengerTv.ReadMessages();
            if (messagesTv != null)
            {
                foreach (string message in messagesTv)
                {
                    processReceivedMessage(message);
                }
            }

        }

        /// <summary>
        /// Processes the message
        /// </summary>
        /// <param name="message"></param>
        


        private void processReceivedMessage(string message)
        {
            MessageBox.Show(message);
            if (message.StartsWith("LOCKNFC:"))
            {
                // the string after LOCKNFC: is allways a nfc tag
                string NFCTag = getValue(message);
                if (CheckLockAccess(NFCTag))
                {
                    serialMessengerlock.SendMessage("OpenDoor");
                }
            }
            // the string after TVHex: is allways a hex command that will be converted into a enum
            else if (message.StartsWith("TVHex:"))
            {
                IRValue irvalue;
                // Get value will return a hex comand
                switch (getValue(message))
                {
                    case @"FF30CF\r":
                        irvalue = IRValue.on;
                        MessageBox.Show(irvalue.ToString());
                        break;
                    case @"FF18E7\r":
                        irvalue = IRValue.off;
                        MessageBox.Show(irvalue.ToString());
                        break;
                    case @"FF7A85\r":
                        irvalue = IRValue.play;
                        MessageBox.Show(irvalue.ToString());
                        break;
                    case @"FF10EF\r":
                        irvalue = IRValue.pause;
                        MessageBox.Show(irvalue.ToString());
                        break;
                }

            }
        }



    private string getValue(string message)
        {
            int colonIndex = message.IndexOf(':');
            if (colonIndex != -1)
            {
                string value = message.Substring(colonIndex + 1);
                return value;
 
            }
            throw new ArgumentException("message contains no value parameter");
        }
        private int getParamValue(string message)
        {
            int colonIndex = message.IndexOf(':');
            if (colonIndex != -1)
            {
                string param = message.Substring(colonIndex + 1);
                int value;
                bool done = int.TryParse(param, out value);
                if (done)
                {
                    return value;
                }
            }
            throw new ArgumentException("message contains no value parameter");
        }
        /// <summary>
        /// Check if the NFC id has access
        /// </summary>
        /// <param name="nfc">User's NFC id used to open the door</param>
        /// <returns></returns>
        private bool CheckLockAccess(string nfc)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString))
            {
                conn.Open();

                string commandString = string.Format("select u.id, l.enabled " +
                                                     "from [User] u, [LockSettings] l " +
                                                     "where (u.id = l.user_id) and nfc = '{0}';", nfc);

                using (SqlCommand command = new SqlCommand(commandString, conn))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (Convert.ToBoolean(reader["enabled"]))
                        {
                            //Access field value was 1
                            return true;
                        }
                        else
                        {
                            // Access field value was 0
                            MessageBox.Show("No Access");
                            return false;
                        }
                    }
                    // Query didn't return rows
                    MessageBox.Show("LockSettings entry does not exist");
                    return false;
                }
            }
        }
    }
}
