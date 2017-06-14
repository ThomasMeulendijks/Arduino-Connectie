using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Arduino_Connectie
{
    public partial class ComPortSelector : Form
    {
        public string CommLock { get; private set; }
        public string CommTv { get; private set; }
        public ComPortSelector()
        {
            InitializeComponent();
            string[] comportStrings = SerialPort.GetPortNames();
            foreach (string commport in comportStrings)
            {
                cbbLock.Items.Add(commport);
                cbbTv.Items.Add(commport);
            }

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // checks if the cbb are not the same and have a value then saves them
            if (cbbLock.SelectedText != cbbTv.SelectedText && cbbTv.SelectedIndex > -1 && cbbLock.SelectedIndex > -1)
            {
                CommLock = cbbLock.SelectedText;
                CommTv = cbbTv.SelectedText;
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
