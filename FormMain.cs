namespace UI_lab1;

public partial class FormMain : Form
{
	private const string _filename = "times.txt";
	private readonly Random _rand = new();
	private DateTime _oldTime;

	private int S_count = -1;
	private readonly int[] D = [7];

	private int D_count = 0;
	private readonly int[] S = [0, 20, 40, 60, 100, 150, 200, 250, 300, 350];

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
		if (delta > 0 || this.S_count == 0) {
			this.state = 2;
			this._oldTime = DateTime.Now;
			this.label1.Text = DateTime.Now.Millisecond.ToString();
		}
	}

	private void button1_Click (object sender, EventArgs e)
	{
		if (mode == -1) {
			this.mode = 0;
			this.state = 0;
			Cursor.Position = this.Bounds.Location;
			this.button1.Hide();

			Thread.Sleep(1000);

			this._oldTime = DateTime.Now;
			this.button1.Show();
			this.state = 1;
			this.S_count++;
			this._oldTime = DateTime.Now;

			double ang = RandomAngle(this._rand);
			Point p = new(IntCeil(Math.Cos(ang) * this.S[this.S_count]), IntCeil(Math.Sin(ang) * this.S[this.S_count]));
			this.button1.Location = p;
			this.button1.Size = new Size((int)(1.5f * this.D[this.D_count]), this.D[this.D_count]);
		} else {
			var stream = File.Open(_filename, FileMode.Append);
			var writer = new StreamWriter(stream);
			writer.WriteLine($"{this.S[this.S_count]} {this.D[this.D_count]} {(DateTime.Now - _oldTime).TotalMilliseconds}");
			writer.Flush();
			writer.Close();
			stream.Close();

			this.tries++;
			Cursor.Position = this.Bounds.Location;

			if (this.tries == 5) {
				this.S_count++;
				if (this.S_count >= this.S.Length) {
					this.S_count = 0;
					this.D_count++;
				}
				if (this.D_count >= this.D.Length) {
					Application.Exit();
					return;
				}
				this.tries = 0;
			}
			this.state = 0;
			this.button1.Hide();

			Thread.Sleep(1000);
			
			this.button1.Show();
			if (this.S[this.S_count] == 0) {
				this._oldTime = DateTime.Now;
			}

			this.state = 1;

			double ang = RandomAngle(this._rand);
			this.button1.Location = new Point(IntCeil(Math.Cos(ang) * this.S[this.S_count]), IntCeil(Math.Sin(ang) * this.S[this.S_count]));
			this.button1.Size = new Size((int)(1.5f * this.D[this.D_count]), this.D[this.D_count]);
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
		if (this.S[this.S_count] == 0) {
			this._oldTime = DateTime.Now;
		}
		this.state = 1;
		double ang = RandomAngle(this._rand);
		this.button1.Location = new Point(IntCeil(Math.Cos(ang) * this.S[this.S_count]), IntCeil(Math.Sin(ang) * this.S[this.S_count]));

		this.button1.Size = new Size((int)(1.5f * this.D[this.D_count]), this.D[this.D_count]);
	}

	private void FormMain_MouseMove (object sender, MouseEventArgs e) => this.HandleMouseMove();

	private static int IntCeil(double x) => (int)Math.Ceiling(x);

	private static double RandomAngle(Random rnd) => (rnd.NextDouble() / 2 * Math.PI);
}
