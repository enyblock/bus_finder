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
    public partial class BusP2P : PhoneApplicationPage
    {

        public string start_point = "";
        public string end_point = "";
        public string lines_count = "";

        public BusP2P()
        {
            InitializeComponent();
        }

        // set the title name 
        private void set_title_name(string _tag)
        {



            if (NavigationContext.QueryString.TryGetValue("start", out start_point) && NavigationContext.QueryString.TryGetValue("end", out end_point) && NavigationContext.QueryString.TryGetValue("count", out lines_count))
            {
                if (_tag == "one")
                {
                    ascending_lines_info_panoramaitem.Header = string.Format("经过\"{0}\"\n到\"{1}\"\n的线路有{2}条:", start_point, end_point, lines_count);
                    descending_lines_info_panoramaitem.Header = string.Format("从\"{1}\"\n返回\"{0}\"\n的线路有{2}条:", start_point, end_point, lines_count);
                }
                else if (_tag == "change")
                {
                    ascending_lines_info_panoramaitem.Header = string.Format("经过\"{0}\"\n到\"{1}\"\n的直达线路不存在\n您可选择的换乘的方案有{2}种:", start_point, end_point, lines_count);
                    descending_lines_info_panoramaitem.Header = string.Format("从\"{1}\"\n返回\"{0}\"\n的直达线路不存在\n您选择的可换乘的方案有{2}种:", start_point, end_point, lines_count);
                }
                else
                {
                    ascending_lines_info_panoramaitem.Header = string.Format("经过\"{0}\"\n到\"{1}\"\n的直达线路不存在,\n换乘一次的方案也不存在。\n\n很抱歉！！！", start_point, end_point);
                    descending_lines_info_panoramaitem.Header = string.Format("从\"{1}\"\n返回\"{0}\"\n的直达线路不存在,\n换乘一次的方案也不存在。\n\n很抱歉！！！", start_point, end_point);
                }

            }
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

            string tag = get_tag();

            set_title_name(tag);

            if (tag == "one")
            {
                ascending_lines_info_listbox.Visibility = System.Windows.Visibility.Visible;
                descending_lines_info_listbox.Visibility = System.Windows.Visibility.Visible;
                ascending_lines_info_listbox_changed_line.Visibility = System.Windows.Visibility.Collapsed;
                descending_lines_info_listbox_changed_line.Visibility = System.Windows.Visibility.Collapsed;

                one_line_direct_p2p();
            }
            else if (tag == "change")
            {

                ascending_lines_info_listbox.Visibility = System.Windows.Visibility.Collapsed;
                descending_lines_info_listbox.Visibility = System.Windows.Visibility.Collapsed;
                ascending_lines_info_listbox_changed_line.Visibility = System.Windows.Visibility.Visible;
                descending_lines_info_listbox_changed_line.Visibility = System.Windows.Visibility.Visible;

                changed_line_p2p();
            }
            else
            {
                ;
            }

        }


        // one changed line
        private void changed_line_p2p()
        {
            List<string> s_ascending = new List<string>();
            List<string> s_descending = new List<string>();
            

            foreach (Test temp in (Application.Current as App).lst_p2p_zhans)
            {
                string temp_asc = "";
                string temp_des = "";

                get_changed_info(temp.zhan, out temp_asc, out temp_des);

                s_ascending.Add(temp_asc);
                s_descending.Add(temp_des);

            }


            
            ascending_lines_info_listbox_changed_line.ItemsSource = s_ascending;
            descending_lines_info_listbox_changed_line.ItemsSource = s_descending;


        }


        // get the chenged info

        private void get_changed_info(string between_point, out string s_asc, out string s_des)
        {
            string select_cmd = "";
            string first_lines = "";
            string second_lines = "";

            select_cmd = string.Format("select id,busw,shijian,shuzi from cnbusw where id in (select xid from cnbus where zhan = \"{0}\" intersect select xid from cnbus where zhan = \"{1}\")", start_point, between_point);

            if (MainPage.db != null)
            {
                DateTime start = DateTime.Now;
                try
                {

                    SQLiteCommand cmd = MainPage.db.CreateCommand(select_cmd);
                    var lst = cmd.ExecuteQuery<BF_LINE>();

                    List<string> s1 = new List<string>();

                    foreach (BF_LINE temp1 in lst)
                    {
                        first_lines += "\"" + temp1.busw + "\",";
                    }
                }
                catch (SQLiteException ex)
                {

                }

                first_lines = first_lines.Substring(0, first_lines.Length-1);
            }

            select_cmd = string.Format("select id,busw,shijian,shuzi from cnbusw where id in (select xid from cnbus where zhan = \"{0}\" intersect select xid from cnbus where zhan = \"{1}\")", between_point, end_point);

            if (MainPage.db != null)
            {
                DateTime start = DateTime.Now;
                try
                {

                    SQLiteCommand cmd = MainPage.db.CreateCommand(select_cmd);
                    var lst = cmd.ExecuteQuery<BF_LINE>();

                    List<string> s1 = new List<string>();

                    foreach (BF_LINE temp1 in lst)
                    {
                        second_lines += "\"" + temp1.busw + "\",";
                    }
                }
                catch (SQLiteException ex)
                {

                }

                second_lines = second_lines.Substring(0, second_lines.Length-1);
            }

            s_asc = string.Format("您可以乘坐{0}到\"{1}\"站下车,然后换乘{2}到\"{3}\"站", first_lines, between_point, second_lines,end_point);
            s_des = string.Format("您可以乘坐{0}到\"{1}\"站下车,然后换乘{2}到\"{3}\"站", second_lines, between_point, first_lines, start_point);

        }


        // one line direct p2p
        private void one_line_direct_p2p()
        {
            List<string> s_ascending = new List<string>();

            int j = (Application.Current as App).lst_p2p_lines.ToList().Count;
            foreach (BF_LINE temp in (Application.Current as App).lst_p2p_lines)
            {
                s_ascending.Add(temp.shijian);
            }

            ascending_lines_info_listbox.ItemsSource = (Application.Current as App).lst_p2p_lines;
            descending_lines_info_listbox.ItemsSource = (Application.Current as App).lst_p2p_lines;
        }


        // judge the tag type to decide the 
        private string get_tag()
        {
            string tmp_tag = "";

            if (NavigationContext.QueryString.TryGetValue("tag", out tmp_tag))
            {
                return tmp_tag;
            }

            return tmp_tag;
        }

        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}