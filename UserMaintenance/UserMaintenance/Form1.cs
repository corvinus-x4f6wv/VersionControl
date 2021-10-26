﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMaintenance.Entities;


namespace UserMaintenance
{
    public partial class Form1 : Form
    {
      BindingList<User> users = new BindingList<User>();
      
        public Form1()
        {
            InitializeComponent();
            lblFullName.Text = Resource1.FullName; // label1

            btnAdd.Text = Resource1.Add; // button1
            btnExport.Text = Resource1.Export;

            listUsers.DataSource = users;
            listUsers.ValueMember = "ID";
            listUsers.DisplayMember = "FullName";

            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };


            var response = mnbService.GetExchangeRates(request);


            var result = response.GetExchangeRatesResult;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var u = new User()
            {
                FullName = txtFullName.Text,

            };
            users.Add(u);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() != DialogResult.OK) return;
            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                foreach (var u in users)
                {
                    sw.Write(u.ID);
                    sw.Write(u.FullName);
                    sw.WriteLine();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var törlés = listUsers.SelectedItem;
            if (törlés != null)
            {
                users.Remove((User)törlés);
            }
        }
    }
}
