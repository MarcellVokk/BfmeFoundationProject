﻿#pragma checksum "..\..\..\..\..\..\Elements\Editors\WorkshopEntryScreenshotLibrary.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "600B1EFD12FCD74CB8AFCE135FF9501794718EF2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BfmeFoundationProject.WorkshopStudio.Elements;
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


namespace BfmeFoundationProject.WorkshopStudio.Elements {
    
    
    /// <summary>
    /// WorkshopEntryScreenshotLibrary
    /// </summary>
    public partial class WorkshopEntryScreenshotLibrary : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\..\..\..\Elements\Editors\WorkshopEntryScreenshotLibrary.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scrollViewer;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\..\..\..\Elements\Editors\WorkshopEntryScreenshotLibrary.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel images;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\..\..\..\Elements\Editors\WorkshopEntryScreenshotLibrary.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid placeholder;
        
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
            System.Uri resourceLocater = new System.Uri(("/BfmeFoundationProject_WorkshopStudio;component/elements/editors/workshopentryscr" +
                    "eenshotlibrary.xaml"), System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Elements\Editors\WorkshopEntryScreenshotLibrary.xaml"
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
            
            #line 8 "..\..\..\..\..\..\Elements\Editors\WorkshopEntryScreenshotLibrary.xaml"
            ((BfmeFoundationProject.WorkshopStudio.Elements.WorkshopEntryScreenshotLibrary)(target)).Drop += new System.Windows.DragEventHandler(this.OnFileDrop);
            
            #line default
            #line hidden
            
            #line 8 "..\..\..\..\..\..\Elements\Editors\WorkshopEntryScreenshotLibrary.xaml"
            ((BfmeFoundationProject.WorkshopStudio.Elements.WorkshopEntryScreenshotLibrary)(target)).DragOver += new System.Windows.DragEventHandler(this.OnFileDragOver);
            
            #line default
            #line hidden
            return;
            case 2:
            this.scrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 3:
            this.images = ((System.Windows.Controls.WrapPanel)(target));
            return;
            case 4:
            this.placeholder = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

