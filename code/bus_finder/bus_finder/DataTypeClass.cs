using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace bus_finder
{
    public class DataTypeClass
    {

    }


    // define the BF_LIND data type
    public class BF_LINE
    {
        // constructor function 
        public BF_LINE()
        {
        }

        // bus index
        int _id;
        public int id { get { return _id; } set { _id = value; } }

        // bus name
        string _busw;
        public string busw { get { return _busw; } set { _busw = value; } }

        // bus time
        string _shijian;
        public string shijian { get { return _shijian; } set { _shijian = value; } }

        // bus name including num
        int _shuzi;
        public int shuzi { get { return _shuzi; } set { _shuzi = value; } }

    }

    // initial database test data
    public class Test
    {
        public Test()
        {
        }

        string _zhan;
        public string zhan { get { return _zhan; } set { _zhan = value; } }
    }

    public class TestXID
    {
        public TestXID()
        {
        }

        int _xid;
        public int xid { get { return _xid; } set { _xid = value; } }
    }

}
