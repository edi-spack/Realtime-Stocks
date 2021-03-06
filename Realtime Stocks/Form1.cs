﻿using System;
using System.IO;
using System.Drawing;
using System.Net;
using System.Windows.Forms;
using System.Globalization;

namespace Realtime_Stocks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int cw = 10, w = 800, h = 400, offset = 20, nrd = 30;
        bool firstTime = true, draw = true;
        double lo, hi;
        StockExchange[] stocks = new StockExchange[nrd];
        DateTime first, last;
        Graphics g;
        string offlineStocks =
@"timestamp,open,high,low,close,volume
2020-04-08,262.7400,267.3700,261.2300,266.0700,41207280
2020-04-07,270.8000,271.7000,259.0000,259.4300,50721831
2020-04-06,250.9000,263.1100,249.3800,262.4700,50455071
2020-04-03,242.8000,245.7000,238.9741,241.4100,32470017
2020-04-02,240.3400,245.1500,236.9000,244.9300,41483493
2020-04-01,246.5000,248.7200,239.1300,240.9100,44054638
2020-03-31,255.6000,262.4900,252.0000,254.2900,49250501
2020-03-30,250.7400,255.5200,249.4000,254.8100,41994110
2020-03-27,252.7500,255.8700,247.0500,247.7400,51054153
2020-03-26,246.5200,258.6800,246.3600,258.4400,63140169
2020-03-25,250.7500,258.2500,244.3000,245.5200,75900510
2020-03-24,236.3600,247.6900,234.3000,246.8800,71882773
2020-03-23,228.0800,228.4997,212.6100,224.3700,84188208
2020-03-20,247.1800,251.8300,228.0000,229.2400,100423346
2020-03-19,247.3850,252.8400,242.6100,244.7800,67964255
2020-03-18,239.7700,250.0000,237.1200,246.6700,75058406
2020-03-17,247.5100,257.6100,238.4000,252.8600,81013965
2020-03-16,241.9500,259.0800,240.0000,242.2100,80605865
2020-03-13,264.8900,279.9200,252.9500,277.9700,92683032
2020-03-12,255.9400,270.0000,248.0000,248.2300,104618517
2020-03-11,277.3900,281.2200,271.8600,275.4300,64094970
2020-03-10,277.1400,286.4400,269.3700,285.3400,71322520
2020-03-09,263.7500,278.0900,263.0000,266.1700,71686208
2020-03-06,282.0000,290.8200,281.2300,289.0300,56544246
2020-03-05,295.5200,299.5500,291.4100,292.9200,46893219
2020-03-04,296.4400,303.4000,293.1300,302.7400,54794568
2020-03-03,303.6700,304.0000,285.8000,289.3200,79868852
2020-03-02,282.2800,301.4400,277.7200,298.8100,85349339
2020-02-28,257.2600,278.4100,256.3700,273.3600,106721230
2020-02-27,281.1000,286.0000,272.9600,273.5200,80151381
2020-02-26,286.5300,297.8800,286.5000,292.6500,49678431
2020-02-25,300.9500,302.5300,286.1300,288.0800,57668364
2020-02-24,297.2600,304.1800,289.2300,298.1800,55548828
2020-02-21,318.6200,320.4500,310.5000,313.0500,32426415
2020-02-20,322.6300,324.6500,318.2100,320.3000,25141489
2020-02-19,320.0000,324.5700,320.0000,323.6200,23495991
2020-02-18,315.3600,319.7500,314.6100,319.0000,38190545
2020-02-14,324.7300,325.9800,322.8500,324.9500,20028447
2020-02-13,324.1900,326.2200,323.3500,324.8700,23686892
2020-02-12,321.4700,327.2200,321.4700,327.2000,28432573
2020-02-11,323.6000,323.9000,318.7100,319.6100,23580780
2020-02-10,314.1800,321.5500,313.8500,321.5500,27337215
2020-02-07,322.3700,323.4000,318.0000,320.0300,29421012
2020-02-06,322.5700,325.2200,320.2648,325.2100,26356385
2020-02-05,323.5200,324.7600,318.9500,321.4500,29706718
2020-02-04,315.3100,319.6400,313.6345,318.8500,34154134
2020-02-03,304.3000,313.4900,302.2200,308.6600,43496401
2020-01-31,320.9300,322.6800,308.2900,309.5100,49897096
2020-01-30,320.5435,324.0900,318.7500,323.8700,31685808
2020-01-29,324.4500,327.8500,321.3800,324.3400,54149928
2020-01-28,312.6000,318.4000,312.1900,317.6900,40558486
2020-01-27,310.0600,311.7700,304.8800,308.9500,40485005
2020-01-24,320.2500,323.3300,317.5188,318.3100,36634380
2020-01-23,317.9200,319.5600,315.6500,319.2300,26117993
2020-01-22,318.5800,319.9900,317.3100,317.7000,25458115
2020-01-21,317.1900,319.0200,316.0000,316.5700,27235039
2020-01-17,316.2700,318.7400,315.0000,318.7300,34454117
2020-01-16,313.5900,315.7000,312.0900,315.2400,27207254
2020-01-15,311.8500,315.5000,309.5500,311.3400,30480882
2020-01-14,316.7000,317.5700,312.1700,312.6800,40653457
2020-01-13,311.6400,317.0700,311.1500,316.9600,30028742
2020-01-10,310.6000,312.6700,308.2500,310.3300,35217272
2020-01-09,307.2350,310.4300,306.2000,309.6300,42621542
2020-01-08,297.1600,304.4399,297.1560,303.1900,33090946
2020-01-07,299.8400,300.9000,297.4800,298.3900,27877655
2020-01-06,293.7900,299.9600,292.7500,299.8000,29644644
2020-01-03,297.1500,300.5800,296.5000,297.4300,36633878
2020-01-02,296.2400,300.6000,295.1900,300.3500,33911864
2019-12-31,289.9300,293.6800,289.5200,293.6500,25247625
2019-12-30,289.4600,292.6900,285.2200,291.5200,36059614
2019-12-27,291.1200,293.9700,288.1200,289.8000,36592936
2019-12-26,284.8200,289.9800,284.7000,289.9100,23334004
2019-12-24,284.6900,284.8900,282.9197,284.2700,12119714
2019-12-23,280.5300,284.2500,280.3735,284.0000,24677883
2019-12-20,282.2300,282.6500,278.5600,279.4400,69032743
2019-12-19,279.5000,281.1800,278.9500,280.0200,24626947
2019-12-18,279.8000,281.9000,279.1200,279.7400,29024687
2019-12-17,279.5700,281.7700,278.8000,280.4100,28575798
2019-12-16,277.0000,280.7900,276.9800,279.8600,32081105
2019-12-13,271.4600,275.3000,270.9300,275.1500,33432806
2019-12-12,267.7800,272.5599,267.3210,271.4600,34437042
2019-12-11,268.8100,271.1000,268.5000,270.7700,19723391
2019-12-10,268.6000,270.0700,265.8600,268.4800,22632383
2019-12-09,270.0000,270.8000,264.9100,266.9200,32182645
2019-12-06,267.4800,271.0000,267.3000,270.7100,26547493
2019-12-05,263.7900,265.8900,262.7300,265.5800,18661343
2019-12-04,261.0700,263.3100,260.6800,261.7400,16810388
2019-12-03,258.3100,259.5300,256.2900,259.4500,29377268
2019-12-02,267.2700,268.2500,263.4500,264.1600,23693550
2019-11-29,266.6000,268.0000,265.9000,267.2500,11654363
2019-11-27,265.5800,267.9800,265.3100,267.8400,16386122
2019-11-26,266.9400,267.1600,262.5000,264.2900,26334882
2019-11-25,262.7100,266.4400,262.5200,266.3700,21029517
2019-11-22,262.5900,263.1800,260.8400,261.7800,16331263
2019-11-21,263.6900,264.0050,261.1800,262.0100,30348778
2019-11-20,265.5400,266.0830,260.4000,263.1900,26609919
2019-11-19,267.9000,268.0000,265.3926,266.2900,19069597
2019-11-18,265.8000,267.4300,264.2300,267.1000,21700897
2019-11-15,263.6800,265.7800,263.0100,265.7600,25093666
2019-11-14,263.7500,264.8800,262.1000,262.6400,22395556
";

        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (firstTime)
            {
                firstTime = false;
                stocksComboBox.SelectedIndex = 0;
            }
        }

        private void OnStockChanged(object sender, EventArgs e)
        {
            GetStocks(stocksComboBox.SelectedItem.ToString());
            if (draw)
            {
                Redraw();
            }
            else
            {
                g = this.CreateGraphics();
                g.Clear(this.BackColor);
            }
        }

        private void GetStocks(string symbol)
        {
            int i;
            string url = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&datatype=csv&symbol=" + symbol + "&apikey=SKZJIS6SR29Z62E1";
            WebRequest request = WebRequest.Create(url);
            string result;
            try
            {
                Stream stream = request.GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                result = reader.ReadToEnd().ToString();
                this.Text = "Realtime Stocks - " + symbol;
            }
            catch //(WebException e)
            {
                result = offlineStocks;
                this.Text = "Realtime Stocks - OFFLINE MODE";
            }
            string[] delims = new string[] {"\r\n"};
            string[] days = result.Split(delims, StringSplitOptions.None);
            delims = new string[] {","};
            string[] day;
            lo = double.MaxValue;
            hi = double.MinValue;
            if(days.Length < nrd)
            {
                draw = false;
                this.Text = "Realtime Stocks - Limit of 5 requests per minute / 500 requests per day reached";
            }
            else
            {
                draw = true;
                for (i = 1; i <= nrd; i++)
                {
                    day = days[i].Split(delims, StringSplitOptions.None);
                    stocks[nrd - i] = new StockExchange(double.Parse(day[1], CultureInfo.InvariantCulture), Convert.ToDouble(day[2], CultureInfo.InvariantCulture), Convert.ToDouble(day[3], CultureInfo.InvariantCulture), Convert.ToDouble(day[4], CultureInfo.InvariantCulture));
                    if(stocks[nrd - i].GetHigh() > hi)
                    {
                        hi = stocks[nrd - i].GetHigh();
                    }
                    if (stocks[nrd - i].GetLow() < lo)
                    {
                        lo = stocks[nrd - i].GetLow();
                    }
                    if(i == 1)
                    {
                        last = DateTime.Parse(day[0]);
                    }
                    if(i == nrd)
                    {
                        first = DateTime.Parse(day[0]);
                    }
                }
            }
        }

        private void Redraw()
        {
            g = this.CreateGraphics();
            Pen blackPen = new Pen(Color.Black);
            SolidBrush redBrush = new SolidBrush(Color.Red);
            SolidBrush greenBrush = new SolidBrush(Color.Green);
            Font font = new Font("Arial", 10);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            int i, x1, y1, x2, y2, w1, h1;

            g.Clear(this.BackColor);
            g.DrawRectangle(blackPen, offset, offset, w, h);

            for(i = 0; i < nrd; i++)
            {
                x1 = offset + cw * (i + 1) + (w - nrd * cw) / nrd * i + cw / 2;
                y1 = (int)(offset + h - (stocks[i].GetHigh() - lo) * (double) h / (hi - lo));
                x2 = offset + cw * (i + 1) + (w - nrd * cw) / nrd * i + cw / 2;
                y2 = (int)(offset + h - (stocks[i].GetLow() - lo) * (double) h / (hi - lo));
                g.DrawLine(blackPen, x1, y1, x2, y2);

                x1 = offset + cw * (i + 1) + (w - nrd * cw) / nrd * i;
                w1 = cw;
                if(stocks[i].GetOpen() < stocks[i].GetClose())
                {
                    y1 = (int)(offset + h - (stocks[i].GetClose() - lo) * (double) h / (hi - lo));
                    h1 = (int)(offset + h - (stocks[i].GetOpen() - lo) * (double)h / (hi - lo)) - y1;
                    g.FillRectangle(greenBrush, x1, y1, w1, h1);
                }
                else
                {
                    y1 = (int)(offset + h - (stocks[i].GetOpen() - lo) * (double)h / (hi - lo));
                    h1 = (int)(offset + h - (stocks[i].GetClose() - lo) * (double)h / (hi - lo)) - y1;
                    g.FillRectangle(redBrush, x1, y1, w1, h1);
                }
            }

            g.DrawString(hi.ToString(), font, blackBrush, offset, offset);
            g.DrawString(lo.ToString(), font, blackBrush, offset, offset + h - font.Height);
            g.DrawString(first.ToShortDateString(), font, blackBrush, offset, offset + h);
            g.DrawString(last.ToShortDateString(), font, blackBrush, offset + w - 85, offset + h);
        }
    }

    public class StockExchange
    {
        private double open, high, low, close;

        public StockExchange(double o, double h, double l , double c)
        {
            open = o;
            high = h;
            low = l;
            close = c;
        }

        public double GetOpen()
        {
            return open;
        }

        public double GetHigh() // != 🌿🚬
        {
            return high;
        }

        public double GetLow()
        {
            return low;
        }

        public double GetClose()
        {
            return close;
        }
    }
}