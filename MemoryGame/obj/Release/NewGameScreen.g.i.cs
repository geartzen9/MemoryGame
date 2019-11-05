﻿#pragma checksum "..\..\NewGameScreen.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "69D7C28E23F8B3C895D438AD215ED4DEDA0AD4EB4C8259DDBD39B4B22E45D2E1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MemoryGame;
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


namespace MemoryGame {
    
    
    /// <summary>
    /// NewGameScreen
    /// </summary>
    public partial class NewGameScreen : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\NewGameScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MemoryGame.NewGameScreen page;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\NewGameScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button backButton;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\NewGameScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox InputP1;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\NewGameScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Moeilijkheidsgraad;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\NewGameScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox InputP2;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\NewGameScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup StandardPopup;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\NewGameScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox themeInput;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\NewGameScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StartButton;
        
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
            System.Uri resourceLocater = new System.Uri("/MemoryGame;component/newgamescreen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\NewGameScreen.xaml"
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
            this.page = ((MemoryGame.NewGameScreen)(target));
            return;
            case 2:
            this.backButton = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\NewGameScreen.xaml"
            this.backButton.Click += new System.Windows.RoutedEventHandler(this.BackButtonClick);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 34 "..\..\NewGameScreen.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowPopupOffsetClicked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.InputP1 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.Moeilijkheidsgraad = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.InputP2 = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.StandardPopup = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 8:
            this.themeInput = ((System.Windows.Controls.ComboBox)(target));
            
            #line 59 "..\..\NewGameScreen.xaml"
            this.themeInput.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 59 "..\..\NewGameScreen.xaml"
            this.themeInput.Loaded += new System.Windows.RoutedEventHandler(this.ComboBox_Loaded);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 79 "..\..\NewGameScreen.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ClosePopupClicked);
            
            #line default
            #line hidden
            return;
            case 10:
            this.StartButton = ((System.Windows.Controls.Button)(target));
            
            #line 85 "..\..\NewGameScreen.xaml"
            this.StartButton.Click += new System.Windows.RoutedEventHandler(this.startButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
