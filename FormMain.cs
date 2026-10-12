namespace UI_lab1;

public partial class FormMain : Form
{
	private const string _filename = "times.txt";
	private readonly Random _rand = new();
	private DateTime _oldTime;

	//Размеры кнопок
	private readonly int[] D = [7];
	private int D_index = 0;

	//Дистанции до кнопки
	private readonly int[] S = [0, 20, 40, 60, 100, 150, 200, 250, 300, 350];
	private int S_index = -1;

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
		Cursor.Position = this.Bounds.Location;
		this.button1.Hide();

		Thread.Sleep(1000);

		this._oldTime = DateTime.Now;
		this.button1.Show();
		this.state = 1;
		this.S_index++;
		this._oldTime = DateTime.Now;

		double ang = RandomAngle(this._rand);
		int x = IntCeil(Math.Cos(ang) * this.S[this.S_index]);
		int y = IntCeil(Math.Sin(ang) * this.S[this.S_index]);
		this.button1.Location = new Point (x,y);
		this.button1.Size = new Size((int)(1.5f * this.D[this.D_index]), this.D[this.D_index]);
	}

	private void HandleNotFirstClick()
	{
		var stream = File.Open(_filename, FileMode.Append);
		var writer = new StreamWriter(stream);
		writer.WriteLine($"{this.S[this.S_index]} {this.D[this.D_index]} {(DateTime.Now - _oldTime).TotalMilliseconds}");
		writer.Flush();
		writer.Close();
		stream.Close();

		this.tries++;
		Cursor.Position = this.Bounds.Location;

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
		if (this.S[this.S_index] == 0) {
			this._oldTime = DateTime.Now;
		}

		this.state = 1;

		double ang = RandomAngle(this._rand);
		int x = IntCeil(Math.Cos(ang) * this.S[this.S_index]);
		int y = IntCeil(Math.Sin(ang) * this.S[this.S_index]);
		this.button1.Location = new Point(x, y);

		this.button1.Size = new Size((int)(1.5f * this.D[this.D_index]), this.D[this.D_index]);
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
		Cursor.Position = this.Bounds.Location;
		this.button1.Hide();
		Thread.Sleep(1000);

		this.button1.Show();
		if (this.S[this.S_index] == 0) {
			this._oldTime = DateTime.Now;
		}
		this.state = 1;

		double ang = RandomAngle(this._rand);
		int x = IntCeil(Math.Cos(ang) * this.S[this.S_index]);
		int y = IntCeil(Math.Sin(ang) * this.S[this.S_index]);
		this.button1.Location = new Point(x,y);

		this.button1.Size = new Size((int)(1.5f * this.D[this.D_index]), this.D[this.D_index]);
	}

	private void FormMain_MouseMove (object sender, MouseEventArgs e) => this.HandleMouseMove();

	private static int IntCeil(double x) => (int)Math.Ceiling(x);

	private static double RandomAngle(Random rnd) => (rnd.NextDouble() / 2 * Math.PI);
}
