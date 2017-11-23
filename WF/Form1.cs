using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bitfinex.Net;
using Bitfinex.Net.Objects;
using System.Threading;
using System.Threading.Tasks;

namespace WF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Thread t = new Thread(start);
            //t.Start();
            Task<string> task = new Task<string>(() => start());
            task.Start();            
            this.richTextBox1.Text = task.Result;            
        }
        //static Task<int> CreateTask(string name)
        //{
        //    return new Task<int>(() => TaskMethod(name));
        //}

        public string start()
        {
            string info = "";
            BitfinexClient bit = new BitfinexClient();
            bit.SetApiKey("cndYoNyd8Tp2g1rAslKFPCcuxYklO3PEprVrlTYMT5l");
            bit.SetApiSecret("ms0cM9Hd1FgEkR6p9TZYVWfw9yDiO3OXg7o2mMhvvx8");
            BitfinexApiResult<BitfinexAccountInfo> result = bit.GetAccountInfo();
            if (result.Error == null)
            {
                info += "MakerFees:" + result.Result.MakerFees;
                info += "TakerFees:" + result.Result.TakerFees;
                int i = 1;
                foreach(BitfinexFee bf in result.Result.Fees)
                {
                    info += "BitfinexFee:"+i.ToString();
                    info += "MakerFees:" + bf.MakerFees;
                    info += "Pairs:" + bf.Pairs;
                    info += "TakerFees:" + bf.TakerFees;
                    info += "\r\n";
                    i++;
                }               
            }
            else
            {
                info = "error:" + result.Error.ErrorCode.ToString() + result.Error.ErrorMessage;
            }
            return info;            
        }
    }
}
