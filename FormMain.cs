using System.Runtime.InteropServices;

namespace CaptureMousePos
{
    public partial class FormMain : Form
    {
        private LowLevelKeyboardHook _hook;
        private Point _pt;

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        public FormMain()
        {
            InitializeComponent();
        }

        private void _hook_KeyDown(object? sender, EventArgs e)
        {
            GetCursorPos(ref _pt);

            textBoxOutput.Text += $"{_pt.X}, {_pt.Y}{Environment.NewLine}";

            textBoxOutput.SelectionStart = textBoxOutput.Text.Length;
            textBoxOutput.ScrollToCaret();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            _hook = new LowLevelKeyboardHook();

            _hook.KeyDown += _hook_KeyDown;

            _hook.Hook();

            _pt = new Point();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _hook.Unhook();
        }
    }
}