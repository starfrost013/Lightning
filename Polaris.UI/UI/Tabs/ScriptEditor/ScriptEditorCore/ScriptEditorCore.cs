﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Polaris.UI
{
    /// <summary>
    /// ScriptEditorCore
    /// 
    /// June 13, 2021
    /// 
    /// Implements a Lua script editor for Polaris. 
    /// </summary>
    public class ScriptEditorCore : Control
    {
        public ScriptEditorHighlight Highlight { get; set; }
        public List<TextChunk> Text { get; set; }
        
        public ScriptEditorCore()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScriptEditorCore), new FrameworkPropertyMetadata(typeof(ScriptEditorCore)));

        }


        private void Init()
        {
            base.MouseDown += OnMouseDown;
            Highlight = new ScriptEditorHighlight();
        }

        public override void OnMouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
