﻿#pragma checksum "..\..\IPSettingDlg.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "71ADCC1A9DCDD6CAAD1ABEF66F8EC1B6919B26792BF451A786FF1B6F53F25E5A"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TaderProject;


namespace TaderProject {
    
    
    /// <summary>
    /// IPSettingDlg
    /// </summary>
    public partial class IPSettingDlg : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 72 "..\..\IPSettingDlg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ToMiniButton;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\IPSettingDlg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CloseButton;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\IPSettingDlg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox IPSettingTB;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\IPSettingDlg.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button IPSettingBtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TaderProject;component/ipsettingdlg.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\IPSettingDlg.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 64 "..\..\IPSettingDlg.xaml"
            ((System.Windows.Controls.Canvas)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 67 "..\..\IPSettingDlg.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ToMiniButton = ((System.Windows.Controls.Button)(target));
            
            #line 73 "..\..\IPSettingDlg.xaml"
            this.ToMiniButton.Click += new System.Windows.RoutedEventHandler(this.ToMiniButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.CloseButton = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\IPSettingDlg.xaml"
            this.CloseButton.Click += new System.Windows.RoutedEventHandler(this.CloseButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.IPSettingTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.IPSettingBtn = ((System.Windows.Controls.Button)(target));
            
            #line 113 "..\..\IPSettingDlg.xaml"
            this.IPSettingBtn.Click += new System.Windows.RoutedEventHandler(this.IPSettingBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

