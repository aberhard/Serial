using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Serial
{
    public partial class Form1 : Form
    {

        private DateTime dataTime;
        private string in_data;


        public Form1()
        {
            InitializeComponent();
            GetAvailablePorts();
        }
        
       void GetAvailablePorts()
        {
            String[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text==""||comboBox2.Text=="")
                {
                    textBox2.Text = "Please select port settings";
                }
                else
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                    serialPort1.DataReceived += SerialPort1_DataReceived;
                    serialPort1.Open();
                    progressBar1.Value = 100;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    textBox1.Enabled = true;
                    button3.Enabled = false;
                    button4.Enabled = true;

                }
            }
            catch (UnauthorizedAccessException)
            {
                textBox2.Text = "Unauthorized Access";
                
            }
        }

        public void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                this.Invoke(new EventHandler(displaydata_event));
                in_data = serialPort1.ReadLine();
            }
            catch (Exception ex4)
            {

                MessageBox.Show("Problem" + ex4.Message);
            }
            
            
        }

        public void displaydata_event(object sender, EventArgs e)
        {
            dataTime = DateTime.Now;

            string zeit = dataTime.ToString("MM/dd/yy HH:mm:ss");
            //string hour = dataTime.Hour.ToString("HH");
            //string minutes = dataTime.Minute.ToString("mm");
            //string seconds = dataTime.Second.ToString("ss");
            //string time = hour + ":" + minutes + ":" + seconds; 
            textBox2.AppendText(zeit + " " +  in_data + "\n"); 
            textBox2.ScrollToCaret();
        }

        private void button4_Click(object sender, EventArgs e) 
        {
            serialPort1.Close();
            progressBar1.Value = 0;
            button1.Enabled = false;
            button2.Enabled = false;
            textBox1.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine(textBox1.Text);
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                textBox2.Text = serialPort1.ReadLine();

            }
            catch (TimeoutException)
            {
                textBox2.Text = "Timeout Exeption";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort1.Close();
            progressBar1.Value = 0;
            button1.Enabled = false;
            button2.Enabled = false;
            textBox1.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string pathFile = @"C:\Test\Data\"; //TODO neue Messdatei schreiben
                string fileName = "Messdaten.txt";
                System.IO.File.WriteAllText(pathFile + fileName, textBox2.Text);
                MessageBox.Show("Data has been saved to " + pathFile, "Save File");
            }
            catch (Exception ex3)
            {

                MessageBox.Show(ex3.Message);
            }
            
        }
    }
        
        
            
        
}




