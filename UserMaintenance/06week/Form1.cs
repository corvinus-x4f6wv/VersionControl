using _06week.Entities;
using _06week.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace _06week
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> currencies = new BindingList<string>();
        public Form1()
        {
            InitializeComponent();
            comboBox1.DataSource = currencies;
            RefreshData();
        }

        private void RefreshData()
        {
            Rates.Clear();
            string xmlstring = Consume();
            LoadXML(xmlstring);
            dg1.DataSource = Rates;
            Charting();
        }

        private void Charting()
        {
            chartRateData.DataSource = Rates;
            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;
            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
            var legend = chartRateData.Legends[0];
        }
        private void LoadXML(string input)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(input);
            foreach (XmlElement item in xml.DocumentElement)
            {
                RateData r = new RateData();
                r.Date = DateTime.Parse(item.GetAttribute("date"));
                XmlElement child = (XmlElement)item.FirstChild;
                r.Currency = child.GetAttribute("curr");
                r.Value = decimal.Parse(child.InnerText);
                int unit = int.Parse(child.GetAttribute("unit"));
                if (unit != 0)
                {
                    r.Value = r.Value / unit;
                }
                Rates.Add(r);
            }
        }
        string Consume()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetExchangeRatesRequestBody();
            {
                request.currencyNames = comboBox1.SelectedItem.ToString();
                request.startDate = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                request.endDate = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                var response = mnbService.GetExchangeRates(request);
                string result = response.GetExchangeRatesResult;
                return result;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void filterChanged(object sender, EventArgs e)
        {

        }
    }
}
