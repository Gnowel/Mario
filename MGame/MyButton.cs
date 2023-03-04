using System;
using System.Drawing;
using System.Windows.Forms;

namespace MGame
{
    public class MyButton : Control
    {
		private StringFormat _format;
		private bool _buttonSelected;
		public MyButton()
		{
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw 
				| ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
			DoubleBuffered = true;

			Size = new Size(100, 100);

			_format = new StringFormat();

			_format.Alignment = StringAlignment.Near;
			_format.LineAlignment = StringAlignment.Center;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			Graphics graphics = e.Graphics;


			Rectangle rect = new Rectangle(0, 0, Width, Height);
			graphics.DrawString(Text, Font, new SolidBrush(ForeColor), rect, _format);


		}

		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			_buttonSelected = true;
			Invalidate();
		}

		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			_buttonSelected = false;
			Invalidate();
		}



		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			if (_buttonSelected && (Keys)e.KeyChar == Keys.Enter)
				OnClick(e);
		}
	}
}
