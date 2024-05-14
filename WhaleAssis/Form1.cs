using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;

namespace WhaleAssis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string RandomStr()
        {
            Random random = new Random();
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] result = new char[32];

            for (int i = 0; i < 32; i++)
            {
                result[i] = characters[random.Next(characters.Length)];
            }

            string randomString = new string(result);
            return randomString;
        }

        protected string RandomNum()
        {
            Random random = new Random();
            string randomString = "";

            for (int i = 0; i < 12; i++)
            {
                randomString += random.Next(10);
            }

            return randomString;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            var textBox1Text = textBox1.Text;
            var inviteCode = textBox2.Text;
            int number1 = 0;
            try
            {
                number1 = int.Parse(textBox1Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show("请输入数字！");
                return;
            }


            for (int i = 0; i < number1; i++)
            {
                label2.Text = "正在运行...";
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var deviceId = RandomStr();
                        var email = RandomNum();

                        // 设置请求的URL
                        string url =
                            "https://wa01.googla.org/account/register?platform=2&api_version=14&app_version=1.43&lang=zh&_key=&market_id=1000&pkg=com.bjchuhai&device_id=rk_" +
                            deviceId +
                            "&sys_version=13&ts=1688957158281&sub_pkg=com.bjchuhai&version_code=43";

                        // 创建要发送的内容
                        var content = new FormUrlEncodedContent(new Dictionary<string, string>
                        {
                            { "passwd", "e99a18c428cb38d5f260853678922e03" },
                            { "invite_code", inviteCode },
                            { "email", email + "@gmail.com" }
                        });

                        // 发送POST请求
                        HttpResponseMessage response = await client.PostAsync(url, content);

                        // 检查响应是否成功
                        response.EnsureSuccessStatusCode();

                        // 读取响应内容
                        string responseContent = await response.Content.ReadAsStringAsync();

                        // 处理响应内容，例如在窗体上显示

                        label2.Text = "第" + (i + 1) + "个月成功";
                        // 延迟一秒钟
                        await Task.Delay(1000);
                    }
                }
                catch (Exception ex)
                {
                    // 处理异常
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            button2.Enabled = true;
            label2.Text = "";
            MessageBox.Show("全部完成！");
        }
    }
}