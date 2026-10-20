namespace UI_lab1;

public partial class FormMain : Form
{
	private const string _filename = "times.txt";
	private const double _angleRange = Math.PI / 2;
	private const int _widthMult = 3;

	private readonly Random _rand = new();
	private DateTime _oldTime;

	//Размеры кнопок
	private readonly int[] D = [7];
	private int D_index = 0;
	private int CurrentButtonHeight => this.D[this.D_index];
	private int CurrentButtonWidth => this.CurrentButtonHeight * _widthMult;

	private Size NewButtonSize => new(this.CurrentButtonWidth, this.CurrentButtonHeight);

	private Point NewButtonPosition
	{
		get
		{
			double ang = FormMain.RandomAngle(this._rand);
			var len = this.CurrentLength;
			int x = FormMain.IntCeil(Math.Cos(ang) * len);
			int y = FormMain.IntCeil(Math.Sin(ang) * len);

			var centre = this.ScreenCentreRelative;
			return new Point (x + centre.X, y + centre.Y);
		}
	}

	//Дистанции до кнопки
	private readonly int[] S = [0, 20, 40, 60, 100, 150, 200, 250, 300, 350];
	private int S_index = -1;
	private int CurrentLength => this.S[this.S_index];

	private int tries = 0;
	private bool ProgramFirstLaunched = true;
	private int state = 0;
	
	public FormMain () => this.InitializeComponent();

	private void HandleMouseMove ()
	{
		if (this.state != 1) {
			return;
		}

		var delta = Cursor.Position.X - Bounds.Location.X;
		if (delta > 0 || this.S_index == 0) {
			this.state = 2;
			this._oldTime = DateTime.Now;
			this.ChangeLabel();
		}
	}

	private void ChangeLabel()
	{
		this.label1.Text = DateTime.Now.Millisecond.ToString();
	}

	private void HandleFirstClick()
	{
		this.ProgramFirstLaunched = false;
		this.state = 0;
		Cursor.Position = this.ScreenCentreAbsolute;
		this.button1.Hide();

		Thread.Sleep(1000);

		this._oldTime = DateTime.Now;
		this.button1.Show();
		this.state = 1;
		this.S_index++;
		this._oldTime = DateTime.Now;

		
		this.button1.Location = this.NewButtonPosition;
		this.button1.Size = this.NewButtonSize;
	}

	private void WriteInFile()
	{
		var curLen = this.CurrentLength;
		var btnHeight = this.CurrentButtonHeight;
		var time = (DateTime.Now - _oldTime).TotalMilliseconds;

		using var stream = File.Open(_filename, FileMode.Append);
		using var writer = new StreamWriter(stream);

		writer.WriteLine($"{curLen} {btnHeight} {time}");
		writer.Flush();
	}

	private void HandleNotFirstClick()
	{
		this.WriteInFile();

		this.tries++;
		Cursor.Position = this.ScreenCentreAbsolute;

		if (this.tries == 5) {
			this.S_index++;
			if (this.S_index >= this.S.Length) {
				this.S_index = 0;
				this.D_index++;
			}
			if (this.D_index >= this.D.Length) {
				Application.Exit();
				return;
			}
			this.tries = 0;
		}
		this.state = 0;
		this.button1.Hide();

		Thread.Sleep(1000);
		
		this.button1.Show();
		if (this.CurrentLength == 0) {
			this._oldTime = DateTime.Now;
		}

		this.state = 1;

		
		this.button1.Location = this.NewButtonPosition;
		this.button1.Size = this.NewButtonSize;
	}

	private void button1_Click (object sender, EventArgs e)
	{
		if (ProgramFirstLaunched == true) {
			this.HandleFirstClick();
		} else {
			this.HandleNotFirstClick();
		}
	}

	private void FormMail_Load (object sender, EventArgs e)
	{
		FileStream stream = File.Open(_filename, FileMode.Create);

		stream.Close();

		this.BackColor = Color.Gray;
		this.button1.BackColor = Color.Gray;
	}

	private void FormMain_MouseClick (object sender, MouseEventArgs e)
	{
		if (this.state != 2) {
			return;
		}

		this.state = 0;
		Cursor.Position = this.ScreenCentreAbsolute;
		this.button1.Hide();
		Thread.Sleep(1000);

		this.button1.Show();
		if (this.CurrentLength == 0) {
			this._oldTime = DateTime.Now;
		}
		this.state = 1;

	
		this.button1.Location = this.NewButtonPosition;
		this.button1.Size = this.NewButtonSize;
	}

	private Point ScreenCentreRelative
	{
		get
		{
			var x = this.Bounds.Width / 2;
			var y = this.Bounds.Height / 2;
			return new Point (x, y);
		}
	}

	private Point ScreenCentreAbsolute
	{
		get
		{
			var centre = this.ScreenCentreRelative;
			var x = this.Bounds.X + centre.X;
			var y = this.Bounds.Y + centre.Y;
			return new Point(x, y);
		}
	}

	private void FormMain_MouseMove (object sender, MouseEventArgs e) => this.HandleMouseMove();

	private static int IntCeil(double x) => (int)Math.Ceiling(x);

	private static double RandomAngle(Random rnd) => (rnd.NextDouble() * _angleRange);
}
