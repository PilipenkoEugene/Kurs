using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Windows.Forms;

namespace Lab16
{
    class DataStorage : DataInterface
    {
        public bool IsReady
        {
            get
            {
                if (rawdata == null) return false;
                else return true;
            }
        }

        private List<RawDataItem> rawdata;
        private List<SummaryDataItem> sumdata;
        
        private char devider = '*';
        public DataStorage() { }

        private void BuildSummary()
        {
            Dictionary<int, float> tmp = new Dictionary<int, float>();
            string au = "";
            int[] check = new int[10000];
            sumdata = new List<SummaryDataItem>();

            for (int i = 0; i < (int)10000; i++)
            {
                check[i] = 0;
            }

            foreach (var item in rawdata)
            {
                if (check[item.id] == 0)
                {
                    foreach (var item1 in rawdata)
                    {
                        if (item1.id == item.id && au.Length > 0)
                            au += item1.Name;
                        else if (item1.id == item.id)
                            au += item1.Name;
                    }

                    sumdata.Add(new SummaryDataItem()
                    {
                        GroupValue = item.id,
                        GroupName = item.fLine,
                        GroupA = au,
                        GroupDate = item.date
                    });

                    check[item.id] = 1;
                    au = "";
                }
            }  
        }
        private bool InitData(String datapath)
        {
            rawdata = new List<RawDataItem>();

            try
            {
                StreamReader sr = new StreamReader(datapath, System.Text.Encoding.UTF8);
                String line;
                
                while ((line = sr.ReadLine()) != null)
                {
                    string[] items = line.Split(devider);
                    //MessageBox.Show(items[0]);
                    var item = new RawDataItem()
                    {
                        Name = items[0].Trim(),
                        fLine = items[1].Trim(),
                        date = items[2].Trim(),
                        id = Convert.ToInt32(items[3].Trim(), CultureInfo.InvariantCulture),          
                    };
                    rawdata.Add(item);
                }
                sr.Close();
                BuildSummary();
            } catch (IOException ex)
            {
                return false;
            }
            return true;
        }

        public static DataStorage DataCreator(String path)
        {
            DataStorage d = new DataStorage();
            if (d.InitData(path))
                return d;
            else
                return null;
        }

        public List<RawDataItem> GetRawData()
        {
            if (this.IsReady)
                return rawdata;
            else
                return null;
        }

        public List<SummaryDataItem> GetSummaryData()
        {
            if (this.IsReady)
                return sumdata;
            else
                return null;
        }
    }
}
