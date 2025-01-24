using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Drawing.Text;
using Application = System.Windows.Forms.Application;

namespace PM4MAto
{
    public partial class Frm_Main : Form
    {
        List<Control[]> controlmatrix = new List<Control[]>();
        List<string[]> data_user = new List<string[]>();
        List<string[]> data_userTHAYTHE = new List<string[]>();
        List<string[]> data_userHT = new List<string[]>();
        List<string[]> data_userNEW = new List<string[]>();
        private SerialPort serialPort;
        private SerialPort Port;

        public Frm_Main()
        {
            InitializeComponent();
            string filePath_user = Path.Combine(Application.StartupPath, "DATA", "USER.CSV");
            string filePath_userTHAYTHE = Path.Combine(Application.StartupPath, "DATA", "USERTHAYTHE.CSV");
            string filePath_userHT = Path.Combine(Application.StartupPath, "DATA", "USERTHT.CSV");
            string filePath_userNEW = Path.Combine(Application.StartupPath, "DATA", "USERNEW.CSV");
            // day la duong dan den file deerr vua roi
            //sau khi co filepath thi anh goi ham vua tao len de load danh sach len
            loadcsvfile(filePath_user, data_user);
            loadcsvfile1(filePath_userTHAYTHE, data_userTHAYTHE);
            loadcsvfile2(filePath_userHT, data_userHT);
            loadcsvfile3(filePath_userNEW, data_userNEW);
            //ok the nay la xong anh


            this.FormClosed += new FormClosedEventHandler(Frm_Main_FormClosed);
            if (Port == null)
            {
                Port = new SerialPort("COM22", 9600);
                Port.Open();
            }
        }

        private void Frm_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Port != null && Port.IsOpen)
            {
                Port.Close();
            }
        }

        //day la ham load file csv len dung
        private void loadcsvfile(string filepath, List<string[]> data)
        {
            using (StreamReader reader = new StreamReader(filepath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] columns = line.Split(',');
                    data_user.Add(columns);
                }
            }
        }
        private void loadcsvfile1(string filepath, List<string[]> data)
        {
            using (StreamReader reader = new StreamReader(filepath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] columns = line.Split(',');
                    data_userTHAYTHE.Add(columns);
                }
            }
        }
        private void loadcsvfile2(string filepath, List<string[]> data)
        {
            using (StreamReader reader = new StreamReader(filepath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] columns = line.Split(',');
                    data_userHT.Add(columns);
                }
            }
        }

        private void loadcsvfile3(string filepath, List<string[]> data)
        {
            using (StreamReader reader = new StreamReader(filepath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] columns = line.Split(',');
                    data_userNEW.Add(columns);
                }
            }
        


            // them data vao matran control
            controlmatrix.Add(new Control[] { lb_vtri1, lb_vtri2, lb_vtri3, lb_vtri4, lb_vtri5, lb_vtri6, lb_vtri7, lb_vtri8, lb_vtri9, lb_vtri11, lb_vtri12, lb_vtri13, lb_vtri14, lb_vtri15, lb_vtri16, lb_vtri17, lb_vtri18, lb_vtri19, lb_vtri20, lb_vtri21, lb_vtri22, lb_vtri23, lb_vtri24, lb_vtri25, lb_vtri26, lb_vtri27, lb_vtri28, lb_vtri29, lb_vtri30, lb_vtri31 });
            controlmatrix.Add(new Control[] { code1, code2, code3, code4, code5, code6, code7, code8, code9, code10, code11, code12, code13, code14, code15, code16, code17, code18, code19, code20, code21, code22, code23, code24, code25, code26, code27, code28, code29, code30, code31 });
            controlmatrix.Add(new Control[] { name1, name2, name3, name4, name5, name6, name7, name8, name9, name10, name11, name12, name13, name14, name15, name16, name17, name18, name19, name20, name21, name22, name23, name24, name25, name26, name27, name28, name29, name30, name31 });


            serialPort = new SerialPort("COM1");
            serialPort.BaudRate = 9600;
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            serialPort.Open();
        }
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string data = sp.ReadLine().Trim();

            for (int i = 0; i < data_user.Count; i++)
            {
                if (data == data_user[i][1])
                {
                    int index = int.Parse(data_user[i][0]) - 1;
                    this.Invoke(new Action(() =>
                    {

                        controlmatrix[1][index].Text = data;
                        controlmatrix[1][index].BackColor = Color.Blue;
                        controlmatrix[2][index].Text = data_user[i][2];
                        controlmatrix[2][index].BackColor = Color.Blue;
                        Port.Write("G" + index);
                    }));
                    break;
                }
                for (int j = 0; j < data_userTHAYTHE.Count; j++)
                {
                    if (data == data_userTHAYTHE[j][1])
                    {
                        int index = int.Parse(data_userTHAYTHE[j][0]) - 1;
                        this.Invoke(new Action(() =>
                        {

                            controlmatrix[1][index].Text = data;
                            controlmatrix[1][index].BackColor = Color.Green;
                            controlmatrix[2][index].Text = data_userTHAYTHE[j][2];
                            controlmatrix[2][index].BackColor = Color.Green;
                            Port.Write("G" + index);
                        }));
                        break;
                    }
                    for (int g = 0; g < data_userHT.Count; g++)
                    {
                        if (data == data_userHT[g][1])
                        {
                            int index = int.Parse(data_userHT[g][0]) - 1;
                            this.Invoke(new Action(() =>
                            {

                                controlmatrix[1][index].Text = data;
                                controlmatrix[1][index].BackColor = Color.Yellow;
                                controlmatrix[2][index].Text = data_userHT[g][2];
                                controlmatrix[2][index].BackColor = Color.Yellow;
                                Port.Write("Y" + index);
                            }));
                            break;
                        }
                        for (int h = 0; h < data_userNEW.Count; h++)
                        {
                            if (data == data_userNEW[h][1])
                            {
                                int index = int.Parse(data_userNEW[h][0]) - 1;
                                this.Invoke(new Action(() =>
                                {

                                    controlmatrix[1][index].Text = data;
                                    controlmatrix[1][index].BackColor = Color.Violet;
                                    controlmatrix[2][index].Text = data_userNEW[h][2];
                                    controlmatrix[2][index].BackColor = Color.Violet;
                                    Port.Write("V" + index);
                                }));
                                break;
                            }
                        }
                    }
                }
            }
        }


                    private void Frm_Main_Load(object sender, EventArgs e)
                    {
                        this.FormClosed += new FormClosedEventHandler(Frm_Main_FormClosed);
                        if (Port == null)
                        {
                            Port = new SerialPort("COM22", 9600);
                            Port.Open();
                            {
                                if (Port != null && Port.IsOpen)
                                {
                                    Port.Close();
                                }
                            }
                        }
                    }
                    private void button1_Click(object sender, EventArgs e)
                        {
                    Port.Write("START");
                         }

                    private void button2_Click(object sender, EventArgs e)
                    {
                        {
                            Port.Write("END");
                        }
                    }

        private void name15_Click(object sender, EventArgs e)
        {

        }

        private void QL_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void code11_Click(object sender, EventArgs e)
        {

        }

        private void label104_Click(object sender, EventArgs e)
        {

        }
    }
}
