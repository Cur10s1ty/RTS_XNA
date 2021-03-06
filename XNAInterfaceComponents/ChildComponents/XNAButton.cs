﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XNAInterfaceComponents.Interfaces;
using XNAInputHandler.MouseInput;

public delegate void OnClick();

namespace XNAInterfaceComponents.AbstractComponents
{
    public class XNAButton : ChildComponent, Focusable, MouseClickListener
    {
        private OnClick onClickListeners { get; set; }

        public XNAButton(ParentComponent parent, Rectangle bounds, String text)
            : base(parent, bounds)
        {
            this.text = text;
            parent.AddChild(this);
            MouseManager.GetInstance().mouseClickedListeners += OnMouseClick;
            MouseManager.GetInstance().mouseReleasedListeners += OnMouseRelease;
        }

        public override void Draw(SpriteBatch sb)
        {
            if (this.clearTexture == null) clearTexture = ComponentUtil.GetClearTexture2D(sb);
            if( this.parent == null || this.visible == false ) return;

            Color drawColor = new Color();
            if (this.isMouseOver) drawColor = this.mouseOverColor;
            else drawColor = this.backgroundColor;

            sb.Draw(clearTexture, this.GetScreenLocation(), drawColor);
            if (this.border != null) border.Draw(sb);
        }

        public override void Update()
        {

        }

        public void OnFocusReceived()
        {
            this.isFocussed = true;

            Console.Out.WriteLine("XNA Button @ " + this.GetScreenLocation() + " grabbed focus!");
        }

        public void OnFocusLost()
        {
            this.isFocussed = false;

            Console.Out.WriteLine("XNA Button @ " + this.GetScreenLocation() + " lost focus!");
        }

        public void OnMouseClick(MouseEvent m_event)
        {
            if (m_event.button == MouseEvent.MOUSE_BUTTON_1)
            {
                Point screenLocation = parent.RequestScreenLocation(new Point(this.bounds.X, this.bounds.Y));
                Rectangle screenRect = new Rectangle(screenLocation.X, screenLocation.Y, this.bounds.Width, this.bounds.Height);
                if (screenRect.Contains(m_event.location))
                {
                    Console.Out.WriteLine("Pressed on a button!");
                    if (this.onClickListeners != null) onClickListeners();
                }
            }
        }

        public void OnMouseRelease(MouseEvent m_event)
        {

        }

        public override void OnMouseEnter(MouseEvent m_event)
        {
            this.isMouseOver = true;
            Console.Out.WriteLine("XNA Button @ " + this.GetScreenLocation() + " mouse entered!");
        }

        public override void OnMouseExit(MouseEvent m_event)
        {
            this.isMouseOver = false;
            Console.Out.WriteLine("XNA Button @ " + this.GetScreenLocation() + " mouse exitted!");
        }

    }
}
