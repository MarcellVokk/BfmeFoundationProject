﻿#pragma checksum "..\..\..\..\..\Elements\Generic\Acrylic.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BC6B188531C7D8E5DC9D53E12818A8B517435719"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AllInOneLauncher.Elements;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace AllInOneLauncher.Elements.Generic {
    
    
    /// <summary>
    /// Acrylic
    /// </summary>
    public partial class Acrylic : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\..\..\Elements\Generic\Acrylic.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal AllInOneLauncher.Elements.Generic.Acrylic userControl;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\..\..\Elements\Generic\Acrylic.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image image;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AllInOneLauncher;V2.0.2.0;component/elements/generic/acrylic.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Elements\Generic\Acrylic.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.userControl = ((AllInOneLauncher.Elements.Generic.Acrylic)(target));
            
            #line 8 "..\..\..\..\..\Elements\Generic\Acrylic.xaml"
            this.userControl.LayoutUpdated += new System.EventHandler(this.OnLayoutUpdated);
            
            #line default
            #line hidden
            
            #line 8 "..\..\..\..\..\Elements\Generic\Acrylic.xaml"
            this.userControl.IsVisibleChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.OnVisibilityChanged);
            
            #line default
            #line hidden
            
            #line 8 "..\..\..\..\..\Elements\Generic\Acrylic.xaml"
            this.userControl.Loaded += new System.Windows.RoutedEventHandler(this.OnLoad);
            
            #line default
            #line hidden
            return;
            case 2:
            this.image = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

