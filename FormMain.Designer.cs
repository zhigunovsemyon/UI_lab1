namespace UI_lab1;

partial class FormMain
{
	/// <summary>
	/// Обязательная переменная конструктора.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// Освободить все используемые ресурсы.
	/// </summary>
	/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
	protected override void Dispose (bool disposing)
	{
		if (disposing && (components != null)) {
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Код, автоматически созданный конструктором форм Windows

	/// <summary>
	/// Требуемый метод для поддержки конструктора — не изменяйте 
	/// содержимое этого метода с помощью редактора кода.
	/// </summary>
	private void InitializeComponent ()
	{
		this.panel = new Panel();
		this.SuspendLayout();
		// 
		// panel
		// 
		this.panel.BackColor = Color.White;
		this.panel.Location = new Point(358, 389);
		this.panel.Name = "panel";
		this.panel.Size = new Size(193, 174);
		this.panel.TabIndex = 2;
		this.panel.MouseClick += this.panel1_MouseClick;
		// 
		// FormMain
		// 
		this.AutoScaleDimensions = new SizeF(7F, 15F);
		this.AutoScaleMode = AutoScaleMode.Font;
		this.BackColor = Color.Gray;
		this.ClientSize = new Size(859, 825);
		this.Controls.Add(this.panel);
		this.FormBorderStyle = FormBorderStyle.None;
		this.Margin = new Padding(2);
		this.Name = "FormMain";
		this.Text = "Form1";
		this.Load += this.FormMain_Load;
		this.MouseClick += this.FormMain_MouseClick;
		this.MouseMove += this.FormMain_MouseMove;
		this.ResumeLayout(false);

	}

	#endregion
	private Panel panel;
}

