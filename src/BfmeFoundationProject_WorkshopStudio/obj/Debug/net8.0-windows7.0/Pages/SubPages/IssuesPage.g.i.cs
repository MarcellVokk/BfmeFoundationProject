﻿#pragma checksum "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0AAB7F0D47C14117D5FF41E0C90B0B3CB0E679C9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using BfmeFoundationProject.MissionControl.Elements;
using BfmeFoundationProject.MissionControl.Pages.SubPages;
using BfmeFoundationProject.SharedUi.Elements;
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


namespace BfmeFoundationProject.MissionControl.Pages.SubPages {
    
    
    /// <summary>
    /// IssuesPage
    /// </summary>
    public partial class IssuesPage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button clearIssuesButton;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button refresh;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox search;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button filterButton;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid filters;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal BfmeFoundationProject.SharedUi.Elements.LabeledCheckbox onlyTimeouts;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal BfmeFoundationProject.SharedUi.Elements.LabeledCheckbox onlyDisconnects;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal BfmeFoundationProject.SharedUi.Elements.LabeledCheckbox onlyClientErrors;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal BfmeFoundationProject.SharedUi.Elements.LabeledCheckbox onlyServerErrors;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal BfmeFoundationProject.SharedUi.Elements.VirtualList issues;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid info;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal BfmeFoundationProject.MissionControl.Elements.IssueInfoEditor issueInfoEditor;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BfmeFoundationProject_WorkshopStudio;component/pages/subpages/issuespage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.clearIssuesButton = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
            this.clearIssuesButton.Click += new System.Windows.RoutedEventHandler(this.OnClearIssues);
            
            #line default
            #line hidden
            return;
            case 2:
            this.refresh = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
            this.refresh.Click += new System.Windows.RoutedEventHandler(this.OnReload);
            
            #line default
            #line hidden
            return;
            case 3:
            this.search = ((System.Windows.Controls.TextBox)(target));
            
            #line 39 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
            this.search.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.OnSearchFilterChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.filterButton = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
            this.filterButton.Click += new System.Windows.RoutedEventHandler(this.FilterClicked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.filters = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.onlyTimeouts = ((BfmeFoundationProject.SharedUi.Elements.LabeledCheckbox)(target));
            
            #line 48 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
            this.onlyTimeouts.IsCheckedChanged += new System.EventHandler(this.OnFiltersChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.onlyDisconnects = ((BfmeFoundationProject.SharedUi.Elements.LabeledCheckbox)(target));
            
            #line 49 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
            this.onlyDisconnects.IsCheckedChanged += new System.EventHandler(this.OnFiltersChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.onlyClientErrors = ((BfmeFoundationProject.SharedUi.Elements.LabeledCheckbox)(target));
            
            #line 50 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
            this.onlyClientErrors.IsCheckedChanged += new System.EventHandler(this.OnFiltersChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.onlyServerErrors = ((BfmeFoundationProject.SharedUi.Elements.LabeledCheckbox)(target));
            
            #line 51 "..\..\..\..\..\Pages\SubPages\IssuesPage.xaml"
            this.onlyServerErrors.IsCheckedChanged += new System.EventHandler(this.OnFiltersChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.issues = ((BfmeFoundationProject.SharedUi.Elements.VirtualList)(target));
            return;
            case 11:
            this.info = ((System.Windows.Controls.Grid)(target));
            return;
            case 12:
            this.issueInfoEditor = ((BfmeFoundationProject.MissionControl.Elements.IssueInfoEditor)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

