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

	private Size CurrentButtonSize => new(this.CurrentButtonWidth, this.CurrentButtonHeight);

	private Point NewButtonPosition
	{
		get
		{
			double ang = FormMain.RandomAngle(this._rand);
			var len = this.CurrentLength;
			int x = FormMain.IntCeil(Math.Cos(ang) * len);
			int y = FormMain.IntCeil(Math.Sin(ang) * len);
			return new(x,y);
		}
	}

	//Дистанции до кнопки
	private readonly int[] S = [0, 20, 40, 60, 100, 150, 200, 250, 300, 350];
	private int S_index = -1;
	private int CurrentLength => this.S[this.S_index];

	private int tries = 0;
	private int mode = -1;
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
			this.label1.Text = DateTime.Now.Millisecond.ToString();
		}
	}

	private void HandleFirstClick()
	{
		this.mode = 0;
		this.state = 0;
		Cursor.Position = this.ScreenCentre;
		this.button1.Hide();

		Thread.Sleep(1000);

		this._oldTime = DateTime.Now;
		this.button1.Show();
		this.state = 1;
		this.S_index++;
		this._oldTime = DateTime.Now;

		
		this.button1.Location = this.NewButtonPosition;
		this.button1.Size = this.CurrentButtonSize;
	}

	private void HandleNotFirstClick()
	{
		var stream = File.Open(_filename, FileMode.Append);
		var writer = new StreamWriter(stream);
		writer.WriteLine($"{this.CurrentLength} {this.CurrentButtonHeight} {(DateTime.Now - _oldTime).TotalMilliseconds}");
		writer.Flush();
		writer.Close();
		stream.Close();

		this.tries++;
		Cursor.Position = this.ScreenCentre;

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
		this.button1.Size = this.CurrentButtonSize;
	}

	private void button1_Click (object sender, EventArgs e)
	{
		if (mode == -1) {
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
		Cursor.Position = this.ScreenCentre;
		this.button1.Hide();
		Thread.Sleep(1000);

		this.button1.Show();
		if (this.CurrentLength == 0) {
			this._oldTime = DateTime.Now;
		}
		this.state = 1;

	
		this.button1.Location = this.NewButtonPosition;
		this.button1.Size = this.CurrentButtonSize;
	}

	private Point ScreenCentre
	{
		get
		{
			var x = this.Bounds.X + this.Bounds.Width / 2;
			var y = this.Bounds.Y + this.Bounds.Height / 2;
			return new Point(x, y);
		}
	}

	private void FormMain_MouseMove (object sender, MouseEventArgs e) => this.HandleMouseMove();

	private static int IntCeil(double x) => (int)Math.Ceiling(x);

	private static double RandomAngle(Random rnd) => (rnd.NextDouble() * _angleRange);
}
