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

// using sqlite client reference
using SQLiteClient;


namespace bus_finder
{
    public partial class MainPage : PhoneApplicationPage
    {
        // sqlite connection variable
        public static SQLiteConnection db = null;


        // Constructor
        public MainPage()
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



            line_autocompletebox.ItemsSource = cities;
            line_autocompletebox.ItemFilter += SearchCountry;

            line_autocompletebox.Visibility = System.Windows.Visibility.Visible;
            point_autocompletebox.Visibility = System.Windows.Visibility.Collapsed;
            point_start_autocompletebox.Visibility = System.Windows.Visibility.Collapsed;
            point_end_autocompletebox.Visibility = System.Windows.Visibility.Collapsed;

            //select_bug();

            init_database_data();


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
        


        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        // search the bus line 
        private void bus_line_search()
        {
            
            lbOutput.Text = "";
            // get the line number which you input

            string select_cmd = "";
            if (line_autocompletebox.Text != "")
            {
                select_cmd = string.Format("select id,busw,shijian,shuzi from cnbusw where busw = \"{0}\"", line_autocompletebox.Text);
            }

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
                        string uri = string.Format("/bus_finder;component/BusLine.xaml?id={0}&busw={1}&shijian={2}&shuzi={3}", temp.id, temp.busw, temp.shijian, temp.shuzi);
                        NavigationService.Navigate(new Uri(uri, UriKind.Relative));
                    }

                    


