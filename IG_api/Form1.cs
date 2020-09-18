using dto.endpoint.auth.session.v2;
using IGWebApiClient;
using IGWebApiClient.Common;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IG_api
{
    public partial class Form1 : Form
    {
		
        public Form1()
        {
            InitializeComponent();
            CreateRectangle rectangleForm = new CreateRectangle();
            ArrayList gList = rectangleForm.createGreenRectangleFunction();
            ArrayList rList = rectangleForm.createRedRectangleFunction();

            MarketData mk = new MarketData();
            mk.MarketDataList();

            //初始化表格
            DataGridViewRowCollection rows = dataGridView1.Rows;
            for (int i=0; i< MarketData.DataModelDict.Count+5; i++)
            {
                rows.Add(null, null,  null, null, null);                
                this.dataGridView1.Rows[i].Cells[3] = new DataGridViewImageCell();
                rows[i].Cells[3].Value = gList[0];
            }
						
           
            mk.Updated = (dataModelDict) =>
            {              
             int i = 0;
                foreach (KeyValuePair<string,L1LsPriceData> dm in dataModelDict)
                {
                    if (MarketData.ErrorMarketDataEpicsList.Contains(dm.Key))
                    {
                        continue;
                    }

                    rows[i].Cells[5].Value = Convert.ToString(dm.Value.Bid);


                    if (dm.Value.High == null || dm.Value.Low == null)
                    {
                        continue;
                    }
                    var Denominator = dm.Value.High - dm.Value.Low;
                    if (Denominator == 0)
                    {
                        rows[i].Cells[4].Value = 0+"%";
                       
                        rows[i].Cells[3].Value = gList[0];
                    }
                    else
                    {
                        if (dm.Value.Bid == null || dm.Value.Offer == null)
                        {
                            continue;
                        }
                        var mean = (dm.Value.High + dm.Value.Low) / 2;

                        var ChangeRate = 100 * ((dm.Value.Bid+dm.Value.Offer)/2 - mean) / (Denominator/2);
                        rows[i].Cells[4].Value = Math.Round((double)ChangeRate, 2, MidpointRounding.AwayFromZero) + "%";

                        if (ChangeRate >= 0)
                        {
                            
                            rows[i].Cells[3].Value = gList[(int)ChangeRate];
                        }
                        else
                        {
                            
                            rows[i].Cells[3].Value = rList[-(int)ChangeRate];
                        }
                    }

                    string[] arrayString = dm.Key.Split(':');
                    rows[i].Cells[2].Value = mk.MarketDataInstrumentNameDict[arrayString[1]];
                    rows[i].Cells[1].Value = dm.Key;
                    rows[i].Cells[0].Value = mk.MarketDataEpicsDict[arrayString[1]];

                    i++;
                }

            };
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

		private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
