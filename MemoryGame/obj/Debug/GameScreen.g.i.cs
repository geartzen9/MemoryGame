<<<<<<< HEAD
﻿#pragma checksum "..\..\GameScreen.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "82141C85FB08F413FDF34EA96DDF9F748A1E62C71A820E189181B20AEFD424A5"
=======
﻿#pragma checksum "..\..\GameScreen.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "092FD35BB5971867CBE36388CE8E9B0EF57DD7F33FCBFB921A635641FD9ACC7D"
>>>>>>> master
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
    /// GameScreen
    /// </summary>
    public partial class GameScreen : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\GameScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid parentCanvas;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\GameScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid cardHolder;
        
        #line default
        #line hidden
        
        /// <summary>
        /// player1NameLabel Name Field
        /// </summary>
        
        #line 23 "..\..\GameScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public System.Windows.Controls.Label player1NameLabel;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\GameScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Run player1ScoreLabel;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\GameScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label player2NameLabel;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\GameScreen.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.Run player2ScoreLabel;
        
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
            System.Uri resourceLocater = new System.Uri("/MemoryGame;component/gamescreen.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\GameScreen.xaml"
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
            this.parentCanvas = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.cardHolder = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.player1NameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.player1ScoreLabel = ((System.Windows.Documents.Run)(target));
            return;
            case 5:
            this.player2NameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.player2ScoreLabel = ((System.Windows.Documents.Run)(target));
            return;
            case 7:
            
            #line 36 "..\..\GameScreen.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RestartButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 37 "..\..\GameScreen.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.QuitButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

