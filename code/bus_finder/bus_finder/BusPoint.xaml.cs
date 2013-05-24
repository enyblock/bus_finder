using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using SQLiteClient;

namespace bus_finder
{
    public partial class BusPoint : PhoneApplicationPage
    {
        public BusPoint()
        {
            InitializeComponent();
        }

        // set the title name 
        private void set_title_name(int num)
        {
            
            string zhan = "";

            if (NavigationContext.QueryString.TryGetValue("zhan", out zhan))
            {
                lines_info_panoramaitem.Header = string.Format("经过\"{0}\"\n的线路有{1}条:",zhan,num);
                
            }
        }


        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            

            //time output
            lbOutput.Text = "";

            // get the line number which you input
            string zhan = "";
            string select_cmd = "";

            if (NavigationContext.QueryString.TryGetValue("zhan", out zhan))
            {
                select_cmd = string.Format("select id,busw,shijian,shuzi from cnbusw where id in (select distinct xid from cnbus where zhan = \"{0}\")", zhan);
            }


            if (MainPage.db != null)
            {
                DateTime start = DateTime.Now;
                try
                {
                    //List<Test> lst=new List<Test>();
                    //SQLiteCommand cmd = db.CreateCommand("SELECT xid FROM cnbus where zhan = \'青城桥\'");
                    SQLiteCommand cmd = MainPage.db.CreateCommand(select_cmd);
                    var lst = cmd.ExecuteQuery<BF_LINE>();

                    lbOutput.Text += "Selected " + lst.ToList().Count + " items\r\nTime " + (DateTime.Now - start).TotalSeconds;



                    List<string> s_ascending = new List<string>();
                    

                    int j = lst.ToList().Count;
                    foreach (BF_LINE temp in lst)
                    {
                        s_ascending.Add(temp.shijian);
                    }

                    lines_info_listbox.ItemsSource = lst;

                    set_title_name(lst.ToList().Count);

                    lbOutput.Text += "\r\nSelected " + lst.ToList().Count + " items\r\nTime " + (DateTime.Now - start).TotalSeconds;

                }
                catch (SQLiteException ex)
                {
                    
                }
            }




        }

        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}