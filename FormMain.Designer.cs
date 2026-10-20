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
	private void InitializeComponent()
	{
		button1 = new Button();
		label1 = new Label();
		SuspendLayout();
		// 
		// button1
		// 
		button1.Location = new Point(11, 11);
		button1.Margin = new Padding(2);
		button1.Name = "button1";
		button1.Size = new Size(61, 28);
		button1.TabIndex = 0;
		button1.Text = "button1";
		button1.UseVisualStyleBackColor = true;
		button1.Click += button1_Click;
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Location = new Point(201, 140);
		label1.Margin = new Padding(2, 0, 2, 0);
		label1.Name = "label1";
		label1.Size = new Size(38, 15);
		label1.TabIndex = 1;
		label1.Text = "label1";
		// 
		// FormMain
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(859, 825);
		Controls.Add(label1);
		Controls.Add(button1);
		FormBorderStyle = FormBorderStyle.None;
		Margin = new Padding(2);
		Name = "FormMain";
		Text = "Form1";
		Load += FormMain_Load;
		MouseClick += FormMain_MouseClick;
		MouseMove += FormMain_MouseMove;
		ResumeLayout(false);
		PerformLayout();

	}

	#endregion

	private System.Windows.Forms.Button button1;
	private System.Windows.Forms.Label label1;
}

