﻿#pragma checksum "C:\Users\Administrator\bus_finder\code\bus_finder\bus_finder\BusLine.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3453BADB3FCDA5A1CD98BEF35CB22AD7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace bus_finder {
    
    
    public partial class BusLine : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Media.Animation.Storyboard busline_page_come_animation;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Panorama bus_finder_panorama;
        
        internal Microsoft.Phone.Controls.PanoramaItem ascending_panoramaitem;
        
        internal System.Windows.Controls.ListBox ascending_listbox;
        
        internal System.Windows.Controls.TextBlock ascending_line_shijian_textblock;
        
        internal System.Windows.Controls.TextBlock ascending_textblock;
        
        internal Microsoft.Phone.Controls.PanoramaItem descending_panoramaitem;
        
        internal System.Windows.Controls.ListBox descending_listbox;
        
        internal System.Windows.Controls.TextBlock descending_line_shijian_textblock;
        
        internal System.Windows.Controls.TextBlock descending_textblock;
        
        internal System.Windows.Controls.TextBlock lbOutput;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/bus_finder;component/BusLine.xaml", System.UriKind.Relative));
            this.busline_page_come_animation = ((System.Windows.Media.Animation.Storyboard)(this.FindName("busline_page_come_animation")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.bus_finder_panorama = ((Microsoft.Phone.Controls.Panorama)(this.FindName("bus_finder_panorama")));
            this.ascending_panoramaitem = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("ascending_panoramaitem")));
            this.ascending_listbox = ((System.Windows.Controls.ListBox)(this.FindName("ascending_listbox")));
            this.ascending_line_shijian_textblock = ((System.Windows.Controls.TextBlock)(this.FindName("ascending_line_shijian_textblock")));
            this.ascending_textblock = ((System.Windows.Controls.TextBlock)(this.FindName("ascending_textblock")));
            this.descending_panoramaitem = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("descending_panoramaitem")));
            this.descending_listbox = ((System.Windows.Controls.ListBox)(this.FindName("descending_listbox")));
            this.descending_line_shijian_textblock = ((System.Windows.Controls.TextBlock)(this.FindName("descending_line_shijian_textblock")));
            this.descending_textblock = ((System.Windows.Controls.TextBlock)(this.FindName("descending_textblock")));
            this.lbOutput = ((System.Windows.Controls.TextBlock)(this.FindName("lbOutput")));
        }
    }
}

