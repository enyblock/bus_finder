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
using Community.CsharpSqlite;



namespace bus_finder
{
    // initial database test data

    public partial class sqlite_test : PhoneApplicationPage
    {

        public static SQLiteConnection db = null;

        public sqlite_test()
        {
            InitializeComponent();

            List<string> cities = new List<string>();
            cities.Add("1路");
            cities.Add("2路");
            cities.Add("3路");
            cities.Add("4路");
            cities.Add("5路");
            cities.Add("6路");
            cities.Add("7路");
            cities.Add("10路");
            cities.Add("11路");
            cities.Add("12路");
            cities.Add("101路");
            cities.Add("101路A");
            cities.Add("102路");
            cities.Add("201路");



            line_textbox.ItemsSource = cities;
            line_textbox.ItemFilter += SearchCountry;


        }            

        

        private bool SearchCountry(string search, object value)
        {
            if (value != null)
            {
                //如果包含了搜索的字符串则返回true
                if (value.ToString().ToLower().IndexOf(search) >= 0)
                    return true;
            }
            
            // 如果不匹配 返回false
            return false;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {


            if (db == null)
            {
                //db = new SQLiteConnection("dujiangyan_index");
                db = new SQLiteConnection("dujiangyan");
                db.Open();
            }

            string select_cmd0 = "select zhan from cnbus where xid = 9 and kind = 1 order by pm";
            string select_cmd1 = "select id,busw,shijian,shuzi from cnbusw where busw = \'1路\'";
            string select_cmd2 = "select distinct zhan from cnbus";
            string select_cmd3 = "select id,busw,shijian,shuzi from cnbusw where id in (select distinct xid from cnbus where zhan = \'405医院\')";
            string select_cmd4 = "select zhan from cnbus where xid = 2 intersect select  zhan from cnbus where xid = 1";
            string select_cmd5 = "select id,busw,shijian,shuzi from cnbusw where id in (select distinct xid from cnbus where zhan = \'青城桥\')";
            //string select_cmd6 = "select xid from cnbus where zhan = \"电信大楼\"";


            if (db != null)
            {
                DateTime start = DateTime.Now;
                try
                {
                    //List<Test> lst=new List<Test>();
                    //SQLiteCommand cmd = db.CreateCommand("SELECT xid FROM cnbus where zhan = \'青城桥\'");
                    SQLiteCommand cmd = db.CreateCommand(select_cmd0);
                    var lst0 = cmd.ExecuteQuery<Test>();
                    //cmd = db.CreateCommand(select_cmd6);
                    //var lst6 = cmd.ExecuteQuery<TestXID>();
                    cmd = db.CreateCommand(select_cmd1);
                    var lst1 = cmd.ExecuteQuery<BF_LINE>();
                    cmd = db.CreateCommand(select_cmd2);
                    var lst2 = cmd.ExecuteQuery<Test>();
                    cmd = db.CreateCommand(select_cmd3);
                    var lst3 = cmd.ExecuteQuery<BF_LINE>();
                    cmd = db.CreateCommand(select_cmd4);
                    var lst4 = cmd.ExecuteQuery<Test>();
                    cmd = db.CreateCommand(select_cmd5);
                    var lst5 = cmd.ExecuteQuery<BF_LINE>();



                    //lbOutput.Text += "Selected " + lst.ToList().Count + " items\r\nTime " + (DateTime.Now - start).TotalSeconds;

                    //List<int> s2 = new List<int>();
                    //foreach (TestXID temp in lst6)
                    //{
                    //    s2.Add(temp.xid);
                    //}




                    List<string> s = new List<string>();

                    foreach (Test temp in lst2)
                    {
                        s.Add(temp.zhan);
                    }

                    point_textbox.ItemsSource = s;
                    point_textbox.ItemFilter += SearchCountry;
                    point_start_textbox.ItemsSource = s;
                    point_start_textbox.ItemFilter += SearchCountry;
                    point_end_textbox.ItemsSource = s;
                    point_end_textbox.ItemFilter += SearchCountry;

                    List<string> s1 = new List<string>();

                    foreach (BF_LINE temp1 in lst5)
                    {
                        s1.Add(temp1.shijian);
                    }
                    

                    //lstResult.ItemsSource = s;

                    //lbOutput.Text += "\r\nSelected " + lst.ToList().Count + " items\r\nTime " + (DateTime.Now - start).TotalSeconds;

                }
                catch (SQLiteException ex)
                {
                    lbOutput.Text += "Error: " + ex.Message;
                }
            }

            lstResult.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            
            lstResult.Visibility = System.Windows.Visibility.Visible;
            lbOutput.Text = "";
            // get the line number which you input

            string select_cmd = "";
            if (line_textbox.Text != "")
            {
                select_cmd = string.Format("select id,busw,shijian,shuzi from cnbusw where busw = \'{0}\'", line_textbox.Text);
            }


            // create the lst

            // get the busw and shijian  return xid

            // using xid find the point list

            //string select_cmd = "SELECT zhan FROM cnbus where xid = ";

            //if (line_textbox.Text != "")
            //{
            //    select_cmd += line_textbox.Text;
            //    select_cmd += " and kind = 1 order by pm";
            //}


            //get the data


            if (db != null)
            {
                DateTime start = DateTime.Now;
                try
                {
                    //List<Test> lst=new List<Test>();
                    //SQLiteCommand cmd = db.CreateCommand("SELECT xid FROM cnbus where zhan = \'青城桥\'");
                    SQLiteCommand cmd = db.CreateCommand(select_cmd);
                    var lst = cmd.ExecuteQuery<BF_LINE>();

                    lbOutput.Text += "Selected " + lst.ToList().Count + " items\r\nTime " + (DateTime.Now - start).TotalSeconds;



                    List<string> s = new List<string>();

                    foreach (BF_LINE temp in lst)
                    {
                        s.Add(temp.shijian);
                        string uri = string.Format("/bus_finder;component/BusLine.xaml?id={0}&busw={1}&shijian={2}&shuzi={3}",temp.id,temp.busw,temp.shijian,temp.shuzi);
                        NavigationService.Navigate(new Uri(uri, UriKind.Relative));
                    }

                    lstResult.ItemsSource = s;


                    lbOutput.Text += "\r\nSelected " + lst.ToList().Count + " items\r\nTime " + (DateTime.Now - start).TotalSeconds;

                }
                catch (SQLiteException ex)
                {

                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            // create autocompletebox data
            // in load function 

            // navigate the BusPoint (point_name)

            if (point_textbox.Text != "")
            {
                string uri = string.Format("/bus_finder;component/BusPoint.xaml?zhan={0}", point_textbox.Text);
                NavigationService.Navigate(new Uri(uri, UriKind.Relative));
            }
            

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (point_start_textbox.Text != "" && point_end_textbox.Text != "")
            {
                // create the select cmd
                string select_cmd = "";
                select_cmd = string.Format("select id,busw,shijian,shuzi from cnbusw where id in (select xid from cnbus where zhan = \'{0}\' intersect select xid from cnbus where zhan = \'{1}\')", point_start_textbox.Text, point_end_textbox.Text);

                // select the data
                if (db != null)
                {
                    DateTime start = DateTime.Now;
                    try
                    {
                        //List<Test> lst=new List<Test>();
                        //List<BF_LINE> lst = new List<BF_LINE>();
                        //SQLiteCommand cmd = db.CreateCommand("SELECT xid FROM cnbus where zhan = \'青城桥\'");
                        SQLiteCommand cmd = db.CreateCommand(select_cmd);
                        //var lst = cmd.ExecuteQuery<BF_LINE>();

                        (Application.Current as App).lst_p2p_lines = cmd.ExecuteQuery<BF_LINE>();

                        lbOutput.Text += "Selected " + (Application.Current as App).lst_p2p_lines.ToList().Count + " items\r\nTime " + (DateTime.Now - start).TotalSeconds;

                        List<string> s1 = new List<string>();

                        foreach (BF_LINE temp1 in (Application.Current as App).lst_p2p_lines)
                        {
                            s1.Add(temp1.shijian);
                        }

                        string uri = string.Format("/bus_finder;component/BusP2P.xaml?count={0}&start={1}&end={2}", (Application.Current as App).lst_p2p_lines.ToList().Count, point_start_textbox.Text, point_end_textbox.Text);
                        NavigationService.Navigate(new Uri(uri, UriKind.Relative));
                     

                        lbOutput.Text += "\r\nSelected " + (Application.Current as App).lst_p2p_lines.ToList().Count + " items\r\nTime " + (DateTime.Now - start).TotalSeconds;

                    }
                    catch (SQLiteException ex)
                    {

                    }
                }

            }
        }
    }
}