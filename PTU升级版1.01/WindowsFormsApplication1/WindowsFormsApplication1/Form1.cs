using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;            
using System.Net.Sockets;    
using System.Threading;
using System.IO;

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        int[] inputdata = new int[100];
        //int x1;
        private UdpClient server;
        IPAddress hostip = IPAddress.Parse("10.2.2.217");  //对方的  destIP  "10.1.1.217"
        IPAddress destip = IPAddress.Parse("10.1.2.217");
        IPAddress destip2 = IPAddress.Parse("10.2.2.217");
        private IPEndPoint receivepoint;
        private IPEndPoint receivepoint2;
        private int port = 1011; // //定义对方端口号
        private int destport = 1011;  // 控制器的端口号1
        private int destport2 = 1011; // 控制器的端口号2
        private Thread startserver;
        private delegate void FlushClient();//代理
        public static byte[] senddata = new byte[500];
        public static byte[] recvdata = new byte[400];
        public static byte[] rec = new byte[200];
        private byte[] temA1 = { 0, 0 };
        private int tem1 = 0;
        private byte[] temA2 = { 0, 0 };
        private int tem2 = 0;
        private byte[] temA3 = { 0, 0 };
        private int tem3 = 0;
        private byte[] temA4 = { 0, 0 };
        private int tem4 = 0;
        private byte[] temA5 = { 0, 0 };
        private int tem5 = 0;
        private byte[] temA6 = { 0, 0 };
        private int tem6 = 0;

        private byte[] tNum1 = { 0, 0 };
        private byte[] tNum2 = { 0, 0 };
        private byte[] tNum3 = { 0, 0 };
        private byte[] tNum4 = { 0, 0 };
        private byte[] tNum5 = { 0, 0 };
        private byte[] tNum6 = { 0, 0 };
        private string b = string.Empty;
        private string b1 = string.Empty;
        private string b2 = string.Empty;
        private static byte[] array1 = new byte[8];//2019 12 25拆分测试 
        private static byte[] array2 = new byte[8];//2019 12 25拆分测试
        private static byte[] array3 = new byte[8];//2019 12 25拆分测试
        private static byte[] array4 = new byte[8];//2019 12 25拆分测试
        private static byte[] array5 = new byte[8];//2019 12 25拆分测试

        ParseSockData parseClass = new ParseSockData();
     //   Form2 frm2 = new Form2();


         string filename = @"D:\MuComid";
        // FileStream fs;
        // BinaryReader br;


        /*******************************************************
        *   UDP 网络数据接收线程
        ********************************************************/

        private void start_server()
        {
            ASCIIEncoding encode = new ASCIIEncoding();
            while (true)
            {
                Thread.Sleep(100);
                //ThreadFunction();  //第二种线程使用控件的方法
                try
                {
                    byte[] rec = server.Receive(ref receivepoint);
  
                    byte[] rec2 = server.Receive(ref receivepoint2);

                    String temp="";
                    for (int j = 300; j < 400; j++)
                    {
                        temp = temp + "recvdata[" + j + "] = "+ rec[j].ToString() + "\n";

                    }
                    textBox3.Text = temp;

                    //控制方式打印
                    if (rec[0] == 0)
                    {
                        textBox52.Text = "停机";
                    }
                    else if (rec[0] == 1)
                    {
                        textBox52.Text = "自动";
                    }
                    else if (rec[0] == 2)
                    {
                        textBox52.Text = "通风";
                    }
                    else if (rec[0] == 3)
                    {
                        textBox52.Text = "紧急风";
                    }
                    else if (rec[0] == 4)
                    {
                        textBox52.Text = "紧急关";
                    }
                    else if (rec[0] == 5)
                    {
                        textBox52.Text = "PTU";
                    }
                    else if (rec[0] == 6)
                    {
                        textBox52.Text = "手动";
                    }
                    else if (rec[0] == 7)
                    {
                        textBox52.Text = "预冷";
                    }
                    else if (rec[0] == 8)
                    {
                        textBox52.Text = "预热";
                    }
                    else if (rec[0] == 9)
                    {
                        textBox52.Text = "洗车";
                    }
                    else if (rec[0] == 10)
                    {
                        textBox52.Text = "整备";
                    }



                    // 2019 12 25 对所以进行显示
                    for (int j = 1; j < 81; j++)
                    {
                        b1 = "Button" + j.ToString();
                        if (rec[j + 162] == 1)
                        {
                            ((Button)this.Controls[string.Format("button{0}", j + 1)]).BackColor = Color.Green;
                        }
                        else
                        {
                            ((Button)this.Controls[string.Format("button{0}", j + 1)]).BackColor = Color.Red;
                        }
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        array1[i] = (byte)((rec[12] & 0x01 << i) >> i);
                        array2[i] = (byte)((rec[13] & 0x01 << i) >> i);
                        array3[i] = (byte)((rec[14] & 0x01 << i) >> i);
                        array4[i] = (byte)((rec[15] & 0x01 << i) >> i);
                        array5[i] = (byte)((rec[16] & 0x01 << i) >> i);
                    }
                    for (int j = 82; j < 122; j++)
                    {
                        b2 = "Button" + j.ToString();
                        if (j > 113)
                        {
                            if (array5[j - 114] == 1)
                            {
                                ((Button)this.Controls[string.Format("button{0}", j)]).BackColor = Color.Yellow;
                            }
                            else
                            {
                                ((Button)this.Controls[string.Format("button{0}", j)]).BackColor = Color.Blue;
                            }
                        }
                        else if (j > 105)
                        {
                            if (array4[j - 106] == 1)
                            {
                                ((Button)this.Controls[string.Format("button{0}", j)]).BackColor = Color.Yellow;
                            }
                            else
                            {
                                ((Button)this.Controls[string.Format("button{0}", j)]).BackColor = Color.Blue;
                            }
                        }
                        else if (j > 97)
                        {
                            if (array3[j - 98] == 1)
                            {
                                ((Button)this.Controls[string.Format("button{0}", j)]).BackColor = Color.Yellow;
                            }
                            else
                            {
                                ((Button)this.Controls[string.Format("button{0}", j)]).BackColor = Color.Blue;
                            }
                        }
                        else if (j > 89)
                        {
                            if (array2[j - 90] == 1)
                            {
                                ((Button)this.Controls[string.Format("button{0}", j)]).BackColor = Color.Yellow;
                            }
                            else
                            {
                                ((Button)this.Controls[string.Format("button{0}", j)]).BackColor = Color.Blue;
                            }
                        }
                        else
                        {
                            if (array1[j - 82] == 1)
                            {
                                ((Button)this.Controls[string.Format("button{0}", j)]).BackColor = Color.Yellow;
                            }
                            else
                            {
                                ((Button)this.Controls[string.Format("button{0}", j)]).BackColor = Color.Blue;
                            }
                        }

                        
                    }
                }
                catch
                {

                }

            }
        }

       
        public Form1()
        {
            InitializeComponent();           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
             //利用本地端口号初始化一个UDP网络服务
            server = new UdpClient(1012);   // //定义自己的端口号
            receivepoint = new IPEndPoint(hostip, port); //127,1011    destIP  对方的
            receivepoint2 = new IPEndPoint(destip2, destport2); //10.2.1.216   1012
            startserver = new Thread(new ThreadStart(start_server));
            //启动线程
            startserver.IsBackground = true;  //第二种线程使用控件方法
            startserver.Start();
            //解决线程中使用控件的方法1
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            inputdata[10] = 0x01;

            //2019 12 25 按钮初始值
            for (int i = 1; i < 81; i++)
            {
                b = "Button" + i.ToString();
                ((Button)this.Controls[string.Format("button{0}", i + 1)]).BackColor = Color.Red;
            }
            for (int j = 82; j < 122; j++)
            {
                b = "Button" + j.ToString();
                ((Button)this.Controls[string.Format("button{0}", j)]).BackColor = Color.Blue;
            }
        }


      

        /***************************************************************************
        *function:      btnDispFrm2 按钮
        *description:   显示窗口2的参数，证明公用属性实现窗体间数据实时传输是可行的。
        ****************************************************************************/

        private void btnDispFrm2_Click(object sender, EventArgs e)
        {
         
        }


        public  byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading

               // textBox1.Text = length.ToString();
                for (int i=0;i< length;i++)
                { 
                  //textBox1.Items.Add(buffer[i]);
                }
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }
        /*******************************************************
       *function:      set 按钮
       *description:   向receivepoint发一条数据包
       ********************************************************/
        private void button1_Click(object sender, EventArgs e)
        {
            ASCIIEncoding encode = new ASCIIEncoding();
            //  string sendstring = textBox1.Text;
            //byte[] senddata = encode.GetBytes(sendstring);
            Form1.senddata[23] = Convert.ToByte(textBox2.Text);
            Form1.senddata[55] = Convert.ToByte(textBox4.Text);
            Form1.senddata[56] = Convert.ToByte(textBox5.Text);
            Form1.senddata[57] = Convert.ToByte(textBox6.Text);
            Form1.senddata[58] = Convert.ToByte(textBox7.Text);
            Form1.senddata[5] = Convert.ToByte(textBox12.Text);
            Form1.senddata[6] = Convert.ToByte(textBox11.Text);
            Form1.senddata[7] = Convert.ToByte(textBox10.Text);
            Form1.senddata[8] = Convert.ToByte(textBox9.Text);
            Form1.senddata[9] = Convert.ToByte(textBox8.Text);
            Form1.senddata[10] = Convert.ToByte(textBox17.Text);
            Form1.senddata[11] = Convert.ToByte(textBox16.Text);
            Form1.senddata[12] = Convert.ToByte(textBox15.Text);
            Form1.senddata[13] = Convert.ToByte(textBox14.Text);
            Form1.senddata[14] = Convert.ToByte(textBox13.Text);
            Form1.senddata[15] = Convert.ToByte(textBox22.Text);
            Form1.senddata[16] = Convert.ToByte(textBox21.Text);
            Form1.senddata[17] = Convert.ToByte(textBox20.Text);
            Form1.senddata[18] = Convert.ToByte(textBox19.Text);
            Form1.senddata[54] = Convert.ToByte(textBox18.Text);
            Form1.senddata[20] = Convert.ToByte(textBox27.Text);

            //客室温度1
            tNum1 = BitConverter.GetBytes(Convert.ToInt16(textBox26.Text) * 10);
            Form1.senddata[24] = tNum1[0];
            Form1.senddata[25] = tNum1[1];

            //客室温度2
            tNum2 = BitConverter.GetBytes(Convert.ToInt16(textBox25.Text) * 10);
            Form1.senddata[26] = tNum2[0];
            Form1.senddata[27] = tNum2[1];

            //客室温度3
            tNum3 = BitConverter.GetBytes(Convert.ToInt16(textBox24.Text) * 10);
            Form1.senddata[28] = tNum3[0];
            Form1.senddata[29] = tNum3[1];

            //客室温度4
            tNum4 = BitConverter.GetBytes(Convert.ToInt16(textBox23.Text) * 10);
            Form1.senddata[30] = tNum4[0];
            Form1.senddata[31] = tNum4[1];

            //送风温度
            tNum5 = BitConverter.GetBytes(Convert.ToInt16(textBox54.Text) * 10);
            Form1.senddata[32] = tNum5[0];
            Form1.senddata[33] = tNum5[1];

            //新风温度
            tNum6 = BitConverter.GetBytes(Convert.ToInt16(textBox53.Text) * 10);
            Form1.senddata[34] = tNum6[0];
            Form1.senddata[35] = tNum6[1];

            this.textBox1.Text = "";
            this.textBox3.Text = "";
            for (int i = 0; i <= 100; i++)
            {
                this.textBox1.Text = this.textBox1.Text + "senddata[" + i + "] = " + Form1.senddata[i].ToString() + "\n";
            }
                      
            server.Send(senddata, senddata.Length, receivepoint);
            Thread.Sleep(500);
            server.Send(senddata, senddata.Length, receivepoint2);

        }
    }

    /*******************************************************
    *   解析字节数组的类
    ********************************************************/
    public class ParseSockData
    {
        public string value;

        public int GetBit(int data, int num)
        {
            return (data & (0x01 << num)) >> num;
        }
        public int SetBit(ref int data, int num)
        {
            data = data | (0x01 << num);
            return data;
        }
    }



}
