using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsServerInstall
{
    public partial class Form1 : Form
    {
        public Form1()
        {
        
            InitializeComponent();
        }

        private string _installUtilUrl = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe";

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();       //获得路径
            textBox1.Text = path;                                                                            //由一个textBox显示路径
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;                                                              //重要代码：表明是所有类型的数据，比如文件路径
            else
                e.Effect = DragDropEffects.None;
        }

        private void button1_Click(object sender, EventArgs e)
        {

           Thread t= new Thread(c =>
           {
               Console.WriteLine(456);


               System.Diagnostics.Process p = new System.Diagnostics.Process();
               p.StartInfo.FileName = "cmd.exe";
               p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
               p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
               p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
               p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
               p.StartInfo.CreateNoWindow = true;//不显示程序窗口
               p.Start();//启动程序

               //向cmd窗口发送输入信息
               p.StandardInput.WriteLine(_installUtilUrl+" "+textBox1.Text  + "&exit");

               p.StandardInput.AutoFlush = true;
               //p.StandardInput.WriteLine("exit");
               //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
               //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令



               //获取cmd窗口的输出信息
               string output = p.StandardOutput.ReadToEnd();

               //StreamReader reader = p.StandardOutput;
               //string line=reader.ReadLine();
               //while (!reader.EndOfStream)
               //{
               //    str += line + "  ";
               //    line = reader.ReadLine();
               //}

               p.WaitForExit();//等待程序执行完退出进程
               p.Close();
               MessageBox.Show(output);
            
           });
            t.Start();


            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            Thread t = new Thread(c =>
            {
                Console.WriteLine(456);


                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                p.Start();//启动程序

                //向cmd窗口发送输入信息
                p.StandardInput.WriteLine(_installUtilUrl + " " + textBox1.Text+" /u" + "&exit");

                p.StandardInput.AutoFlush = true;
                //p.StandardInput.WriteLine("exit");
                //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
                //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令



                //获取cmd窗口的输出信息
                string output = p.StandardOutput.ReadToEnd();

                //StreamReader reader = p.StandardOutput;
                //string line=reader.ReadLine();
                //while (!reader.EndOfStream)
                //{
                //    str += line + "  ";
                //    line = reader.ReadLine();
                //}

                p.WaitForExit();//等待程序执行完退出进程
                p.Close();
                MessageBox.Show(output);

            });
            t.Start();

        }
    }
}