                    lbOutput.Text += "\r\nSelected " + lst.ToList().Count + " items\r\nTime " + (DateTime.Now - start).TotalSeconds;

                }
                catch (SQLiteException ex)
                {
                    lbOutput.Text += "Error: " + ex.Message;
                }
            }
        }

        // search the bus point
        private void bus_point_search()
        {
            if (point_autocompletebox.Text != "")
            {
                string uri = string.Format("/bus_finder;component/BusPoint.xaml?zhan={0}", point_autocompletebox.Text);
                NavigationService.Navigate(new Uri(uri, UriKind.Relative));
            }
        }

        // search the bus point 2 point
        private void bus_point_to_point_search()
        {
            // create the select cmd
            string select_cmd = "";
            string tag = "";

            if (point_start_autocompletebox.Text != "" && point_end_autocompletebox.Text != "")
            {

                select_cmd = string.Format("select id,busw,shijian,shuzi from cnbusw where id in (select xid from cnbus where zhan = \"{0}\" intersect select xid from cnbus where zhan = \"{1}\")", point_start_autocompletebox.Text, point_end_autocompletebox.Text);
                
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

                        // has a one direct line
                        if ((Application.Current as App).lst_p2p_lines.ToList().Count != 0)
                        {
                            tag = "one";
                            string uri = string.Format("/bus_finder;component/BusP2P.xaml?count={0}&start={1}&end={2}&tag={3}", (Application.Current as App).lst_p2p_lines.ToList().Count, point_start_autocompletebox.Text, point_end_autocompletebox.Text, tag);

                            NavigationService.Navigate(new Uri(uri, UriKind.Relative));
                        }
                        else
                        {
                            tag = "change";
                            // search the point you need to change the line
                            // create the change line select_cmd
                            select_cmd = string.Format("select distinct zhan from cnbus where xid in (select distinct xid from cnbus where zhan = \"{0}\") intersect select distinct zhan from cnbus where xid in (select distinct xid from cnbus where zhan = \"{1}\")", point_start_autocompletebox.Text, point_end_autocompletebox.Text);

                            // select the data
                            cmd = db.CreateCommand(select_cmd);
                            (Application.Current as App).lst_p2p_zhans = cmd.ExecuteQuery<Test>();

                            List<string> s2 = new List<string>();

                            foreach (Test temp1 in (Application.Current as App).lst_p2p_zhans)
                            {
                                s2.Add(temp1.zhan);
                            }


                            // uri to changed point line info
                            if ((Application.Current as App).lst_p2p_zhans.ToList().Count == 0)
                            {
                                tag = "no";
                            }

                            string uri = string.Format("/bus_finder;component/BusP2P.xaml?count={0}&start={1}&end={2}&tag={3}", (Application.Current as App).lst_p2p_zhans.ToList().Count, point_start_autocompletebox.Text, point_end_autocompletebox.Text, tag);

                            NavigationService.Navigate(new Uri(uri, UriKind.Relative));

                        }





                        lbOutput.Text += "\r\nSelected " + (Application.Current as App).lst_p2p_lines.ToList().Count + " items\r\nTime " + (DateTime.Now - start).TotalSeconds;

                    }
                    catch (SQLiteException ex)
                    {

                    }
                }

            }
        }


        //changed the app bar
        private void Panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (0 == bus_finder_panorama.SelectedIndex)
            {
                line_autocompletebox.Visibility  = System.Windows.Visibility.Visible;
                point_autocompletebox.Visibility = System.Windows.Visibility.Collapsed;
                point_start_autocompletebox.Visibility = System.Windows.Visibility.Collapsed;
                point_end_autocompletebox.Visibility = System.Windows.Visibility.Collapsed;
                this.line_autocompletebox_come_animation.Begin();
                
                //bus_finder_panorama.Title = string.Format("bus finder 0");
            }
            else if (1 == bus_finder_panorama.SelectedIndex)
            {
                line_autocompletebox.Visibility = System.Windows.Visibility.Collapsed;
                point_autocompletebox.Visibility = System.Windows.Visibility.Visible;
                point_start_autocompletebox.Visibility = System.Windows.Visibility.Collapsed;
                point_end_autocompletebox.Visibility = System.Windows.Visibility.Collapsed;
                
                //bus_finder_panorama.Title = string.Format("bus finder 1");
            }
            else if (2 == bus_finder_panorama.SelectedIndex)
            {
                line_autocompletebox.Visibility = System.Windows.Visibility.Collapsed;
                point_autocompletebox.Visibility = System.Windows.Visibility.Collapsed;
                point_start_autocompletebox.Visibility = System.Windows.Visibility.Visible;
                point_end_autocompletebox.Visibility = System.Windows.Visibility.Visible;
                //bus_finder_panorama.Title = string.Format("bus finder 2");
            }
            else
            {

                line_autocompletebox.Visibility = System.Windows.Visibility.Collapsed;
                point_autocompletebox.Visibility = System.Windows.Visibility.Collapsed;
                point_start_autocompletebox.Visibility = System.Windows.Visibility.Collapsed;
                point_end_autocompletebox.Visibility = System.Windows.Visibility.Collapsed;
                //bus_finder_panorama.Title = string.Format("bus finder 3");
            }
        }

        private void app_bar_search_click(object sender, EventArgs e)
        {
            if (0 == bus_finder_panorama.SelectedIndex)
            {
                bus_line_search();
            }
            else if (1 == bus_finder_panorama.SelectedIndex)
            {
                bus_point_search();
            }
            else if (2 == bus_finder_panorama.SelectedIndex)
            {
                bus_point_to_point_search();
            }
            else
            {
                this.SearchPerssedAnimation.Begin(); 
            }

                       
        }

        private void select_bug()
        {
            //if (OpenDB())   

            if (db == null)
            {
                //db = new SQLiteConnection("dujiangyan_index");
                db = new SQLiteConnection("dujiangyan");
                db.Open();
            }

            if (db != null)
            {
                DateTime start = DateTime.Now;
                try
                {


                    //SQLiteCommand cmd = db.CreateCommand("select id,busw,shijian,shuzi from cnbusw where id in (select distinct xid from cnbus where zhan = \"405医院\")");
                    SQLiteCommand cmd = db.CreateCommand("select id,busw,shijian,shuzi from cnbusw where id in (select distinct xid from cnbus where zhan = \"电信大楼\")");
                    //SQLiteCommand cmd = db.CreateCommand("select xid from cnbus where zhan = \"观风小区\" intersect select xid from cnbus where zhan = \"电信大楼\"");



                    var lst = cmd.ExecuteQuery<BF_LINE>();

                    List<string> s1 = new List<string>();

                    foreach (BF_LINE temp1 in lst)
                    {
                        s1.Add(temp1.shijian);
                    }

                    //var lst = cmd.ExecuteQuery<Test>();

                    lbOutput.Text += "Selected " + lst.ToList().Count + " items\r\nTime " + (DateTime.Now - start).TotalSeconds;
                }
                catch (SQLiteException ex)
                {
                    lbOutput.Text += "Error: " + ex.Message;
                }
            }

        }

        // initial the database data for quickly search the data
        private void init_database_data()
        {
            if (db == null)
            {
                //db = new SQLiteConnection("dujiangyan_index");
                db = new SQLiteConnection("dujiangyan");
                db.Open();
            }

            string select_cmd0 = "select zhan from cnbus where xid = 9 and kind = 1 order by pm";
            string select_cmd1 = "select id,busw,shijian,shuzi from cnbusw where busw = \"1路\"";
            string select_cmd2 = "select distinct zhan from cnbus";
            string select_cmd3 = "select id,busw,shijian,shuzi from cnbusw where id in (select distinct xid from cnbus where zhan = \"405医院\")";
            string select_cmd4 = "select zhan from cnbus where xid = 2 intersect select  zhan from cnbus where xid = 1";
            string select_cmd5 = "select id,busw,shijian,shuzi from cnbusw where id in (select distinct xid from cnbus where zhan = \"青城桥\")";
            string select_cmd6 = "select distinct zhan from cnbus where xid in (select distinct xid from cnbus where zhan = \"青城桥\") intersect select distinct zhan from cnbus where xid in (select distinct xid from cnbus where zhan = \"灌口镇政府\")";


            if (db != null)
            {
                DateTime start = DateTime.Now;
                try
                {
                    //List<Test> lst=new List<Test>();
                    //SQLiteCommand cmd = db.CreateCommand("SELECT xid FROM cnbus where zhan = \'青城桥\'");
                    SQLiteCommand cmd = db.CreateCommand(select_cmd0);
                    var lst0 = cmd.ExecuteQuery<Test>();
                    cmd = db.CreateCommand(select_cmd6);
                    var lst6 = cmd.ExecuteQuery<Test>();
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

                    List<string> s2 = new List<string>();

                    foreach (Test temp in lst6)
                    {
                        s2.Add(temp.zhan);
                    }




                    List<string> s = new List<string>();

                    foreach (Test temp in lst2)
                    {
                        s.Add(temp.zhan);
                    }


                    point_autocompletebox.ItemsSource = s;
                    point_autocompletebox.ItemFilter += SearchCountry;

                    point_start_autocompletebox.ItemsSource = s;
                    point_start_autocompletebox.ItemFilter += SearchCountry;
                    point_end_autocompletebox.ItemsSource = s;
                    point_end_autocompletebox.ItemFilter += SearchCountry;

                    List<string> s1 = new List<string>();

                    foreach (BF_LINE temp1 in lst5)
                    {
                        s1.Add(temp1.shijian);
                    }


                    //lstResult.ItemsSource = s;

                    lbOutput.Text += "\r\nSelected " + lst2.ToList().Count + " items\r\nTime " + (DateTime.Now - start).TotalSeconds;

                }
                catch (SQLiteException ex)
                {
                    lbOutput.Text += "Error: " + ex.Message;
                }
            }
        }
    }
}