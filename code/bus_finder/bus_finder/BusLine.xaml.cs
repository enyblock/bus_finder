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
    public partial class BusLine : PhoneApplicationPage
    {

        

        public BusLine()
        {
            InitializeComponent();
        }

        private void set_panoramaitem_header()
        {
            string busw = "";

            if (NavigationContext.QueryString.TryGetValue("busw", out busw))
            {
                ascending_panoramaitem.Header = busw;
                descending_panoramaitem.Header = busw;
            }
             
        }
        

        private void set_line_shijian()
        {
            string shijian = "";

            if (NavigationContext.QueryString.TryGetValue("shijian", out shijian))
            {
                string ascending_temp_shijian = "";
                string descending_temp_shijian = "";

                string[] str_split = shijian.Split(new char[2] { '-', ' ' });
                ascending_temp_shijian  = string.Format("{0}--{1}\n{2}-{3}", str_split[1], str_split[0], str_split[2], str_split[3]);
                descending_temp_shijian = string.Format("{0}--{1}\n{2}-{3}", str_split[0], str_split[1], str_split[2], str_split[3]);
                
                ascending_line_shijian_textblock.Text = ascending_temp_shijian;
                descending_line_shijian_textblock.Text = descending_temp_shijian;
            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.busline_page_come_animation.Begin();

            set_panoramaitem_header();

            set_line_shijian();

            string shijian = "";
            if (NavigationContext.QueryString.TryGetValue("shijian", out shijian))
            {
                //ascending_listbox.Items.Add(shijian);
            }

            string xid = "";

            if (NavigationContext.QueryString.TryGetValue("id", out xid))
            {
                //ascending_listbox.Items.Add(xid);
            }

            string select_cmd = "";

            select_cmd = string.Format("select zhan from cnbus where xid = {0} and kind = 1 order by pm", xid);


            if (MainPage.db != null)
            {
                DateTime start = DateTime.Now;
                try
                {
                    //List<Test> lst=new List<Test>();
                    //SQLiteCommand cmd = db.CreateCommand("SELECT xid FROM cnbus where zhan = \'青城桥\'");
                    SQLiteCommand cmd = MainPage.db.CreateCommand(select_cmd);
                    var lst = cmd.ExecuteQuery<Test>();

                    lbOutput.Text += "Selected " + lst.ToList().Count + " items\r\nTime " + (DateTime.Now - start).TotalSeconds;



                    List<string> s_ascending = new List<string>();
                    List<string> s_descending = new List<string>();

                    int i = 0;
                    int j = lst.ToList().Count;
                    foreach (Test temp in lst)
                    {
                        s_ascending.Add(string.Format("{0:00}  {1}", i + 1, temp.zhan));
                        s_descending.Add(string.Format("{0:00}  {1}", j, temp.zhan));

                        i++;
                        j--;
                        
                    }

                    s_descending.Reverse();

                    ascending_listbox.ItemsSource = s_ascending;
                    descending_listbox.ItemsSource = s_descending;

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