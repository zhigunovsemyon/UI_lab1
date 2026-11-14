namespace UI_lab1;

/*
Общее для заданий:
Прямоугольник 4DxD
Объект белый, фон серый

Всем вариантам соответствуют следующие требования и рекомендации:
    1. Типичный алгоритм работы программы должен быть следующим: 
	а) выбрать номер опыта из меню; 
	б) нарисовать объект и установить курсор в заданные позиции; 
	в) щелкнуть по объекту, отсчитывая время от момента начала движения; 
	г) сохранить промежуточные результаты; 
	д) сделать паузу до нажатия какой-либо кнопки или клавиши; 
	е) перерисовать объект в новом месте окна, установить курсор в начальную позицию и т.д.
    2. Отчет времени должен начинаться от момента начала движения мыши (событие MouseMove) 
	и заканчиваться во время щелчка на объекте (событие Click).
    3. Отчет времени проводить с помощью DataTime.Now 
	(возвращает число тактов, 1 такт = 0,1 мс), функции GetTickCount().
    4. Если не удалось попасть по объекту с первого раза, то данная попытка 
	не учитывается и повторяется снова.
    5. Объект должен быть полностью виден в окне.
    6. Размером объекта D для упрощения расчетов будем считать его высоту.
    7. С целью исключения эффекта натренированности пользователя объект должен 
	выводиться случайным образом вдоль воображаемой окружности с заданным 
	радиусом (дистанцией S) и с центром в позиции указателя мыши 
	(указатель рекомендуется размещать в центре экрана). 
	Если диаметр окружности превышает величину горизонтального разрешения экрана, 
	то указатель мыши перемещают в угол окна и объект выводят 
	по дуге с заданным радиусом.
    8. Результаты измерений времени и расстояний должны сохраняться в текстовых 
	файлах и передаваться в Microsoft Excel.
    9. Для построения графиков рекомендуется в MS Excel или в аналогичном 
	табличном редакторе выбирать тип диаграммы – «Точечная», так как 
	она позволяет учитывать 2 координаты.

Задание 1: D=10, S={0, 20, 40, 60, 100, 150, 200, 250, 300, 350}
	   определить среднее время достижения объекта при каждой дистанции.
Задание 2: D = {8, 10, 12, 15, 20, 30, 40, 50, 70, 100}, S = 550
	   определить среднее время достижения объекта при каждом размере.
Задание 3: D = {8, 10, 12, 15, 20, 30, 40, 50, 70, 100}
           S = {0, 20, 40, 60, 100, 150, 200, 250, 300, 350}
           используя 2 вложенных цикла, определить для каждого S/D ср. время 
           достижения объекта по 3 нажатиям.

Одиночные опыты по 5, комбо по 3
*/

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

	//Дистанции до кнопки
	private readonly int[] S = [0, 20, 40, 60, 100, 150, 200, 250, 300, 350];
	private int S_index = -1;
	private int CurrentDistance => this.S[this.S_index];

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


	private void SetupBenching()
	{
		this.ProgramFirstLaunched = false;
		this.state = 0;
		this.CenterCursor();
		HideObject();

		Thread.Sleep(1000);

		this._oldTime = DateTime.Now;
		ShowObject();
		this.state = 1;
		this.S_index++;
		this._oldTime = DateTime.Now; // лишний (?)


		SetObject();
	}

	private void WriteInFile()
	{
		var distance = this.CurrentDistance;
		var btnHeight = this.CurrentButtonHeight;
		var time = (DateTime.Now - _oldTime).TotalMilliseconds;

		using var stream = File.Open(_filename, FileMode.Append);
		using var writer = new StreamWriter(stream);

		writer.WriteLine($"{distance} {btnHeight} {time}");
		writer.Flush();
	}

	private void UserMissedButton ()
	{
		this.state = 0;
		this.CenterCursor();
		HideObject();
		Thread.Sleep(1000);

		ShowObject();
		if (this.CurrentDistance == 0) {
			this._oldTime = DateTime.Now;
		}
		this.state = 1;


		SetObject();
	}

	private void UserHitButton()
	{
		this.WriteInFile();

		this.tries++;
		this.CenterCursor();

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
		HideObject();

		Thread.Sleep(1000);

		ShowObject();

		if (this.CurrentDistance == 0) {
			this._oldTime = DateTime.Now;
		}

		this.state = 1;

		SetObject();
	}

	
	private void HandleClick ()
	{
		if (ProgramFirstLaunched == true) {
			this.SetupBenching();
		} 
		else {
			this.UserHitButton();
		}
	}

	private void SetObject ()
	{
		int currentButtonWidth = this.CurrentButtonHeight * _widthMult;
		this.button1.Size = new Size (currentButtonWidth, this.CurrentButtonHeight);

		double ang = _rand.NextDouble() * _angleRange;
		var distance = this.CurrentDistance;

		var centre = this.ScreenCentreRelative;
		int x = (int)Math.Ceiling(Math.Cos(ang) * distance) + centre.X;
		int y = (int)Math.Ceiling(Math.Sin(ang) * distance) + centre.Y;

		this.button1.Location = new Point (x, y);
	}

	private void FormMain_Load (object sender, EventArgs e)
	{
		FileStream stream = File.Open(_filename, FileMode.Create);

		stream.Close();

		this.BackColor = Color.Gray;
		this.button1.BackColor = Color.Gray;
	}

	private void FormMain_MouseClick(object sender, MouseEventArgs e)
	{
		if (this.state == 2) {
			this.UserMissedButton();
		}
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

	private void CenterCursor ()
	{
		var centre = this.ScreenCentreRelative;
		var x = this.Bounds.X + centre.X;
		var y = this.Bounds.Y + centre.Y;
		Cursor.Position = new Point(x, y);
	}

	private void HideObject () => this.button1.Hide();

	private void ShowObject () => this.button1.Show();

	private void ChangeLabel () => this.label1.Text = DateTime.Now.Millisecond.ToString();

	private void FormMain_MouseMove (object sender, MouseEventArgs e) => this.HandleMouseMove();

	private void button1_Click (object sender, EventArgs e) => HandleClick();
}
