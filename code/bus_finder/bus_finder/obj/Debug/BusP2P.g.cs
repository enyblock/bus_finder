﻿#pragma checksum "C:\Users\Administrator\bus_finder\code\bus_finder\bus_finder\BusP2P.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B9D5707D11F77C94C4F47AA6CAFC2463"
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
    
    
    public partial class BusP2P : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Controls.Panorama bus_finder_panorama;
        
        internal Microsoft.Phone.Controls.PanoramaItem ascending_lines_info_panoramaitem;
        
        internal System.Windows.Controls.ListBox ascending_lines_info_listbox;
        
        internal System.Windows.Controls.ListBox ascending_lines_info_listbox_changed_line;
        
        internal Microsoft.Phone.Controls.PanoramaItem descending_lines_info_panoramaitem;
        
        internal System.Windows.Controls.ListBox descending_lines_info_listbox;
        
        internal System.Windows.Controls.ListBox descending_lines_info_listbox_changed_line;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/bus_finder;component/BusP2P.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.bus_finder_panorama = ((Microsoft.Phone.Controls.Panorama)(this.FindName("bus_finder_panorama")));
            this.ascending_lines_info_panoramaitem = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("ascending_lines_info_panoramaitem")));
            this.ascending_lines_info_listbox = ((System.Windows.Controls.ListBox)(this.FindName("ascending_lines_info_listbox")));
            this.ascending_lines_info_listbox_changed_line = ((System.Windows.Controls.ListBox)(this.FindName("ascending_lines_info_listbox_changed_line")));
            this.descending_lines_info_panoramaitem = ((Microsoft.Phone.Controls.PanoramaItem)(this.FindName("descending_lines_info_panoramaitem")));
            this.descending_lines_info_listbox = ((System.Windows.Controls.ListBox)(this.FindName("descending_lines_info_listbox")));
            this.descending_lines_info_listbox_changed_line = ((System.Windows.Controls.ListBox)(this.FindName("descending_lines_info_listbox_changed_line")));
            this.lbOutput = ((System.Windows.Controls.TextBlock)(this.FindName("lbOutput")));
        }
    }
}

